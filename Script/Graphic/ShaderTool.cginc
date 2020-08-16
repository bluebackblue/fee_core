

/**  @brief グラフィック。ツール。
*/


#if !defined(DEF_FEE_GRAPHIC_TOOL_CGINC)
#define DEF_FEE_GRAPHIC_TOOL_CGINC


/** Fee_Linear01Depth
*/
inline float Fee_Linear01Depth(in sampler2D a_depth_tex,in float2 a_uv)
{
	float t_depth = UNITY_SAMPLE_DEPTH(tex2D(a_depth_tex,a_uv));
	t_depth = Linear01Depth(t_depth);
	return t_depth;
}



/** Fee_DepthToWorldPos
*/
inline float4 Fee_DepthToWorldPos(in sampler2D a_depth_tex,in float2 a_uv,in float2 a_screen_pos,in float4x4 a_matrix_inv)
{
	float t_depth = UNITY_SAMPLE_DEPTH(tex2D(a_depth_tex,a_uv));
	float t_depth_01 = Linear01Depth(t_depth);

	//スクリーン座標。
	float4 t_now_pos = float4(a_screen_pos.xy,t_depth,1);

	//-1 -- +1
	float4 t_now_pos_m1p1 = float4(t_now_pos.x * 2 / _ScreenParams.x - 1,1 - t_now_pos.y * 2 / _ScreenParams.y,t_now_pos.z,t_now_pos.w);

	//ワールド座標。
	float4 t_world_pos = mul(a_matrix_inv,t_now_pos_m1p1);
	t_world_pos /= t_world_pos.w;

	//深度。
	t_world_pos.w = t_depth_01;

	return t_world_pos;
}

#endif

