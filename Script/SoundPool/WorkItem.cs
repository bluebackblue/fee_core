

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。ワークアイテム。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** WorkItem
	*/
	public class WorkItem
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

			/** ロードローカル。サウンドプール。
			*/
			LoadLocalSoundPool,

			/** ロードストリーミングアセット。サウンドプール。
			*/
			LoadStreamingAssetsSoundPool,

			/** ロードＵＲＬ。サウンドプール。
			*/
			LoadUrlSoundPool,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_path
		*/
		private File.Path request_path;

		/** request_post_data
		*/
		private UnityEngine.WWWForm request_post_data;

		/** request_certificate_handler
		*/
		private UnityEngine.Networking.CertificateHandler request_certificate_handler;

		/** request_data_version
		*/
		private uint request_data_version;

		/** item
		*/
		private Item item;

		/** constructor
		*/
		public WorkItem()
		{
			//mode
			this.mode = Mode.Start;

			//request_type
			this.request_type = RequestType.None;

			//request_path
			this.request_path = null;

			//request_post_data
			this.request_post_data = null;

			//request_certificate_handler
			this.request_certificate_handler = null;

			//request_data_version
			this.request_data_version = 0;

			//item
			this.item = new Item();
		}

		/** リクエスト。ロードローカル。サウンドプール。
		*/
		public void RequestLoadLocalSoundPool(File.Path a_relative_path)
		{
			this.request_type = RequestType.LoadLocalSoundPool;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードストリーミングアセット。サウンドプール。
		*/
		public void RequestLoadStreamingAssetsSoundPool(File.Path a_relative_path,uint a_data_version)
		{
			this.request_type = RequestType.LoadStreamingAssetsSoundPool;
			this.request_path = a_relative_path;
			this.request_data_version = a_data_version;
		}

		/** リクエスト。ロードＵＲＬ。サウンドプール。
		*/
		public void RequestLoadUrlBinaryFile(File.Path a_url_path,UnityEngine.WWWForm a_post_data,UnityEngine.Networking.CertificateHandler a_certificate_handler,uint a_data_version)
		{
			this.request_type = RequestType.LoadUrlSoundPool;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
			this.request_certificate_handler = a_certificate_handler;
			this.request_data_version = a_data_version;
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
					case RequestType.LoadLocalSoundPool:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadLocalSoundPool(this.request_path) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					case RequestType.LoadStreamingAssetsSoundPool:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadStreamingAssetsSoundPool(this.request_path,this.request_data_version) == true){
								this.mode = Mode.Do_File;
							}
						}break;
					case RequestType.LoadUrlSoundPool:
						{
							if(Fee.SoundPool.SoundPool.GetInstance().GetMainFile().RequestLoadUrlSoundPool(this.request_path,this.request_post_data,this.request_certificate_handler,this.request_data_version) == true){
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
					Main_File t_main = Fee.SoundPool.SoundPool.GetInstance().GetMainFile();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_File.ResultType.None){ 
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_File.ResultType.SoundPool:
							{
								if(t_main.GetResultSoundPool() != null){
									this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
									this.item.SetResultSoundPool(t_main.GetResultSoundPool());
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

