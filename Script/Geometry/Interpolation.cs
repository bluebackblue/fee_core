

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ジオメトリ。位置。
*/


/** Fee.Geometry
*/
namespace Fee.Geometry
{
	/** Interpolation
	*/
	public class Interpolation
	{
		/** 線形。
		*/
		public static UnityEngine.Color Linear(in UnityEngine.Color a_from,in UnityEngine.Color a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return UnityEngine.Color.Lerp(a_from,a_to,t_per);
		}

		/** 線形。
		*/
		public static float Linear(in float a_from,in float a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return UnityEngine.Mathf.Lerp(a_from,a_to,t_per);
		}

		/** 減速。
		*/
		public static float Deceleration(in float a_from,in float a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return UnityEngine.Mathf.Lerp(a_from,a_to,1.0f - (1.0f - t_per) * (1.0f - t_per));
		}

		/** 加速。
		*/
		public static float Acceleration(in float a_from,in float a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return UnityEngine.Mathf.Lerp(a_from,a_to,t_per * t_per);
		}

		/** 線形減速。
		*/
		public static float LinearDeceleration(in float a_from,in float a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return Linear(Linear(in a_from,in a_to,t_per),Deceleration(in a_from,in a_to,t_per),t_per);
		}

		/** 線形加速。
		*/
		public static float LinearAcceleration(in float a_from,in float a_to,float a_per)
		{
			float t_per = UnityEngine.Mathf.Clamp(a_per,0.0f,1.0f);

			return Linear(Linear(in a_from,in a_to,t_per),Acceleration(in a_from,in a_to,t_per),t_per);
		}
	}
}

