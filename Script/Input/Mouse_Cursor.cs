

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。カーソル。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Mouse_Cursor
	*/
	public struct Mouse_Cursor
	{
		/** pos
		*/
		public Fee.Geometry.Pos2D<int> pos;

		/** xy_old
		*/
		public Fee.Geometry.Pos2D<int> pos_old;

		/** action
		*/
		public bool action;

		/** リセット。
		*/
		public void Reset()
		{
			//xy
			this.pos.Set(0,0);

			//xy_old
			this.pos_old.Set(0,0);

			//action
			this.action = false;
		}

		/** 設定。
		*/
		public void Set(int a_x,int a_y)
		{
			this.pos_old = this.pos;
			this.pos.Set(a_x,a_y);
		}

		/** Ｘ。取得。
		*/
		public int GetX()
		{
			return this.pos.x;
		}

		/** Ｙ。取得。
		*/
		public int GetY()
		{
			return this.pos.y;
		}

		/** Ｘ。取得。
		*/
		public int GetOldX()
		{
			return this.pos_old.x;
		}

		/** Ｙ。取得。
		*/
		public int GetOldY()
		{
			return this.pos_old.y;
		}

		/** 更新。
		*/
		public void Main()
		{
			if((this.pos.x != this.pos_old.x)||(this.pos.y != this.pos_old.y)){
				this.action = true;
			}else{
				this.action = false;
			}
		}
	}
}

