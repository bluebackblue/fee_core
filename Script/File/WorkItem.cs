

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。ワークアイテム。
*/


/** Fee.File
*/
namespace Fee.File
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

			/** Do_AndroidContent
			*/
			Do_AndroidContent,

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

			/** ロードローカル。テクスチャファイル。
			*/
			LoadLocalTextureFile,

			/** セーブローカル。バイナリファイル。
			*/
			SaveLocalBinaryFile,

			/** セーブローカル。テキストファイル。
			*/
			SaveLocalTextFile,

			/** セーブローカル。テクスチャファイル。
			*/
			SaveLocalTextureFile,

			/** ロードＵＲＬ。バイナリファイル。
			*/
			LoadUrlBinaryFile,

			/** ロードＵＲＬ。テキストファイル。
			*/
			LoadUrlTextFile,
	
			/** ロードＵＲＬ。テクスチャファイル。
			*/
			LoadUrlTextureFile,

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,

			/** ロードストリーミングアセット。テキストファイル。
			*/
			LoadStreamingAssetsTextFile,

			/** ロードストリーミングアセット。テクスチャファイル。
			*/
			LoadStreamingAssetsTextureFile,

			/** ロードリソース。テキストファイル。
			*/
			LoadResourcesTextFile,

			/** ロードリソース。テクスチャファイル。
			*/
			LoadResourcesTextureFile,

			/** ロードリソース。プレハブファイル。
			*/
			LoadResourcesPrefabFile,

			/** ロードフルパス。バイナリファイル。
			*/
			LoadFullPathBinaryFile,

			/** ロードフルパス。テキストファイル。
			*/
			LoadFullPathTextFile,

			/** ロードフルパス。テクスチャファイル。
			*/
			LoadFullPathTextureFile,

			#if(UNITY_EDITOR)

			/** ロードアセットパス。バイナリファイル。
			*/
			LoadAssetsPathBinaryFile,

			/** ロードアセットパス。テキストファイル。
			*/
			LoadAssetsPathTextFile,

			#endif
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

		/** request_certificate_handler
		*/
		private Fee.File.CustomCertificateHandler request_certificate_handler;

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

		/** リクエスト。ロードローカル。テクスチャファイル。
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

		/** リクエスト。セーブローカル。テクスチャファイル。
		*/
		public void RequestSaveLocalTextureFile(Path a_relative_path,UnityEngine.Texture2D a_texture)
		{
			this.request_type = RequestType.SaveLocalTextureFile;
			this.request_path = a_relative_path;
			this.request_texture = a_texture;
		}

		/** リクエスト。ロードＵＲＬ。バイナリファイル。
		*/
		public void RequestLoadUrlBinaryFile(Path a_relative_path,UnityEngine.WWWForm a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			this.request_type = RequestType.LoadUrlBinaryFile;
			this.request_path = a_relative_path;
			this.request_post_data = a_post_data;
			this.request_certificate_handler = a_certificate_handler;
		}

		/** リクエスト。ロードＵＲＬ。テキストファイル。
		*/
		public void RequestLoadUrlTextFile(Path a_url_path,UnityEngine.WWWForm a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			this.request_type = RequestType.LoadUrlTextFile;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
			this.request_certificate_handler = a_certificate_handler;
		}

		/** リクエスト。ロードＵＲＬ。テクスチャファイル。
		*/
		public void RequestLoadUrlTextureFile(Path a_url_path,UnityEngine.WWWForm a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			this.request_type = RequestType.LoadUrlTextureFile;
			this.request_path = a_url_path;
			this.request_post_data = a_post_data;
			this.request_certificate_handler = a_certificate_handler;
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

		/** リクエスト。ロードストリーミングアセット。テクスチャファイル。
		*/
		public void RequestLoadStreamingAssetsTextureFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadStreamingAssetsTextureFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト、ロードリソース。テキストファイル。
		*/
		public void RequestLoadResourcesTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadResourcesTextFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードリソース。テクスチャファイル。
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

		/** リクエスト。ロードフルパス。バイナリファイル。
		*/
		public void RequestLoadFullPathBinaryFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadFullPathBinaryFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードフルパス。テキストファイル。
		*/
		public void RequestLoadFullPathTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadFullPathTextFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードフルパス。テクスチャーファイル。
		*/
		public void RequestLoadFullPathTextureFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadFullPathTextureFile;
			this.request_path = a_relative_path;
		}

		#if(UNITY_EDITOR)

		/** リクエスト。ロードアセットパス。バイナリファイル。
		*/
		public void RequestLoadAssetsPathBinaryFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadAssetsPathBinaryFile;
			this.request_path = a_relative_path;
		}

		/** リクエスト。ロードアセットパス。バイナリファイル。
		*/
		public void RequestLoadAssetsPathTextFile(Path a_relative_path)
		{
			this.request_type = RequestType.LoadAssetsPathTextFile;
			this.request_path = a_relative_path;
		}

		#endif

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
					case RequestType.LoadUrlBinaryFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlBinaryFile(this.request_path,this.request_post_data,this.request_certificate_handler) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.LoadUrlTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextFile(this.request_path,this.request_post_data,this.request_certificate_handler) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.LoadUrlTextureFile:
						{
							if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextureFile(this.request_path,this.request_post_data,this.request_certificate_handler) == true){
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

					case RequestType.LoadFullPathBinaryFile:
						{
							#if(UNITY_ANDROID)
							if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^content\\:\\/\\/.*$") == true){
								if(Fee.File.File.GetInstance().GetMainAndroidContent().RequestLoadAndroidContentBinaryFile(this.request_path) == true){
									this.mode = Mode.Do_AndroidContent;
									break;
								}
							}
							#endif

							#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
							{
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlBinaryFile(this.request_path,null,null) == true){
									this.mode = Mode.Do_WebRequest;
									break;
								}
							}
							#else
							{
								if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^(http|https)\\:\\/\\/.*$") == true){
									if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlBinaryFile(this.request_path,null,null) == true){
										this.mode = Mode.Do_WebRequest;
										break;
									}
								}

								if(Fee.File.File.GetInstance().GetMainIo().RequestLoadFullPathBinaryFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
									break;
								}
							}
							#endif

						}break;
					case RequestType.LoadFullPathTextFile:
						{
							#if(UNITY_ANDROID)
							if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^content\\:\\/\\/.*$") == true){
								if(Fee.File.File.GetInstance().GetMainAndroidContent().RequestLoadAndroidContentTextFile(this.request_path) == true){
									this.mode = Mode.Do_AndroidContent;
									break;
								}
							}
							#endif
						
							#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
							{
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextFile(this.request_path,null,null) == true){
									this.mode = Mode.Do_WebRequest;
									break;
								}
							}
							#else
							{
								if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^(http|https)\\:\\/\\/.*$") == true){
									if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextFile(this.request_path,null,null) == true){
										this.mode = Mode.Do_WebRequest;
										break;
									}
								}

								if(Fee.File.File.GetInstance().GetMainIo().RequestLoadFullPathTextFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
									break;
								}
							}
							#endif
						}break;
					case RequestType.LoadFullPathTextureFile:
						{
							#if(UNITY_ANDROID)
							if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^content\\:\\/\\/.*$") == true){
								if(Fee.File.File.GetInstance().GetMainAndroidContent().RequestLoadAndroidContentTextureFile(this.request_path) == true){
									this.mode = Mode.Do_AndroidContent;
									break;
								}
							}
							#endif
						
							#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
							{
								if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextureFile(this.request_path,null,null) == true){
									this.mode = Mode.Do_WebRequest;
									break;
								}
							}
							#else
							{
								if(System.Text.RegularExpressions.Regex.IsMatch(this.request_path.GetPath(),"^(http|https)\\:\\/\\/.*$") == true){
									if(Fee.File.File.GetInstance().GetMainWebRequest().RequestLoadUrlTextureFile(this.request_path,null,null) == true){
										this.mode = Mode.Do_WebRequest;
										break;
									}
								}

								if(Fee.File.File.GetInstance().GetMainIo().RequestLoadFullPathTextureFile(this.request_path) == true){
									this.mode = Mode.Do_Io;
									break;
								}
							}
							#endif
						}break;

					#if(UNITY_EDITOR)

					case RequestType.LoadAssetsPathBinaryFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestLoadAssetsPathBinaryFile(this.request_path) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.LoadAssetsPathTextFile:
						{
							if(Fee.File.File.GetInstance().GetMainIo().RequestLoadAssetsPathTextFile(this.request_path) == true){
								this.mode = Mode.Do_Io;
							}
						}break;

					#endif

					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_AndroidContent:
				{
					Main_AndroidContent t_main = Fee.File.File.GetInstance().GetMainAndroidContent();

					this.item.SetResultProgress(t_main.GetResultProgress());

					if(t_main.GetResultType() != Main_AndroidContent.ResultType.None){
						//結果。
						bool t_success = false;
						switch(t_main.GetResultType()){
						case Main_AndroidContent.ResultType.Asset:
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
			case Mode.Do_Io:
				{
					Main_Io t_main = Fee.File.File.GetInstance().GetMainIo();

					this.item.SetResultProgress(t_main.GetResultProgress());

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

					this.item.SetResultProgress(t_main.GetResultProgress());

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
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_main.GetResultErrorString());
							this.item.SetResultResponseHeader(t_main.GetResultResponseHeader());
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

					this.item.SetResultProgress(t_main.GetResultProgress());

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

