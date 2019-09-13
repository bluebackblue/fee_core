

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スプライト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Sprite2D_Rotate
	*/
	public struct Sprite2D_Rotate
	{
		/** フラグ。
		*/
		bool flag;
	
		/** 中心。
		*/
		Fee.Geometry.Pos2D<int> center;

		/** 回転。
		*/
		UnityEngine.Quaternion quaternion;

		/** プールから作成。
		*/
		public void PoolNew()
		{
			//フラグ。
			this.flag = false;

			//中心。
			this.center.Set(0,0);

			//回転。
			this.quaternion = UnityEngine.Quaternion.identity;
		}

		/** 回転。設定。

			return == true : 変更あり。

		*/
		public bool SetRotate(bool a_flag)
		{
			bool t_change = false;

			if(this.flag != a_flag){
				this.flag = a_flag;
				t_change = true;
			}

			return t_change;
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.flag;
		}

		/** 中心。設定。
		*/
		public void SetCenterX(int a_center_x)
		{
			this.center.x = a_center_x;
		}

		/** 中心。設定。
		*/
		public void SetCenterY(int a_center_y)
		{
			this.center.y = a_center_y;
		}

		/** 中心。取得。
		*/
		public int GetCenterX()
		{
			return this.center.x;
		}

		/** 中心。取得。
		*/
		public int GetCenterY()
		{
			return this.center.y;
		}

		/** クォータニオン。設定。

			return == true : 変更あり。

		*/
		public bool SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			bool t_change = false;

			UnityEngine.Quaternion t_quaternion = UnityEngine.Quaternion.identity;

			if(a_euler_x != 0.0f){
				t_quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_x,new UnityEngine.Vector3(1.0f,0.0f,0.0f)) * t_quaternion;
			}
			if(a_euler_y != 0.0f){
				t_quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_y,new UnityEngine.Vector3(0.0f,1.0f,0.0f)) * t_quaternion;
			}
			if(a_euler_z != 0.0f){
				t_quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_z,new UnityEngine.Vector3(0.0f,0.0f,1.0f)) * t_quaternion;
			}

			if(this.quaternion != t_quaternion){
				this.quaternion = t_quaternion;
				t_change = true;
			}

			return t_change;
		}

		/** クォータニオン。設定。

			return == true : 変更あり。

		*/
		public bool SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			bool t_change = false;

			if(this.quaternion != a_quaternion){
				this.quaternion = a_quaternion;
				t_change = true;
			}

			return t_change;
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.quaternion;
		}
	}
}

