Shader "Hidden/Mask"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			sampler2D u_maskTex;
			half2 u_resolution;

			half rect_mask(half2 uv, half x, half y, half w, half h)
			{
				uv.x *= u_resolution.x / u_resolution.y;
				if (uv.x >= x && uv.x <= x + w
					&& uv.y >= y && uv.y <= y + h)
				{
					return 1.;
				}
				return 0.;
			}

			half circle_mask(half2 uv, half2 p, half r, half b)
			{
				uv -= .5;
				uv.x *= u_resolution.x / u_resolution.y;

				half d = distance(uv, p);

				if (d < r)
				{
					return smoothstep(r, r - b, d);
				}
				return 0.;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				/*
				half2 maskUv = i.uv;
				maskUv -= .5;
				maskUv.x *= u_resolution.x / u_resolution.y;
				maskUv += .5;
				maskUv.x = clamp(maskUv.x, 0., 1.);
				fixed4 mask = tex2D(u_maskTex, maskUv);
				*/


				return col * circle_mask(i.uv, half2(0., 0.), 0.3, 0.05);
			}
			ENDCG
		}
	}
}
