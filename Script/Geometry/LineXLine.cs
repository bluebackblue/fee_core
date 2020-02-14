

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ジオメトリ。
*/


/** Fee.Geometry
*/
namespace Fee.Geometry
{
	/** 線と線。

		point_1 = a_line_1_start + (a_line_1_end - a_line_1_start) * t_r
		point_2 = a_line_2_start + (a_line_2_end - a_line_2_start) * t_s

		(point_1 == point_2)となる(t_r)と(t_s)を求める。

		(a_line_1_start.x + (a_line_1_end.x - a_line_1_start.x) * t_r == a_line_2_start.x + (a_line_2_end.x - a_line_2_start.x) * t_s)
		(a_line_1_start.y + (a_line_1_end.y - a_line_1_start.x) * t_r == a_line_2_start.y + (a_line_2_end.y - a_line_2_start.y) * t_s)

		t_outerが流用可能。

	*/
	public class LineXLine
	{
		/** Check
		*/
		public static bool Check(float a_line_1_start_x,float a_line_1_start_y,float a_line_1_end_x,float a_line_1_end_y,float a_line_2_start_x,float a_line_2_start_y,float a_line_2_end_x,float a_line_2_end_y,out float a_point_x,out float a_point_y)
		{
			float t_outer = (a_line_1_end_x - a_line_1_start_x) * (a_line_2_end_y - a_line_2_start_y) - (a_line_1_end_y - a_line_1_start_y) * (a_line_2_end_x - a_line_2_start_x);
			if(t_outer == 0.0f){
				//平行。
				a_point_x = 0.0f;
				a_point_y = 0.0f;
				return false;
			}

			float t_r = ((a_line_2_end_y - a_line_2_start_y) * (a_line_2_start_x - a_line_1_start_x) - (a_line_2_end_x - a_line_2_start_x) * (a_line_2_start_y - a_line_1_start_y)) / t_outer;
			float t_s = ((a_line_1_end_y - a_line_1_start_y) * (a_line_2_start_x - a_line_1_start_x) - (a_line_1_end_x - a_line_1_start_x) * (a_line_2_start_y - a_line_1_start_y)) / t_outer;

			//交点。
			a_point_x = a_line_1_start_x + (a_line_1_end_x - a_line_1_start_x) * t_r;
			a_point_y = a_line_1_start_y + (a_line_1_end_y - a_line_1_start_y) * t_r;

			if((0.0f <= t_r)&&(t_r <= 1.0f)&&(0.0f <= t_s)&&(t_s <= 1.0f)){
				//両方の線分の上にある。
				return true;
			}

			//両方の線分の上にはない。
			return false;
		}
	}
}

