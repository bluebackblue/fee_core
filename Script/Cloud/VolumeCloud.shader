

/**  @brief シェーダー。
*/


Shader "Fee/Cloud/VolumeCloud"
{
	Properties
	{
		_Color("_Color",Color) = (1.0,1.0,1.0,1.0)
		power("power",Range(0.0,50.0)) = 1.0
		noisescale("noisescale",Range(0.0,100.0)) = 10.0
		inv_scale("inv_scale",Range(0.0,10.0)) = 0.6
	}
	SubShader
	{
		Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Transparent" 
		}
		Pass
		{
			Cull Back
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha 

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "../Shader/Noise.cginc"

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
				float3 world_pos		: TEXCOORD1;
			};

			/** _Color
			*/
			float4 _Color;

			/** power
			*/
			float power;

			/** noisescale
			*/
			float noisescale;

			/** inv_scale
			*/
			float inv_scale;

			/** noiseoffset
			*/
			float3 noiseoffset;

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(a_appdata.vertex);
					t_ret.world_pos = mul(unity_ObjectToWorld,a_appdata.vertex);
				}
				return t_ret;
			}

			/** Noise
			*/
			inline float Noise(float3 a_pos)
			{
				float3 t_value_f = frac(a_pos);
				t_value_f = t_value_f * t_value_f * (3.0f - t_value_f * 2.0f);

				float3 t_value_i3 = floor(a_pos);
				float t_value_i = t_value_i3.x + t_value_i3.y * 57.0f + t_value_i3.z * 113.0f;
				float res = lerp(
					lerp(
						lerp(
							HashF(t_value_i + 0.0f),
							HashF(t_value_i + 1.0f),
							t_value_f.x
						),
						lerp(
							HashF(t_value_i + 57.0f),
							HashF(t_value_i + 58.0f),
							t_value_f.x
						),
						t_value_f.y
					),
					lerp(
						lerp(
							HashF(t_value_i + 113.0f),
							HashF(t_value_i + 114.0f),
							t_value_f.x
						),
						lerp(
							HashF(t_value_i + 170.0f),
							HashF(t_value_i + 171.0f),
							t_value_f.x
						),
						t_value_f.y
					),
					t_value_f.z
				);

				return res;
			}

			/** Fbm
			*/
			inline float Fbm(float3 a_pos)
			{
				float t_value = 0.0f;
				{
					t_value += 0.5f * Noise(a_pos);
					t_value += 0.3f * Noise(a_pos * 1.72f);
					t_value += 0.2f * Noise(a_pos * 3.47f);
				}
				return t_value;
			}

			/** Calc
			*/
			inline float Calc(float3 a_local_pos)
			{
				return saturate(	(Fbm(a_local_pos * noisescale + noiseoffset) - length(a_local_pos) * inv_scale) * power);
			}

			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				
				int DEF_LOOP = 32;

				//頂点のローカル位置。
				float3 t_local_pos = mul(unity_WorldToObject,float4(a_v2f.world_pos,1.0));

				//カメラから見た奥方向。
				float3 t_local_dir = UnityWorldToObjectDir(normalize(a_v2f.world_pos - _WorldSpaceCameraPos));

				//合成。
				fixed t_alpha = 0.0f;
				float3 t_step = t_local_dir * (1.0 / DEF_LOOP);
				for(int ii=0;ii<DEF_LOOP;++ii){
					t_alpha += Calc(t_local_pos) * (1.0f - t_alpha);
					t_local_pos += t_step;
				}

				return fixed4(_Color.rgb,_Color.a * t_alpha);
			}

			ENDCG
		}
	}
}

