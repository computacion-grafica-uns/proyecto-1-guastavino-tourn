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

            v2f vert(appdata v)
            {
                v2f o;
                float4 worldPos4 = mul(_ModelMatrix, v.vertex);
                o.worldPos = worldPos4.xyz;

                o.clipPos = mul(_ProjectionMatrix, mul(_ViewMatrix, worldPos4));
                return o;
            }

            float hash21(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5);
            }

            float specular(float3 normal, float3 viewDir, float3 lightDir, float power)
            {
                float3 halfVec = normalize(viewDir + lightDir);
                return pow(max(dot(normal, halfVec), 0.0), power);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 tileCoord = i.worldPos.xz / _TileSize;
                float2 tileID    = floor(tileCoord);
                float2 localPos  = frac(tileCoord);   

                float halfGap  = (_GapSize / _TileSize) * 0.5;
                float isGapX   = step(1.0 - halfGap * 2.0, localPos.x);
                float isGapZ   = step(1.0 - halfGap * 2.0, localPos.y);
                float isGap    = max(isGapX, isGapZ);

                float  h         = hash21(tileID);
                float4 tileColor = lerp(_TileDark, _TileLight, h);

                tileColor.rgb += (h - 0.5) * _VariationStr;

                float bevelX = min(localPos.x, 1.0 - localPos.x);
                float bevelZ = min(localPos.y, 1.0 - localPos.y);
                float bevel  = min(bevelX, bevelZ);

                float bevelShadow = smoothstep(0.0, 0.06, bevel);
                tileColor.rgb *= lerp(0.65, 1.0, bevelShadow);

                float centerGlow = smoothstep(0.3, 0.5, bevel);
                tileColor.rgb += centerGlow * 0.04;

                float3 normal   = normalize(i.worldNormal);
                float3 viewDir  = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 lightDir = normalize(float3(1.0, 2.0, 1.0));   

                float groutDepth = 1.0 - smoothstep(0.0, halfGap * 2.0,
                                    min(min(localPos.x, 1.0 - localPos.x),
                                        min(localPos.y, 1.0 - localPos.y)));
                float4 grout = _GroutColor * lerp(1.0, 0.6, groutDepth);

                float4 finalColor = lerp(tileColor, grout, isGap);
                finalColor.a = 1.0;

                return finalColor;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}