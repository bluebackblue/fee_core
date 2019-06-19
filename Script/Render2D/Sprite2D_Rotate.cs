

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
		Fee.Render2D.Pos2D<int> center;

		/** 回転。
		*/
		UnityEngine.Quaternion quaternion;

		/** 初期化。
		*/
		public void Initialize()
		{
			//フラグ。
			this.flag = false;

			//中心。
			this.center.Set(0,0);

			//回転。
			this.quaternion = UnityEngine.Quaternion.identity;
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.flag = a_flag;
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
		*/
		public void SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			this.quaternion = UnityEngine.Quaternion.identity;

			if(a_euler_x != 0.0f){
				this.quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_x,new UnityEngine.Vector3(1.0f,0.0f,0.0f)) * this.quaternion;
			}
			if(a_euler_y != 0.0f){
				this.quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_y,new UnityEngine.Vector3(0.0f,1.0f,0.0f)) * this.quaternion;
			}
			if(a_euler_z != 0.0f){
				this.quaternion = UnityEngine.Quaternion.AngleAxis(a_euler_z,new UnityEngine.Vector3(0.0f,0.0f,1.0f)) * this.quaternion;
			}
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(ref UnityEngine.Quaternion a_quaternion)
		{
			this.quaternion = a_quaternion;
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.quaternion;
		}
	}
}

