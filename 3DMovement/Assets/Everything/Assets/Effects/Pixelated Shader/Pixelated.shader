Shader "Hidden/Pixelated"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_ScreenWidth("screen width", float) = 320.0
		_ScreenHeight("screen height", float) = 240.0
		_CellSizeX("size of x cell", float) = 4.0
		_CellSizeY("size of y cell", float) = 4.0
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _ScreenWidth;
			float _ScreenHeight;
			float _CellSizeX;
			float _CellSizeY;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv;

				float pixelX = _ScreenWidth / _CellSizeX;
				float pixelY = _ScreenHeight / _CellSizeY;

				return tex2D(_MainTex, float2(floor(pixelX * uv.x) / pixelX, floor(pixelY * uv.y) / pixelY));
			}
			ENDCG
		}
	}
}
