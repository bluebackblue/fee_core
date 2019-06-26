

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。ワーク。
*/


/** Fee.AssetBundle
*/
namespace Fee.AssetBundle
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

			/** Do_File
			*/
			Do_File,

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

			/** ファイル。
			*/
			File,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_id
		*/
		private string request_id;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public Work()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_id
			this.request_id = null;

			//item
			this.item = new Item();
		}

		/** リクエスト。
		*/
		public void RequestFile(string a_id)
		{
			this.request_type = RequestType.File;
			this.request_id = a_id;
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
					case RequestType.File:
						{
							if(Fee.AssetBundle.AssetBundle.GetInstance().GetMainFile().RequestFile(this.request_id) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_File:
				{
					Main_File t_main = Fee.AssetBundle.AssetBundle.GetInstance().GetMainFile();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_File.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_File.ResultType.AssetBundle:
							{
								if(t_main.GetResultAssetBundle() != null){
									this.item.SetResultAssetBundle(t_main.GetResultAssetBundle());
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

