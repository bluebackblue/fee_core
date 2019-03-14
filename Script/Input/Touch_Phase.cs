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
		/** PhaseType
		*/
		public enum PhaseType
		{
			/** 
			*/
			None,

			/** 
			*/
			Began,

			/** 
			*/
			Moved,

			/** 
			*/
			Stationary,
		};

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

		/** phasetype
		*/
		public PhaseType phasetype;

		/** raw_id
		*/
		public int raw_id;

		/** pressure
		*/
		/*
		public float pressure;
		*/

		/** radius
		*/
		/*
		public float radius;
		*/

		/** angle_altitude
		*/
		/*
		public float angle_altitude;
		*/

		/** angle_azimuth
		*/
		/*
		public float angle_azimuth;
		*/

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

			//phasetype
			this.phasetype = PhaseType.None;

			//raw_id
			this.raw_id = 0;

			//pressure
			/*
			this.pressure = 0.0f;
			*/

			//radius
			/*
			this.radius = 0.0f;
			*/

			//angle_altitude
			/*
			this.angle_altitude = 0.0f;
			*/

			//angle_azimuth
			/*
			this.angle_azimuth = 0.0f;
			*/
		}

		/** 設定。
		*/
		public void Set(int a_value_x,int a_value_y,PhaseType a_phasetype)
		{
			//value_x
			this.value_x = a_value_x;

			//value_y
			this.value_y = a_value_y;

			//phasetype
			this.phasetype = a_phasetype;
		}

		/** RawID。
		*/
		public void SetRawID(int a_raw_id)
		{
			this.raw_id = a_raw_id;
		}

		/** 圧力。
		*/
		/*
		public void SetPressure(float a_pressure)
		{
			//pressure
			this.pressure = a_pressure;
		}
		*/

		/** 半径。
		*/
		/*
		public void SetRadius(float a_radius)
		{
			//radius
			this.radius = a_radius;
		}
		*/

		/** 角度。
		*/
		/*
		public void SetAngle(float a_angle_altitude,float a_angle_azimuth)
		{
			//angle_altitude
			this.angle_altitude = a_angle_altitude;

			//angle_azimuth
			this.angle_azimuth = a_angle_azimuth;
		}
		*/

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

