Shader "BathroomFloorShader"
{
    Properties
    {
        _TileSize     ("Tile Size",      Float)  = 0.5
        _GapSize      ("Gap Size",       Float)  = 0.02
        _TileLight    ("Tile Light",     Color)  = (0.92, 0.92, 0.92, 1)
        _TileDark     ("Tile Dark",      Color)  = (0.75, 0.75, 0.78, 1)
        _GroutColor   ("Grout Color",    Color)  = (0.30, 0.30, 0.30, 1)
        _Glossiness   ("Glossiness",     Float)  = 0.85
        _VariationStr ("Color Variation",Float)  = 0.05
    }

    SubShader
    {
        Cull Off
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // ── Uniforms ──────────────────────────────────────────────
            float  _TileSize;
            float  _GapSize;
            float4 _TileLight;
            float4 _TileDark;
            float4 _GroutColor;
            float  _Glossiness;
            float  _VariationStr;
            
            uniform float4x4 _ModelMatrix;
			uniform float4x4 _ViewMatrix;
			uniform float4x4 _ProjectionMatrix;

            // ── Structs ───────────────────────────────────────────────
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 clipPos  : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
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

            // ── Helpers ───────────────────────────────────────────────

            // Hash 2D → float, da un número estable por tileID
            float hash21(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5);
            }

            // Simula un reflejo especular simple (Blinn-Phong)
            float specular(float3 normal, float3 viewDir, float3 lightDir, float power)
            {
                float3 halfVec = normalize(viewDir + lightDir);
                return pow(max(dot(normal, halfVec), 0.0), power);
            }

            // ── Fragment ──────────────────────────────────────────────
            fixed4 frag(v2f i) : SV_Target
            {
                // ── 1. Coordenadas del mosaico ─────────────────────────
                float2 tileCoord = i.worldPos.xz / _TileSize;
                float2 tileID    = floor(tileCoord);
                float2 localPos  = frac(tileCoord);   // 0..1 dentro del mosaico

                // ── 2. Detectar junta (grout) ──────────────────────────
                float halfGap  = (_GapSize / _TileSize) * 0.5;
                float isGapX   = step(1.0 - halfGap * 2.0, localPos.x);
                float isGapZ   = step(1.0 - halfGap * 2.0, localPos.y);
                float isGap    = max(isGapX, isGapZ);

                // ── 3. Color base del mosaico con variación sutil ───────
                float  h         = hash21(tileID);
                float4 tileColor = lerp(_TileDark, _TileLight, h);

                // Variación de tono muy leve por mosaico (sin ser obvia)
                tileColor.rgb += (h - 0.5) * _VariationStr;

                // ── 4. Patrón de damero cada 2 mosaicos (opcional) ──────
                // Descomenta si querés alternar claro/oscuro tipo ajedrez
                // float checker = fmod(tileID.x + tileID.y, 2.0);
                // tileColor = lerp(_TileDark, _TileLight, checker);

                // ── 5. Bisel: sombra en los bordes del mosaico ─────────
                // Distancia al borde más cercano en X y Z
                float bevelX = min(localPos.x, 1.0 - localPos.x);
                float bevelZ = min(localPos.y, 1.0 - localPos.y);
                float bevel  = min(bevelX, bevelZ);

                // Sombra suave en el borde, brillo en el centro
                float bevelShadow = smoothstep(0.0, 0.06, bevel);
                tileColor.rgb *= lerp(0.65, 1.0, bevelShadow);

                // Brillo sutil en el centro (superficie convexa)
                float centerGlow = smoothstep(0.3, 0.5, bevel);
                tileColor.rgb += centerGlow * 0.04;

                // ── 6. Reflejo especular (simula cerámica brillante) ────
                float3 normal   = normalize(i.worldNormal);
                float3 viewDir  = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 lightDir = normalize(float3(1.0, 2.0, 1.0));   // luz fija diagonal

                float spec = specular(normal, viewDir, lightDir, 80.0) * _Glossiness;
                tileColor.rgb += spec * (1.0 - isGap);  // sin reflejo en la junta

                // ── 7. Junta con detalle de profundidad ─────────────────
                // Las juntas tienen una leve sombra interior
                float groutDepth = 1.0 - smoothstep(0.0, halfGap * 2.0,
                                    min(min(localPos.x, 1.0 - localPos.x),
                                        min(localPos.y, 1.0 - localPos.y)));
                float4 grout = _GroutColor * lerp(1.0, 0.6, groutDepth);

                // ── 8. Mezclar mosaico y junta ──────────────────────────
                float4 finalColor = lerp(tileColor, grout, isGap);
                finalColor.a = 1.0;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}