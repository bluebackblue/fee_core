/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/brownie/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダー。ＵＩテキスト。
*/


Shader "Render2D/UiText"
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
				float4 vertex : SV_POSITION;
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
			v2f vert(appdata v)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(v.vertex);
					t_ret.color = v.color * _Color;
					t_ret.uv = TRANSFORM_TEX(v.uv,_MainTex);
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				if(clip_flag > 0){
					if(clip_x1>i.vertex.x){
						discard;
					}

					if(i.vertex.x>clip_x2){
						discard;
					}

					if(clip_y1>i.vertex.y){
						discard;
					}

					if(i.vertex.y>clip_y2){
						discard;
					}
				}

				fixed4 t_color = i.color;

				t_color.a = saturate(t_color.a * UNITY_SAMPLE_1CHANNEL(_MainTex,i.uv) * 1.3f);

				return t_color;
			}
			ENDCG
		}
	}
}

