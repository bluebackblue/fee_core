

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。ワーク。
*/


/** Fee.File
*/
namespace Fee.File
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

			/** Do_Io
			*/
			Do_Io,

			/** Do_WebRequest
			*/
			Do_WebRequest,

			/** Do_Resources
			*/
			Do_Resources,

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

			/** ロードローカル。バイナリファイル。
			*/
			LoadLocalBinaryFile,

			/** ロードローカル。テキストファイル。
			*/
			LoadLocalTextFile,

			/** ロードローカル。テクスチャーファイル。
			*/
			LoadLocalTextureFile,

			/** セーブローカル。バイナリファイル。
			*/
			SaveLocalBinaryFile,

			/** セーブローカル。テキストファイル。
			*/
			SaveLocalTextFile,

			/** セーブローカル。テクスチャーファイル。
			*/
			SaveLocalTextureFile,

			/** ダウンロード。バイナリファイル。
			*/
			DownLoadBinaryFile,

			/** ダウンロード。テキストファイル。
			*/
			DownLoadTextFile,
	
			/** ダウンロード。テクスチャーファイル。
			*/
			DownLoadTextureFile,

			/** ダウンロード。アセットバンドル。
			*/
			DownLoadAssetBundle,

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,

			/** ロードストリーミングアセット。テキストファイル。
			*/
			LoadStreamingAssetsTextFile,

			/** ロードストリーミングアセット。テクスチャーファイル。
			*/
			LoadStreamingAssetsTextureFile,

			/** リソース。なんでもファイル。
			*/
			LoadResourcesAnythingFile,

			/** リソース。テキストファイル。
			*/
			LoadResourcesTextFile,

			/** リソース。テクスチャーファイル。
			*/
			LoadResourcesTextureFile,

			/** リソース。プレハブファイル。
			*/
			LoadResourcesPrefabFile,

		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_path
		*/
		private Path request_path;

		/** request_post_data
		*/
		private UnityEngine.WWWForm request_post_data;

		/** request_assetbundle_id
		*/
		private long request_assetbundle_id;

		/** request_data_version
		*/
		private uint request_data_version;

		/** request_binary
		*/
		private byte[] request_binary;

		/** request_text
		*/
		private string request_text;

		/** request_texture
		*/
		private UnityEngine.Texture2D request_texture;

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

			//request_path
			this.request_path = null;

			//request_post_data
			this.request_post_data = null;

			//request_assetbundle_id
			this.request_assetbundle_id = 0;

			//request_data_version
			this.request_data_version = 0;

			//request_binary
			this.request_binary = null;

			//request_text
			this.request_text = null;

			//request_texture
			this.request_texture = null;

			//item
			this.item = new Item();
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public void RequestLoadLocalBinaryFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadLocalBinaryFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public void RequestLoadLocalTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadLocalTextFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードローカル。テクスチャーファイル。
		*/
		public void RequestLoadLocalTextureFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadLocalTextureFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public void RequestSaveLocalBinaryFile(Path a_relative_path,byte[] a_binary)
		{
			this.request_type = RequestType.SaveLocalBinaryFile;
			this.request_path = a_relative_path;
			this.request_binary = a_binary;
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public void RequestSaveLocalTextFile(Path a_relative_path,string a_text)
		{
			this.request_type = RequestType.SaveLocalTextFile;
			this.request_path = a_relative_path;
			this.request_text = a_text;
		}

		/** リクエスト。セーブローカル。テクスチャーファイル。
		*/
		public void RequestSaveLocalTextureFile(Path a_relative_path,UnityEngine.Texture2D a_texture)
		{
			this.request_type = RequestType.SaveLocalTextureFile;
			this.request_path = a_relative_path;
			this.request_texture = a_texture;
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public void RequestDownLoadBinaryFile(Path a_relative_path,UnityEngine.WWWForm a_post_data)
		{
			this.request_type = RequestType.DownLoadBinaryFile;
			this.request_path = a_relative_path;
			this.request_post_data = a_post_data;
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public void RequestDownLoadTextFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			this.request_type = RequestType.DownLoadTextFile;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
		}

		/** リクエスト。ダウンロード。テクスチャーファイル。
		*/
		public void RequestDownLoadTextureFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			this.request_type = RequestType.DownLoadTextureFile;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
		}

		/** リクエスト。ダウンロード。アセットバンドル。
		*/
		public void RequestDownLoadAssetBundle(Path a_url_path,long a_assetbundle_id,uint a_data_version)
		{
			this.request_type = RequestType.DownLoadAssetBundle;
			this.request_path = a_url_path;
			this.request_assetbundle_id = a_assetbundle_id;
			this.request_data_version = a_data_version;
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public void RequestLoadStreamingAssetsBinaryFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードストリーミングアセット。テキストファイル。
		*/
		public void RequestLoadStreamingAssetsTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadStreamingAssetsTextFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードストリーミングアセット。テクスチャーファイル。
		*/
		public void RequestLoadStreamingAssetsTextureFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadStreamingAssetsTextureFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト、ロードリソース。なんでもファイル。
		*/
		public void RequestLoadResourcesAnythingFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadResourcesAnythingFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト、ロードリソース。テキストファイル。
		*/
		public void RequestLoadResourcesTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadResourcesTextFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードリソース。テクスチャーファイル。
		*/
		public void RequestLoadResourcesTextureFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadResourcesTextureFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードリソース。プレハブファイル。
		*/
		public void RequestLoadResourcesPrefabFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadResourcesPrefabFile;
			this.request_path = a_relative_path;
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
					case RequestType.LoadLocalBinaryFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestLoadLocalBinaryFile(this.request_path) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.LoadLocalTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestLoadLocalTextFile(this.request_path) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.LoadLocalTextureFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestLoadLocalTextureFile(this.request_path) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalBinaryFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestSaveLocalBinaryFile(this.request_path,this.request_binary) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestSaveLocalTextFile(this.request_path,this.request_text) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalTextureFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestSaveLocalTextureFile(this.request_path,this.request_texture) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.DownLoadBinaryFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestDownLoadBinaryFile(this.request_path,this.request_post_data) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestDownLoadTextFile(this.request_path,this.request_post_data) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadTextureFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestDownLoadTextureFile(this.request_path,this.request_post_data) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadAssetBundle:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestDownLoadAssetBundle(this.request_path,this.request_post_data,this.request_assetbundle_id,this.request_data_version) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.LoadStreamingAssetsBinaryFile:
						{
							#if((UNITY_STANDALONE)||(UNITY_EDITOR))
							{
								if( Fee.File.File.GetInstance().GetMainIo().RequestLoadStreamingAssetsBinaryFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
								}
							}
							#else
							{
								//UNITY_ANDROID || UNITY_WEBGL
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadStreamingAssetsBinaryFile(this.request_path) == true){
									this.mode = Mode.Do_WebRequest;
								}	
							}
							#endif
						}break;
					case RequestType.LoadStreamingAssetsTextFile:
						{
							#if((UNITY_STANDALONE)||(UNITY_EDITOR))
							{
								if( Fee.File.File.GetInstance().GetMainIo().RequestLoadStreamingAssetsTextFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
								}
							}
							#else
							{
								//UNITY_ANDROID || UNITY_WEBGL
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadStreamingAssetsTextFile(this.request_path) == true){
									this.mode = Mode.Do_WebRequest;
								}	
							}
							#endif
						}break;
					case RequestType.LoadStreamingAssetsTextureFile:
						{
							#if((UNITY_STANDALONE)||(UNITY_EDITOR))
							{
								if( Fee.File.File.GetInstance().GetMainIo().RequestLoadStreamingAssetsTextureFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
								}
							}
							#else
							{
								//UNITY_ANDROID || UNITY_WEBGL
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadStreamingAssetsTextureFile(this.request_path) == true){
									this.mode = Mode.Do_WebRequest;
								}	
							}
							#endif
						}break;
					case RequestType.LoadResourcesAnythingFile:
						{
							if(Fee.File.File.GetInstance().GetMainResources().RequestLoadResourcesAnythingFile(this.request_path) == true){
								this.mode = Mode.Do_Resources;
							}
						}break;
					case RequestType.LoadResourcesTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainResources().RequestLoadResourcesTextFile(this.request_path) == true){
								this.mode = Mode.Do_Resources;
							}
						}break;
					case RequestType.LoadResourcesTextureFile:
						{
							if(Fee.File.File.GetInstance().GetMainResources().RequestLoadResourcesTextureFile(this.request_path) == true){
								this.mode = Mode.Do_Resources;
							}
						}break;
					case RequestType.LoadResourcesPrefabFile:
						{
							if(Fee.File.File.GetInstance().GetMainResources().RequestLoadResourcesPrefabFile(this.request_path) == true){
								this.mode = Mode.Do_Resources;
							}
						}break;

					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_Io:
				{
					Main_Io t_main = Fee.File.File.GetInstance().GetMainIo();

					this.item.SetResultProgressUp(t_main.GetResultProgressUp());
					this.item.SetResultProgressDown(t_main.GetResultProgressDown());

					if(t_main.GetResultType() != Main_Io.ResultType.None){ 
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Io.ResultType.Asset:
							{
								if(t_main.GetResultAsset() != null){
									this.item.SetResultAsset(t_main.GetResultAsset());
									t_success = true;
								}
							}break;
						case Main_Io.ResultType.SaveEnd:
							{
								this.item.SetResultSaveEnd();
								t_success = true;
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
			case Mode.Do_WebRequest:
				{
					Main_WebRequest t_main = Fee.File.File.GetInstance().GetMainWebRequest();

					this.item.SetResultProgressUp(t_main.GetResultProgressUp());
					this.item.SetResultProgressDown(t_main.GetResultProgressDown());

					if(t_main.GetResultType() != Main_WebRequest.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_WebRequest.ResultType.Asset:
							{
								if(t_main.GetResultAsset() != null){
									this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
									this.item.SetResultAsset(t_main.GetResultAsset());
									t_success = true;
								}
							}break;
						case Main_WebRequest.ResultType.AssetBundle:
							{
								if(t_main.GetResultAssetBundle() != null){
									this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
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
			case Mode.Do_Resources:
				{
					Main_Resources t_main = Fee.File.File.GetInstance().GetMainResources();

					this.item.SetResultProgressUp(t_main.GetResultProgressUp());
					this.item.SetResultProgressDown(t_main.GetResultProgressDown());

					if(t_main.GetResultType() != Main_Resources.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_Resources.ResultType.Asset:
							{
								if(t_main.GetResultAsset() != null){
									this.item.SetResultAsset(t_main.GetResultAsset());
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

