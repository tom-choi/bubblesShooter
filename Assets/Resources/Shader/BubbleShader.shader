Shader "Custom/BubbleShader"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.95
        _FresnelPower ("Fresnel Power", Range(1,10)) = 5
        _ChromaticAberration ("Chromatic Aberration", Range(0,0.1)) = 0.02
        _DistortionSpeed ("Distortion Speed", Range(0,2)) = 0.5
        _DistortionAmount ("Distortion Amount", Range(0,1)) = 0.1
        _RippleSpeed ("Ripple Speed", Range(0,5)) = 1
        _RippleScale ("Ripple Scale", Range(1,50)) = 20
        _RippleStrength ("Ripple Strength", Range(0,0.1)) = 0.02
        _FoamDensity ("Foam Density", Range(1,100)) = 30
        _FoamSpeed ("Foam Speed", Range(0,2)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        
        Pass
        {
            Name "BubblePass"
            
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
            };

            float _Glossiness;
            float _FresnelPower;
            float _ChromaticAberration;
            float _DistortionSpeed;
            float _DistortionAmount;
            float _RippleSpeed;
            float _RippleScale;
            float _RippleStrength;
            float _FoamDensity;
            float _FoamSpeed;

            float3 random3(float3 p)
            {
                p = float3(dot(p, float3(127.1, 311.7, 74.7)),
                          dot(p, float3(269.5, 183.3, 246.1)),
                          dot(p, float3(113.5, 271.9, 124.6)));
                return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
            }

            // 噪聲函數
            float noise(float3 p)
            {
                float3 i = floor(p);
                float3 f = frac(p);
                f = f * f * (3.0 - 2.0 * f);
                
                float2 c = float2(0, 1);
                return lerp(lerp(lerp(dot(random3(i + c.xxx), f - c.xxx),
                                   dot(random3(i + c.yxx), f - c.yxx), f.x),
                               lerp(dot(random3(i + c.xyx), f - c.xyx),
                                   dot(random3(i + c.yyx), f - c.yyx), f.x), f.y),
                           lerp(lerp(dot(random3(i + c.xxy), f - c.xxy),
                                   dot(random3(i + c.yxy), f - c.yxy), f.x),
                               lerp(dot(random3(i + c.xyy), f - c.xyy),
                                   dot(random3(i + c.yyy), f - c.yyy), f.x), f.y), f.z);
            }

            

            // 動態波紋
            float ripple(float3 p)
            {
                float t = _Time.y * _RippleSpeed;
                return sin(length(p) * _RippleScale + t) * _RippleStrength;
            }

            // 泡沫紋理
            float foam(float3 p)
            {
                float t = _Time.y * _FoamSpeed;
                float n = noise(p * _FoamDensity + t);
                return smoothstep(0.4, 0.6, n);
            }

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                // 動態變形
                float3 pos = input.positionOS.xyz;
                float distortion = noise(pos + _Time.y * _DistortionSpeed) * _DistortionAmount;
                pos += input.normalOS * distortion;
                
                // 加入波紋位移
                pos += input.normalOS * ripple(pos);
                
                output.positionWS = TransformObjectToWorld(pos);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.viewDirWS = GetWorldSpaceViewDir(output.positionWS);
                
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                float3 normalWS = normalize(input.normalWS);
                float3 viewDirWS = normalize(input.viewDirWS);
                
                // 菲涅爾效果
                float fresnel = pow(1.0 - saturate(dot(normalWS, viewDirWS)), _FresnelPower);
                
                // 色散效果
                float3 refractDir = refract(-viewDirWS, normalWS, 1.0 / 1.33);
                float3 refractDirR = refract(-viewDirWS, normalWS, 1.0 / (1.33 + _ChromaticAberration));
                float3 refractDirB = refract(-viewDirWS, normalWS, 1.0 / (1.33 - _ChromaticAberration));
                
                // 泡沫效果
                float foamEffect = foam(input.positionWS);
                
                // 最終顏色
                float3 color = lerp(float3(1,1,1), float3(1,0.7,0.3), fresnel);
                color = lerp(color, float3(0.8,0.9,1.0), foamEffect * 0.3);
                
                // 添加彩虹色散
                color.r += dot(refractDirR, normalWS) * 0.1;
                color.b += dot(refractDirB, normalWS) * 0.1;
                
                float alpha = lerp(0.2, 0.8, fresnel + foamEffect);
                
                return float4(color, alpha);
            }
            ENDHLSL
        }
    }
}