Shader "Custom/NormalCubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _IsEdge ("Is Edge", Float) = 0
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
            Float _IsEdge;

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
                
                // 決定使用哪個部分的紋理
                float2 uv = i.uv;
                
                // 若是頂部面
                if (normal.y < 0.5) {
                    // 使用圖片上半部分 (前18像素)
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
                // 若是其他面
                else {
                    // 使用圖片下半部分 (後18像素)
                    // uv.y = 0.5 + uv.y * 0.5;
                    if (uv.y <= 0.035 && _IsEdge == 0.0) {
                        // 在頂部邊緣使用圖片最頂端的像素
                        uv.y = 1.0;
                    } else {
                        // 正常映射頂部圖案
                        uv.y = 0.5 + uv.y * 0.5; // 18/50 = 0.36
                    }
                }
                
                // 調整X座標以配合50像素寬度
                // uv.x = uv.x * (36.0/50.0);
                
                fixed4 col = tex2D(_MainTex, uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}