


/** STRUCT_DEF
*/
#define STRUCT_DEF				\
								\
/** appdata						\
*/								\
struct appdata					\
{								\
	float4 vertex : POSITION;	\
	fixed4 color : COLOR;		\
};								\
								\
/** v2g							\
*/								\
struct v2g						\
{								\
	float4 pos : SV_POSITION;	\
	fixed4 color : COLOR;		\
};								\
								\
/** g2f							\
*/								\
struct g2f						\
{								\
	float4 pos : SV_POSITION;	\
	float4 color : COLOR;		\
	float3 abc : TEXCOORD0;		\
};								 \
\


/** VERT_PROC
*/
#define VERT_PROC(a_appdata){								\
	v2g t_ret;												\
	{														\
		t_ret.pos = UnityObjectToClipPos(a_appdata.vertex);	\
		t_ret.color = a_appdata.color;						\
	}														\
	return t_ret;											\
}															\
\



/** GEO_PROC
*/
#define GEO_PROC(a_v2g_list,a_out_list){	\
	{										\
		g2f t_ret;							\
		t_ret.pos = a_v2g_list[0].pos;		\
		t_ret.color = a_v2g_list[0].color;	\
		t_ret.abc = float3(1.0f,0.0f,0.0f);	\
		a_out_list.Append(t_ret);			\
	}										\
											\
	{										\
		g2f t_ret;							\
		t_ret.pos = a_v2g_list[1].pos;		\
		t_ret.color = a_v2g_list[1].color;	\
		t_ret.abc = float3(0.0f,1.0f,0.0f);	\
		a_out_list.Append(t_ret);			\
	}										\
											\
	{										\
		g2f t_ret;							\
		t_ret.pos = a_v2g_list[2].pos;		\
		t_ret.color = a_v2g_list[2].color;	\
		t_ret.abc = float3(0.0f,0.0f,1.0f);	\
		a_out_list.Append(t_ret);			\
	}										\
}											\
\



/** FRAG_PROC
*/
#define FRAG_PROC(a_g2f){															\
	float t_limit = 0.02f;															\
	if((a_g2f.abc.x < t_limit)||(a_g2f.abc.y < t_limit)||(a_g2f.abc.z < t_limit)){	\
	}else{																			\
		discard;																	\
	}																				\
	return a_g2f.color;																\
}																					\
\


