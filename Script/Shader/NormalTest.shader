

/**  @brief シェーダー。
*/


Shader "Fee/Shader/NormalTest"
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
			ZTest LEqual
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
				float3  normal		: NORMAL;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				float3  normal		: NORMAL;
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
					t_ret.normal = a_appdata.normal;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				float3 t_normal = normalize(abs(a_v2f.normal));
				return fixed4(t_normal.x,t_normal.y,t_normal.z,1.0f);
			}

			ENDCG
		}
	}
}

