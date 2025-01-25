Shader "Custom/EnhancedCubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _IsEdge ("Is Edge", Float) = 0
        _PixelSize ("Pixel Size", Range(1,100)) = 32
        _GlowColor ("Glow Color", Color) = (1,0,0,1)
        _GlowIntensity ("Glow Intensity", Range(0,2)) = 1
        _ColorDistortion ("Color Distortion", Range(0,1)) = 0.1
        _IsNoise ("Noise", Range(0,1)) = 0
        _TimeScale ("Time Scale", Range(0,10)) = 1
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _IsEdge;
            float _PixelSize;
            float4 _GlowColor;
            float _GlowIntensity;
            float _ColorDistortion;
            float _TimeScale;
            float _IsNoise;

            // 隨機函數
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
                
                // 像素化效果
                float2 pixelUV = floor(i.uv * _PixelSize) / _PixelSize;
                
                // 決定使用哪個部分的紋理
                float2 uv = pixelUV;
                
                // 顏色扭曲效果
                float2 distortion = float2(
                    sin(_Time.y * _TimeScale + uv.y * 10) * _ColorDistortion,
                    cos(_Time.y * _TimeScale + uv.x * 10) * _ColorDistortion
                );
                uv += distortion;
                
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
                
                fixed4 col = tex2D(_MainTex, uv) * _Color;
                
                // 邊緣發光效果
                float edge = 1 - abs(dot(normal, float3(0,0,1)));
                col.rgb += _GlowColor.rgb * edge * _GlowIntensity;
                
                // 添加一些隨機噪點
                if (_IsNoise >= 1.0)
                {
                    float noise = rand(uv + _Time.y) * 0.1;
                    col.rgb += noise;
                }
                
                // 時間基礎的顏色調整
                float colorShift = sin(_Time.y * _TimeScale) * 0.2 + 0.8;
                col.rgb *= colorShift;
                
                return col;
            }
            ENDCG
        }
    }
}
