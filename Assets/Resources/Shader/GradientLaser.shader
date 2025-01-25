Shader "Custom/GradientLaser"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 0, 0, 1)
        _Color2 ("Color 2", Color) = (0, 0, 1, 1)
        _Width ("Width", Range(0, 1)) = 0.1
        _Softness ("Softness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color1;
            fixed4 _Color2;
            float _Width;
            float _Softness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float gradient = i.uv.x < _Width ? i.uv.x / _Width : 1.0;

                // 使用平滑插值來使邊緣柔和
                float smoothStep = smoothstep(0.0, _Softness, gradient);
                
                fixed4 color = lerp(_Color1, _Color2, smoothStep);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}