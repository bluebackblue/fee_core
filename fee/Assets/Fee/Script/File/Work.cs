using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。ワーク。
*/


/** NFile
*/
namespace NFile
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

			/** Do_SoundPool
			*/
			Do_SoundPool,

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

			/** ダウンロード。サウンドプール。
			*/
			DownLoadSoundPool,
		};

		/** mode
		*/
		private Mode mode;

		/** request_type
		*/
		private RequestType request_type;

		/** request_filename
		*/
		private string request_filename;

		/** request_url
		*/
		private string request_url;

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
		private Texture2D request_texture;

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
			this.request_type = RequestType.LoadLocalBinaryFile;

			//request_filename
			this.request_filename = null;

			//request_url
			this.request_url = null;

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
		public void RequestLoadLocalBinaryFile(string a_filename)
		{
			this.request_type = RequestType.LoadLocalBinaryFile;
			this.request_filename = a_filename;
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public void RequestLoadLocalTextFile(string a_filename)
		{
			this.request_type = RequestType.LoadLocalTextFile;
			this.request_filename = a_filename;
		}

		/** リクエスト。ロードローカル。テクスチャーファイル。
		*/
		public void RequestLoadLocalTextureFile(string a_filename)
		{
			this.request_type = RequestType.LoadLocalTextureFile;
			this.request_filename = a_filename;
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public void RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			this.request_type = RequestType.SaveLocalBinaryFile;
			this.request_filename = a_filename;
			this.request_binary = a_binary;
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public void RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			this.request_type = RequestType.SaveLocalTextFile;
			this.request_filename = a_filename;
			this.request_text = a_text;
		}

		/** リクエスト。セーブローカル。テクスチャーファイル。
		*/
		public void RequestSaveLocalTextureFile(string a_filename,Texture2D a_texture)
		{
			this.request_type = RequestType.SaveLocalTextureFile;
			this.request_filename = a_filename;
			this.request_texture = a_texture;
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public void RequestDownLoadBinaryFile(string a_url)
		{
			this.request_type = RequestType.DownLoadBinaryFile;
			this.request_url = a_url;
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public void RequestDownLoadTextFile(string a_url)
		{
			this.request_type = RequestType.DownLoadTextFile;
			this.request_url = a_url;
		}

		/** リクエスト。ダウンロード。テクスチャーファイル。
		*/
		public void RequestDownLoadTextureFile(string a_url)
		{
			this.request_type = RequestType.DownLoadTextureFile;
			this.request_url = a_url;
		}

		/** リクエスト。ダウンロード。アセットバンドル。
		*/
		public void RequestDownLoadAssetBundle(string a_url,long a_assetbundle_id,uint a_data_version)
		{
			this.request_type = RequestType.DownLoadAssetBundle;
			this.request_url = a_url;
			this.request_assetbundle_id = a_assetbundle_id;
			this.request_data_version = a_data_version;
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public void RequestLoadStreamingAssetsBinaryFile(string a_filename)
		{
			this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
			this.request_filename = a_filename;
		}

		/** リクエスト。ダウンロード。サウンドプール。
		*/
		public void RequestDownLoadSoundPool(string a_url,uint a_data_version)
		{
			this.request_type = RequestType.DownLoadSoundPool;
			this.request_url = a_url;
			this.request_data_version = a_data_version;
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
					switch(this.request_type){
					case RequestType.LoadLocalBinaryFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestLoadLocalBinaryFile(this.request_filename) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.LoadLocalTextFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestLoadLocalTextFile(this.request_filename) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.LoadLocalTextureFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestLoadLocalTextureFile(this.request_filename) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalBinaryFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestSaveLocalBinaryFile(this.request_filename,this.request_binary) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalTextFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestSaveLocalTextFile(this.request_filename,this.request_text) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.SaveLocalTextureFile:
						{
							MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
							if(t_io.RequestSaveLocalTextureFile(this.request_filename,this.request_texture) == true){
								this.mode = Mode.Do_Io;
							}
						}break;
					case RequestType.DownLoadBinaryFile:
						{
							MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();
							if(t_webrequest.RequestDownLoadBinaryFile(this.request_url) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadTextFile:
						{
							MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();
							if(t_webrequest.RequestDownLoadTextFile(this.request_url) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadTextureFile:
						{
							MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();
							if(t_webrequest.RequestDownLoadTextureFile(this.request_url) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.DownLoadAssetBundle:
						{
							MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();
							if(t_webrequest.RequestDownLoadAssetBundle(this.request_url,this.request_assetbundle_id,this.request_data_version) == true){
								this.mode = Mode.Do_WebRequest;
							}
						}break;
					case RequestType.LoadStreamingAssetsBinaryFile:
						{
							#if((!UNITY_EDITOR)&&((UNITY_ANDROID)||(UNITY_WEBGL)))
							{
								MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();
								if(t_webrequest.LoadStreamingAssetsBinaryFile(this.request_filename) == true){
									this.mode = Mode.Do_Io;
								}								
							}
							#else
							{
								MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();
								if(t_io.LoadStreamingAssetsBinaryFile(this.request_filename) == true){
									this.mode = Mode.Do_Io;
								}
							}
							#endif
						}break;
					case RequestType.DownLoadSoundPool:
						{
							MonoBehaviour_SoundPool t_soundpool = NFile.File.GetInstance().GetSoundPool();
							if(t_soundpool.RequestDownLoadSoundPool(this.request_url,this.request_data_version) == true){
								this.mode = Mode.Do_SoundPool;
							}
						}break;
					}
				}break;
			case Mode.End:
				{
				}return true;
			case Mode.Do_Io:
				{
					MonoBehaviour_Io t_io = NFile.File.GetInstance().GetIo();

					this.item.SetResultProgress(t_io.GetResultProgress());

					if(t_io.IsFix() == true){
						//結果。
						bool t_success = false;
						switch(t_io.GetResultType()){
						case MonoBehaviour_Base.ResultType.Binary:
							{
								if(t_io.GetResultBinary() != null){
									this.item.SetResultBinary(t_io.GetResultBinary());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.Text:
							{
								if(t_io.GetResultText() != null){
									this.item.SetResultText(t_io.GetResultText());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.Texture:
							{
								if(t_io.GetResultTexture() != null){
									this.item.SetResultTexture(t_io.GetResultTexture());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.SaveEnd:
							{
								this.item.SetResultSaveEnd();
								t_success = true;
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_io.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_io.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_io.Cancel();
					}
				}break;
			case Mode.Do_WebRequest:
				{
					MonoBehaviour_WebRequest t_webrequest = NFile.File.GetInstance().GetWebRequest();

					this.item.SetResultProgress(t_webrequest.GetResultProgress());

					if(t_webrequest.IsFix() == true){
						//結果。
						bool t_success = false;
						switch(t_webrequest.GetResultType()){
						case MonoBehaviour_Base.ResultType.Binary:
							{
								if(t_webrequest.GetResultBinary() != null){
									this.item.SetResultBinary(t_webrequest.GetResultBinary());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.Text:
							{
								if(t_webrequest.GetResultText() != null){
									this.item.SetResultText(t_webrequest.GetResultText());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.Texture:
							{
								if(t_webrequest.GetResultTexture() != null){
									this.item.SetResultTexture(t_webrequest.GetResultTexture());
									t_success = true;
								}
							}break;
						case MonoBehaviour_Base.ResultType.SaveEnd:
							{
								this.item.SetResultSaveEnd();
								t_success = true;
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_webrequest.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_webrequest.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_webrequest.Cancel();
					}
				}break;
			case Mode.Do_SoundPool:
				{
					MonoBehaviour_SoundPool t_soundpool = NFile.File.GetInstance().GetSoundPool();

					this.item.SetResultProgress(t_soundpool.GetResultProgress());

					if(t_soundpool.IsFix() == true){
						//結果。
						bool t_success = false;
						switch(t_soundpool.GetResultType()){
						case MonoBehaviour_Base.ResultType.SoundPool:
							{
								if(t_soundpool.GetResultSoundPool() != null){
									this.item.SetResultSoundPool(t_soundpool.GetResultSoundPool());
									t_success = true;
								}
							}break;
						}

						if(t_success == false){
							this.item.SetResultErrorString(t_soundpool.GetResultErrorString());
						}

						//リクエスト待ち開始。
						t_soundpool.WaitRequest();						

						this.mode = Mode.End;
					}else if(this.item.IsCancel() == true){
						//キャンセル。
						t_soundpool.Cancel();
					}
				}break;
			}

			return false;
		}
	}
}

