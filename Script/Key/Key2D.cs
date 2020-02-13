

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief キー。
*/


/** Fee.Key
*/
namespace Fee.Key
{
	/** Key2D
	*/
	public readonly struct Key2D : System.IEquatable<Key2D>
	{
		/** x
		*/
		public readonly int x;

		/** y
		*/
		public readonly int y;

		/** constructor
		*/
		public Key2D(int a_x,int a_y)
		{
			this.x = a_x;
			this.y = a_y;
		}

		/** GetHashCode
		*/
		public override int GetHashCode()
		{
			return this.x + this.y * 1024;
		}

		/** Equals
		*/
		public override bool Equals(object a_other)
		{
			if(a_other == null){
				return false;
			}

			if(this.GetType() != a_other.GetType()){
				return false;
			}

			{
				Key2D t_other = (Key2D)a_other;
				if((this.x == t_other.x)&&(this.y == t_other.y)){
					return true;
				}
			}

			return false;
		}

		/** [System.IEquatable]Equals
		*/
		public bool Equals(Key2D a_other)
		{
			if((this.x == a_other.x)&&(this.y == a_other.y)){
				return true;
			}

			return false;
		}

		/** operator !=
		*/
		public static bool operator !=(Key2D a_left,Key2D a_right)
		{
			if((a_left.x != a_right.x)||(a_left.y != a_right.y)){
				return true;
			}
			return false;
		}

		/** operator ==
		*/
		public static bool operator ==(Key2D a_left,Key2D a_right)
		{
			if((a_left.x == a_right.x)&&(a_left.y == a_right.y)){
				return true;
			}
			return false;
		}

		/** 範囲内チェック。
		*/
		public bool IsRectIn(int a_x_min,int a_x_max,int a_y_min,int a_y_max)
		{
			if((a_x_min<=this.x)&&(this.x<=a_x_max)&&(a_y_min<=this.y)&&(this.y<=a_y_max)){
				return true;
			}
			return false;
		}
	}
}

