Shader "Custom/AdvancedEffectsShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _HeightMap ("Height Map", 2D) = "black" {}
        _Color ("Color", Color) = (1,1,1,1)
        _IsEdge ("Is Edge", Float) = 0
        
        // 視差映射參數
        _ParallaxScale ("Parallax Scale", Range(0.005, 0.08)) = 0.02
        _ParallaxMinSamples ("Parallax Min Samples", Range(2, 100)) = 4
        _ParallaxMaxSamples ("Parallax Max Samples", Range(2, 100)) = 20
        
        // 全息效果參數
        _HoloColor ("Hologram Color", Color) = (0,1,0.9,1)
        _HoloSpeed ("Hologram Speed", Range(0,10)) = 1
        _HoloLineFrequency ("Hologram Line Frequency", Range(0,100)) = 50
        _HoloIntensity ("Hologram Intensity", Range(0,1)) = 0.5
        
        // 程序化紋理參數
        _NoiseScale ("Noise Scale", Range(0,50)) = 10
        _NoiseStrength ("Noise Strength", Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float3 tangentViewDir : TEXCOORD3;
                float3x3 TBN : TEXCOORD4;
            };

            sampler2D _MainTex, _NormalMap, _HeightMap;
            float4 _MainTex_ST;
            float4 _Color, _HoloColor;
            float _IsEdge, _ParallaxScale;
            float _HoloSpeed, _HoloLineFrequency, _HoloIntensity;
            float _NoiseScale, _NoiseStrength;
            float _ParallaxMinSamples, _ParallaxMaxSamples;

            // 3D Noise函數
            float3 mod289(float3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
            float4 mod289(float4 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
            float4 permute(float4 x) { return mod289(((x*34.0)+1.0)*x); }
            float4 taylorInvSqrt(float4 r) { return 1.79284291400159 - 0.85373472095314 * r; }

            float snoise(float3 v)
            {
                const float2 C = float2(1.0/6.0, 1.0/3.0);
                const float4 D = float4(0.0, 0.5, 1.0, 2.0);

                float3 i  = floor(v + dot(v, C.yyy));
                float3 x0 = v - i + dot(i, C.xxx);

                float3 g = step(x0.yzx, x0.xyz);
                float3 l = 1.0 - g;
                float3 i1 = min(g.xyz, l.zxy);
                float3 i2 = max(g.xyz, l.zxy);

                float3 x1 = x0 - i1 + C.xxx;
                float3 x2 = x0 - i2 + C.yyy;
                float3 x3 = x0 - D.yyy;

                i = mod289(i);
                float4 p = permute(permute(permute(
                    i.z + float4(0.0, i1.z, i2.z, 1.0))
                    + i.y + float4(0.0, i1.y, i2.y, 1.0))
                    + i.x + float4(0.0, i1.x, i2.x, 1.0));

                float n_ = 0.142857142857;
                float3 ns = n_ * D.wyz - D.xzx;

                float4 j = p - 49.0 * floor(p * ns.z * ns.z);

                float4 x_ = floor(j * ns.z);
                float4 y_ = floor(j - 7.0 * x_);

                float4 x = x_ *ns.x + ns.yyyy;
                float4 y = y_ *ns.x + ns.yyyy;
                float4 h = 1.0 - abs(x) - abs(y);

                float4 b0 = float4(x.xy, y.xy);
                float4 b1 = float4(x.zw, y.zw);

                float4 s0 = floor(b0)*2.0 + 1.0;
                float4 s1 = floor(b1)*2.0 + 1.0;
                float4 sh = -step(h, float4(0,0,0,0));

                float4 a0 = b0.xzyw + s0.xzyw*sh.xxyy;
                float4 a1 = b1.xzyw + s1.xzyw*sh.zzww;

                float3 p0 = float3(a0.xy, h.x);
                float3 p1 = float3(a0.zw, h.y);
                float3 p2 = float3(a1.xy, h.z);
                float3 p3 = float3(a1.zw, h.w);

                float4 norm = taylorInvSqrt(float4(dot(p0,p0), dot(p1,p1), dot(p2,p2), dot(p3,p3)));
                p0 *= norm.x;
                p1 *= norm.y;
                p2 *= norm.z;
                p3 *= norm.w;

                float4 m = max(0.6 - float4(dot(x0,x0), dot(x1,x1), dot(x2,x2), dot(x3,x3)), 0.0);
                m = m * m;
                return 42.0 * dot(m*m, float4(dot(p0,x0), dot(p1,x1), dot(p2,x2), dot(p3,x3)));
            }

            // 視差映射函數
            float2 ParallaxMapping(float2 uv, float3 viewDir)
            {
                float numLayers = lerp(_ParallaxMaxSamples, _ParallaxMinSamples, abs(dot(float3(0, 0, 1), viewDir)));
                float layerDepth = 1.0 / numLayers;
                float currentLayerDepth = 0.0;
                float2 P = viewDir.xy * _ParallaxScale; 
                float2 deltaUV = P / numLayers;
                
                float2 currentUV = uv;
                float currentDepthMapValue = tex2D(_HeightMap, currentUV).r;
                
                [unroll(100)]
                while(currentLayerDepth < currentDepthMapValue)
                {
                    currentUV -= deltaUV;
                    currentDepthMapValue = tex2D(_HeightMap, currentUV).r;
                    currentLayerDepth += layerDepth;
                }
                
                float2 prevUV = currentUV + deltaUV;
                float afterDepth  = currentDepthMapValue - currentLayerDepth;
                float beforeDepth = tex2D(_HeightMap, prevUV).r - currentLayerDepth + layerDepth;
                
                float weight = afterDepth / (afterDepth - beforeDepth);
                float2 finalUV = prevUV * weight + currentUV * (1.0 - weight);
                
                return finalUV;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                // 計算TBN矩陣
                float3 tangent = UnityObjectToWorldDir(v.tangent.xyz);
                float3 bitangent = cross(o.normal, tangent) * v.tangent.w;
                o.TBN = float3x3(tangent, bitangent, o.normal);
                
                // 視差映射需要的視角方向
                o.viewDir = normalize(_WorldSpaceCameraPos.xyz - o.worldPos);
                o.tangentViewDir = mul(o.TBN, o.viewDir);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
                
                // 使用視差映射計算偏移後的UV
                float2 parallaxUV = ParallaxMapping(i.uv, i.tangentViewDir);
                float2 uv = parallaxUV;
                
                // 原始UV邏輯
                if (normal.y < 0.5) {
                    if (normal.z > -0.5)
                    {
                        uv.y = uv.y * 0.5;
                    }
                    else 
                    {
                        uv.x = 1.0 - uv.x;
                        uv.y = (1.0 - uv.y) * 0.5;
                    }
                }
                else {
                    if (uv.y <= 0.035 && _IsEdge == 0.0) {
                        uv.y = 1.0;
                    } else {
                        uv.y = 0.5 + uv.y * 0.5;
                    }
                }
                
                // 基礎顏色
                fixed4 col = tex2D(_MainTex, uv) * _Color;
                
                // 程序化噪聲
                float noise = snoise(float3(i.worldPos.xy * _NoiseScale, _Time.y));
                
                // 全息效果
                float scanLines = sin(i.worldPos.y * _HoloLineFrequency + _Time.y * _HoloSpeed);
                float hologram = saturate(scanLines * 0.5 + 0.5);
                
                // 混合所有效果
                float3 finalColor = lerp(col.rgb, _HoloColor.rgb, hologram * _HoloIntensity);
                finalColor += noise * _NoiseStrength;
                
                // 菲涅爾效果
                float fresnel = pow(1.0 - saturate(dot(normal, i.viewDir)), 5.0);
                finalColor += _HoloColor.rgb * fresnel * _HoloIntensity;
                
                return float4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}