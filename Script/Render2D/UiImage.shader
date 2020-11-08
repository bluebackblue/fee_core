

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。ＵＩイメージ。
*/


Shader "Fee/Render2D/UiImage"
{
	Properties
	{
		_MainTex						("_MainTex",2D)			= "white"{}
		[MaterialToggle] clip_flag		("clip_flag",Int)		= 0
		clip_x1							("clip_x1",Float)		= 0
		clip_y1							("clip_y1",Float)		= 0
		clip_x2							("clip_x2",Float)		= 0
		clip_y2							("clip_y2",Float)		= 0
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}
		Pass
		{
			Cull Off
			ZWrite Off
			ZTest Always
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex		: POSITION;
				fixed4 color		: COLOR;
				float2 uv			: TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex		: SV_POSITION;
				fixed4 color		: COLOR;
				float2 uv			: TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;

			/** clip_flag
			*/
			int clip_flag;

			/** clip
			*/
			float clip_x1;
			float clip_y1;
			float clip_x2;
			float clip_y2;
			
			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.color = a_appdata.color;
					t_ret.uv = TRANSFORM_TEX(a_appdata.uv,_MainTex);
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				//クリップ。
				if(clip_flag > 0){
					if(clip_x1>a_v2f.vertex.x){
						discard;
					}

					if(a_v2f.vertex.x>clip_x2){
						discard;
					}

					float t_pos_y;
					#if(UNITY_UV_STARTS_AT_TOP)
					if(_ProjectionParams.x < 0){
						t_pos_y = _ScreenParams.y - a_v2f.vertex.y;
					}else{
						t_pos_y = a_v2f.vertex.y;
					}
					#else
					t_pos_y = _ScreenParams.y - a_v2f.vertex.y;
					#endif

					if(clip_y2 > t_pos_y){
						discard;
					}
					if(t_pos_y > clip_y1){
						discard;
					}
				}

				return tex2D(_MainTex,a_v2f.uv) * a_v2f.color;
			}
			ENDCG
		}
	}
}

