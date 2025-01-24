Shader "Custom/AdvancedBubbleShader"
{
    Properties
    {
        // 基礎參數
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (1,0.7,0.3,1)
        _FoamColor ("Foam Color", Color) = (0.8,0.9,1.0,1)
        
        // 透明度參數
        _MinAlpha ("Minimum Alpha", Range(0,1)) = 0.2
        _MaxAlpha ("Maximum Alpha", Range(0,1)) = 0.8
        _Glossiness ("Smoothness", Range(0,1)) = 0.95
        
        // 菲涅爾效果
        _FresnelPower ("Fresnel Power", Range(1,10)) = 5
        _FresnelIntensity ("Fresnel Intensity", Range(0,2)) = 1
        
        // 色散效果
        _ChromaticAberration ("Chromatic Aberration", Range(0,0.1)) = 0.02
        _ChromaticIntensity ("Chromatic Intensity", Range(0,1)) = 0.1
        _RefractionIndex ("Refraction Index", Range(1,2)) = 1.33
        
        // 扭曲效果
        _DistortionSpeed ("Distortion Speed", Range(0,2)) = 0.5
        _DistortionAmount ("Distortion Amount", Range(0,1)) = 0.1
        _DistortionScale ("Distortion Scale", Range(1,50)) = 10
        
        // 波紋效果
        _RippleSpeed ("Ripple Speed", Range(0,5)) = 1
        _RippleScale ("Ripple Scale", Range(1,50)) = 20
        _RippleStrength ("Ripple Strength", Range(0,0.1)) = 0.02
        _RippleDensity ("Ripple Density", Range(1,10)) = 3
        
        // 泡沫效果
        _FoamDensity ("Foam Density", Range(1,100)) = 30
        _FoamSpeed ("Foam Speed", Range(0,2)) = 0.5
        _FoamIntensity ("Foam Intensity", Range(0,1)) = 0.3
        _FoamContrast ("Foam Contrast", Range(0,1)) = 0.2
        
        // 流動效果
        _FlowSpeed ("Flow Speed", Range(0,2)) = 0.5
        _FlowDirection ("Flow Direction", Vector) = (1,1,0,0)
        
        // 光澤效果
        _SpecularPower ("Specular Power", Range(1,100)) = 50
        _SpecularIntensity ("Specular Intensity", Range(0,2)) = 1
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
                float4 screenPos : TEXCOORD3;
            };

            // 聲明所有屬性變量
            float4 _BaseColor;
            float4 _RimColor;
            float4 _FoamColor;
            float _MinAlpha;
            float _MaxAlpha;
            float _Glossiness;
            float _FresnelPower;
            float _FresnelIntensity;
            float _ChromaticAberration;
            float _ChromaticIntensity;
            float _RefractionIndex;
            float _DistortionSpeed;
            float _DistortionAmount;
            float _DistortionScale;
            float _RippleSpeed;
            float _RippleScale;
            float _RippleStrength;
            float _RippleDensity;
            float _FoamDensity;
            float _FoamSpeed;
            float _FoamIntensity;
            float _FoamContrast;
            float _FlowSpeed;
            float4 _FlowDirection;
            float _SpecularPower;
            float _SpecularIntensity;

            // 改進的噪聲函數
            float3 hash3(float3 p)
            {
                p = float3(dot(p,float3(127.1,311.7,74.7)),
                          dot(p,float3(269.5,183.3,246.1)),
                          dot(p,float3(113.5,271.9,124.6)));
                return -1.0 + 2.0 * frac(sin(p)*43758.5453123);
            }

            float noise(float3 p)
            {
                float3 i = floor(p);
                float3 f = frac(p);
                f = f * f * (3.0 - 2.0 * f);
                
                float n = lerp(lerp(lerp(dot(hash3(i + float3(0,0,0)), f - float3(0,0,0)),
                                      dot(hash3(i + float3(1,0,0)), f - float3(1,0,0)), f.x),
                                 lerp(dot(hash3(i + float3(0,1,0)), f - float3(0,1,0)),
                                      dot(hash3(i + float3(1,1,0)), f - float3(1,1,0)), f.x), f.y),
                            lerp(lerp(dot(hash3(i + float3(0,0,1)), f - float3(0,0,1)),
                                      dot(hash3(i + float3(1,0,1)), f - float3(1,0,1)), f.x),
                                 lerp(dot(hash3(i + float3(0,1,1)), f - float3(0,1,1)),
                                      dot(hash3(i + float3(1,1,1)), f - float3(1,1,1)), f.x), f.y), f.z);
                return 0.5 + 0.5 * n;
            }

            // 疊加噪聲
            float fbm(float3 p)
            {
                float f = 0.0;
                float amp = 0.5;
                for(int i = 0; i < 4; i++)
                {
                    f += amp * noise(p);
                    p *= 2.0;
                    amp *= 0.5;
                }
                return f;
            }

            // 改進的波紋函數
            float ripple(float3 p)
            {
                float t = _Time.y * _RippleSpeed;
                float result = 0;
                for(int i = 0; i < _RippleDensity; i++)
                {
                    result += sin(length(p) * _RippleScale * (1.0 + i * 0.5) + t * (1.0 + i * 0.2)) 
                             * _RippleStrength / (1.0 + i);
                }
                return result;
            }

            // 改進的泡沫函數
            float foam(float3 p)
            {
                float t = _Time.y * _FoamSpeed;
                float2 flow = _FlowDirection.xy * t;
                float n = fbm(p * _FoamDensity + float3(flow.x, flow.y, t));
                return smoothstep(0.5 - _FoamContrast, 0.5 + _FoamContrast, n);
            }

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                // 計算扭曲和流動
                float3 pos = input.positionOS.xyz;
                float distortion = fbm(pos * _DistortionScale + _Time.y * _DistortionSpeed) * _DistortionAmount;
                pos += input.normalOS * distortion;
                
                // 添加波紋
                pos += input.normalOS * ripple(pos);
                
                // 轉換座標
                output.positionWS = TransformObjectToWorld(pos);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.viewDirWS = GetWorldSpaceViewDir(output.positionWS);
                output.screenPos = ComputeScreenPos(output.positionCS);
                
                return output;
            }

            float4 frag(Varyings input) : SV_Target
            {
                float3 normalWS = normalize(input.normalWS);
                float3 viewDirWS = normalize(input.viewDirWS);
                float3 lightDir = GetMainLight().direction;
                
                // 菲涅爾效果
                float fresnel = pow(1.0 - saturate(dot(normalWS, viewDirWS)), _FresnelPower) * _FresnelIntensity;
                
                // 高光效果
                float3 halfDir = normalize(lightDir + viewDirWS);
                float specular = pow(max(dot(normalWS, halfDir), 0.0), _SpecularPower) * _SpecularIntensity;
                
                // 色散效果
                float3 refractDir = refract(-viewDirWS, normalWS, 1.0 / _RefractionIndex);
                float3 refractDirR = refract(-viewDirWS, normalWS, 1.0 / (_RefractionIndex + _ChromaticAberration));
                float3 refractDirB = refract(-viewDirWS, normalWS, 1.0 / (_RefractionIndex - _ChromaticAberration));
                
                // 泡沫效果
                float foamEffect = foam(input.positionWS) * _FoamIntensity;
                
                // 顏色混合
                float3 color = lerp(_BaseColor.rgb, _RimColor.rgb, fresnel);
                color = lerp(color, _FoamColor.rgb, foamEffect);
                
                // 添加色散
                float chromatic = _ChromaticIntensity;
                color.r += dot(refractDirR, normalWS) * chromatic;
                color.b += dot(refractDirB, normalWS) * chromatic;
                
                // 添加高光
                color += specular * _BaseColor.rgb;
                
                // 計算最終透明度
                float alpha = lerp(_MinAlpha, _MaxAlpha, fresnel + foamEffect);
                
                return float4(color, alpha);
            }
            ENDHLSL
        }
    }
}