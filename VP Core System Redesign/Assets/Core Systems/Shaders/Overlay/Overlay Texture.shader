Shader "Vanishing_Point/OverlayTextureShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayTexture_1("OverlayTexture 1", 2D) = "white" {}
        _OverlayTexture_2("OverlayTexture 2", 2D) = "white" {}
        _DetectColor_1("Detect Color 1", Color) = (1,1,1,1)
        _DetectColor_2("Detect Color 2", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _OverlayTexture_1;
            sampler2D _OverlayTexture_2;

            fixed4 _DetectColor_1;
            fixed4 _DetectColor_2;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 ovTex_1 = tex2D(_OverlayTexture_1,i.uv);
                fixed4 ovTex_2 = tex2D(_OverlayTexture_2,i.uv);
                
                fixed4 ovCol_1 = _DetectColor_1;
                fixed4 ovCol_2 = _DetectColor_2;

                float f = (col.r + col.g + col.b) / 3;
                if (f == 1) { col.rgb = 0; col.a = 0; }
                
                bool ov1 = ovCol_1 == col;
                bool ov2 = ovCol_2 == col;

                if (f != 1) { col.rgb = 1; }

                if (ov1) { col = col * ovTex_1; }
                if (ov2) { col = col * ovTex_2; }
                if (!ov2 && !ov1) { col.a = 0; }

                return col;
            }
            ENDCG
        }
    }
}
