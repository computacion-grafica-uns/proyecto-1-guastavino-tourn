Shader "SimpleShaderTexture"
{
	Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // ← esto faltaba
    }
	SubShader
	{
		Pass 
		{
			Cull off

			CGPROGRAM

			#pragma vertex vert
			#pragma  fragment frag

			struct appdata {
				float4 vertex : POSITION;
				float2 uv     : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv  : TEXCOORD0;
			};

			sampler2D _MainTex;
			uniform float4x4 _ModelMatrix;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul( mul (UNITY_MATRIX_P, mul (UNITY_MATRIX_V, _ModelMatrix)), v.vertex);
				o.uv  = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }

			ENDCG
		}
	}
}