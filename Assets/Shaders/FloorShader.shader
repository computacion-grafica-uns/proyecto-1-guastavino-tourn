Shader "FloorShader"
{
    Properties
    {
        _PlankWidth   ("Plank Width",    Float) = 0.4
        _PlankLength  ("Plank Length",   Float) = 2.0
        _WoodLight    ("Wood Light",     Color) = (0.76, 0.60, 0.42, 1)
        _WoodDark     ("Wood Dark",      Color) = (0.45, 0.30, 0.18, 1)
        _VeinStrength ("Vein Strength",  Float) = 0.3
        _GapSize      ("Gap Size",       Float) = 0.03
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ── Uniforms ──────────────────────────────────────────────
            float  _PlankWidth;
            float  _PlankLength;
            float4 _WoodLight;
            float4 _WoodDark;
            float  _VeinStrength;
            float  _GapSize;

            uniform float4x4 _ModelMatrix;
			uniform float4x4 _ViewMatrix;
			uniform float4x4 _ProjectionMatrix;

            // ── Structs ───────────────────────────────────────────────
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 clipPos   : SV_POSITION;
                float3 worldPos  : TEXCOORD0;
            };

            // ── Vertex ────────────────────────────────────────────────
            v2f vert(appdata v)
            {
                v2f o;

                // Posición en mundo usando tu _ModelMatrix
                float4 worldPos4 = mul(_ModelMatrix, v.vertex);
                o.worldPos = worldPos4.xyz;

                // Posición en clip space usando tus matrices de vista/proyección
                o.clipPos = mul(_ProjectionMatrix, mul(_ViewMatrix, worldPos4));
                return o;
            }
			// ── Fragment ──────────────────────────────────────────────
            fixed4 frag(v2f i) : SV_Target
            {
                // 1. Coordenadas por tabla
                float2 plankCoord = i.worldPos.xz / float2(_PlankWidth, _PlankLength);

                // 2. Offset alternado: las tablas impares arrancan desplazadas
                float2 plankID = floor(plankCoord);
                float  offset  = frac(plankID.x * 0.5) * 0.5;
                plankCoord.y  += offset;

                // Recalcular ID y posición local con el offset aplicado
                plankID  = floor(plankCoord);
                float2 localPos = frac(plankCoord);

                // 3. Color base pseudo-aleatorio por tabla
                float hash = frac(sin(dot(plankID, float2(127.1, 311.7))) * 43758.5);
                float4 baseColor = lerp(_WoodDark, _WoodLight, hash);

                // 4. Veta principal (corre a lo largo de la tabla)
                float vein     = sin(localPos.x * 20.0 + hash * 6.28) * 0.5 + 0.5;
                float fineVein = sin(localPos.x * 60.0 + hash * 10.0) * 0.5 + 0.5;
                float finalVein = lerp(vein, fineVein, 0.3);
                baseColor = lerp(baseColor, baseColor * 0.75, finalVein * _VeinStrength);

                // 5. Juntas entre tablas
                float gapX = step(1.0 - _GapSize, localPos.x);
                float gapZ = step(1.0 - _GapSize, localPos.y);
                float gap  = max(gapX, gapZ);
                baseColor  = lerp(baseColor, float4(0.05, 0.03, 0.02, 1.0), gap);

                // 6. Sombra suave en los bordes (sensación de volumen)
                float edgeDist   = min(localPos.x, 1.0 - localPos.x);
                float edgeShadow = smoothstep(0.0, 0.08, edgeDist);
                baseColor.rgb   *= lerp(0.7, 1.0, edgeShadow);

                return baseColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}