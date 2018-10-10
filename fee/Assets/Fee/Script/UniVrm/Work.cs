using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。ワーク。
*/


/** NUniVrm
*/
namespace NUniVrm
{
	/** Work
	*/
	public class Work
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 開始。
			*/
			Start,

			/** 実行。
			*/
			Do,

			/** 完了。
			*/
			End
		};

		/** mode
		*/
		private Mode mode;

		/** item
		*/
		private Item item;

		/** binary
		*/
		private byte[] binary;

		/** constructor
		*/
		public Work()
		{
			//mode
			this.mode = Mode.Start;

			//item
			this.item = new Item();

			//binary
			this.binary = null;
		}

		/** リクエスト。
		*/
		public void Request(byte[] a_binary)
		{
			this.binary = a_binary;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					MonoBehaviour_Vrm t_vrm = NUniVrm.UniVrm.GetInstance().GetVrm();

					if(t_vrm.RequestLoad(this.binary) == true){
						this.mode = Mode.Do;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do:
				{
					MonoBehaviour_Vrm t_vrm = NUniVrm.UniVrm.GetInstance().GetVrm();

					this.item.SetResultProgress(t_vrm.GetResultProgress());

					if(t_vrm.IsFix() == true){
						//結果。

						if(t_vrm.GetResultType() == MonoBehaviour_Base.ResultType.Context){
							this.item.SetResultContext(t_vrm.GetResultContext());
						}else{
							this.item.SetResultErrorString(t_vrm.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_vrm.WaitRequest();

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_vrm.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

