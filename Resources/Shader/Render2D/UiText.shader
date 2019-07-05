/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief シェーダ。ＵＩテキスト。
*/


Shader "Fee/Render2D/UiText"
{
	Properties
	{
		/** _MainTex
		*/
		_MainTex ("Texture", 2D) = "white" {}

		/** _Color
		*/
		_Color ("Text Color", Color) = (1,1,1,1)

		/** clip
		*/
		[MaterialToggle] clip_flag ("Clip Flag", Int) = 0
		clip_x1 ("Clip X1", Float) = 0
		clip_y1 ("Clip Y1", Float) = 0
		clip_x2 ("Clip X2", Float) = 0
		clip_y2 ("Clip Y2", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}

		Cull Off
		ZWrite Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;

			/** _Color
			*/
			uniform fixed4 _Color;

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
					t_ret.pos = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.color = a_appdata.color * _Color;
					t_ret.uv = TRANSFORM_TEX(a_appdata.uv,_MainTex);
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				if(clip_flag > 0){
					if(clip_x1>a_v2f.pos.x){
						discard;
					}

					if(a_v2f.pos.x>clip_x2){
						discard;
					}

					float t_pos_y;
					#if(UNITY_UV_STARTS_AT_TOP)
					if(_ProjectionParams.x < 0){
						t_pos_y = _ScreenParams.y - a_v2f.pos.y;
					}else{
						t_pos_y = a_v2f.pos.y;
					}
					#else
					t_pos_y = _ScreenParams.y - a_v2f.pos.y;
					#endif

					if(clip_y2 > t_pos_y){
						discard;
					}
					if(t_pos_y > clip_y1){
						discard;
					}
				}

				fixed t_alpha = saturate(a_v2f.color.a * UNITY_SAMPLE_1CHANNEL(_MainTex,a_v2f.uv) * 1.3f);

				return fixed4(a_v2f.color.rgb,t_alpha);
			}
			ENDCG
		}
	}
}

