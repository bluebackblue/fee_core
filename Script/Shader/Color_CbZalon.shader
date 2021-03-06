

/**  @brief シェーダー。
*/


Shader "Fee/Shader/Color_CbZalon"
{
	Properties
	{
		_Color("_Color",Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		Pass
		{
			Cull Back
			ZTest Always
			ZWrite On

			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
			};

			/** _Color
			*/
			fixed4 _Color;
			
			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				return _Color;
			}

			ENDCG
		}
	}
}

