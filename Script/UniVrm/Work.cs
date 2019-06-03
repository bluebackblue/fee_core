

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。ワーク。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
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

		/** RequestType
		*/
		private enum RequestType
		{
			/** None
			*/
			None,

			/** ロードＶＲＭ。
			*/
			LoadVrm,
		}

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** item
		*/
		private Item item;

		/** request_binary
		*/
		private byte[] request_binary;

		/** constructor
		*/
		public Work()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//item
			this.item = new Item();

			//request_binary
			this.request_binary = null;
		}

		/** リクエスト。ロードＶＲＭ
		*/
		public void RequestLoadVrm(byte[] a_binary)
		{
			this.request_type = RequestType.LoadVrm;
			this.request_binary = a_binary;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** 更新。

			return == true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					switch(this.request_type){
					case RequestType.LoadVrm:
						{
							if(Fee.UniVrm.UniVrm.GetInstance().GetMainVrm().RequestLoadVrm(this.request_binary) == true){
								this.mode = Mode.Do;
							}
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do:
				{
					Main_Vrm t_main = Fee.UniVrm.UniVrm.GetInstance().GetMainVrm();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_Vrm.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Vrm.ResultType.Context:
							{
								if(t_main.GetResultContext() != null){
									this.item.SetResultContext(t_main.GetResultContext());
									t_success = true;
								}
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
						}

						//完了。
						t_main.Fix();

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_main.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

