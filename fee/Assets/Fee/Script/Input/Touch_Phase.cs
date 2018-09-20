using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。タッチ。フェイズ。
*/


/** NInput
*/
namespace NInput
{
	/** Touch_Phase
	*/
	public class Touch_Phase
	{
		/** value_x
		*/
		public int value_x;

		/** value_y
		*/
		public int value_y;

		/** 更新。
		*/
		public bool update;

		/** fadeoutframe
		*/
		public int fadeoutframe;

		/** pressure
		*/
		public float pressure;

		/** radius
		*/
		public float radius;

		/** angle_altitude
		*/
		public float angle_altitude;

		/** angle_azimuth
		*/
		public float angle_azimuth;

		/** リセット。
		*/
		public void Reset()
		{
			//update
			this.update = false;

			//value_x
			this.value_x = 0;
	
			//value_y
			this.value_y = 0;

			//fadeoutframe
			this.fadeoutframe = 0;

			//pressure
			this.pressure = 0.0f;

			//radius
			this.radius = 0.0f;

			//angle_altitude
			this.angle_altitude = 0.0f;

			//angle_azimuth
			this.angle_azimuth = 0.0f;
		}

		/** 設定。
		*/
		public void Set(int a_value_x,int a_value_y,float a_pressure,float a_radius,float a_angle_altitude,float a_angle_azimuth)
		{
			//value_x
			this.value_x = a_value_x;

			//value_y
			this.value_y = a_value_y;

			//pressure
			this.pressure = a_pressure;

			//radius
			this.radius = a_radius;

			//angle_altitude
			this.angle_altitude = a_angle_altitude;

			//angle_azimuth
			this.angle_azimuth = a_angle_azimuth;
		}

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

