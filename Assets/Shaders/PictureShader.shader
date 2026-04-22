Shader "PictureShader"
{
    Properties
    {
        _Scale ("Fractal Scale", Float) = 2
        _OffsetX ("Offset X", Float) = -0.5
        _OffsetY ("Offset Y", Float) = 0.0
        _Iterations ("Iterations", Float) = 200
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Scale;
            float _OffsetX;
            float _OffsetY;
            int _Iterations;

            float4x4 _ModelMatrix;
            float4x4 _ViewMatrix;
            float4x4 _ProjectionMatrix;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 clipPos : SV_POSITION;
                float2 objPos  : TEXCOORD0;
            };

            // ───── Vertex ──────────────────────────
            v2f vert(appdata v)
            {
                v2f o;

                // guardar coordenadas locales del mesh
                o.objPos = v.vertex.xy;

                float4 worldPos = mul(_ModelMatrix, v.vertex);
                float4 viewPos = mul(_ViewMatrix, worldPos);
                o.clipPos = mul(_ProjectionMatrix, viewPos);

                return o;
            }

            // ───── Mandelbrot ──────────────────────
            float mandelbrot(float2 c)
            {
                float2 z = float2(0, 0);
                for (int i = 0; i < _Iterations; i++)
                {
                    float x = z.x*z.x - z.y*z.y + c.x;
                    float y = 2.0*z.x*z.y      + c.y;
                    z = float2(x, y);
                    if (dot(z, z) > 4.0)
                        return (float)i / _Iterations;
                }
                return 0.0;  // nunca escapó → interior → negro
            }

            // ───── Fragment ────────────────────────
            fixed4 frag(v2f i) : SV_Target
            {
                float2 p = i.objPos * 4.0;

                float2 c = p / _Scale + float2(_OffsetX,_OffsetY);

                float m = mandelbrot(c);

                // interior del fractal → negro
                if (m == 0)
                    return float4(0,0,0,1);

                float3 col = float3(
                    0.23 + 0.5*cos(6.2831*(m + 0.0)),
                    0.34 + 0.5*cos(6.2831*(m + 0.33)),
                    0.35 + 0.5*cos(6.2831*(m + 0.66))
                );

                return float4(col,1);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}