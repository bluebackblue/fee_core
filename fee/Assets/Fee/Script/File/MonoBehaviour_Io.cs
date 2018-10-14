using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。ＩＯ。
*/


/** NFile
*/
namespace NFile
{
	/** MonoBehaviour_Io
	*/
	public class MonoBehaviour_Io : MonoBehaviour_Base
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

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

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_filename
		*/
		[SerializeField]
		private string request_filename;

		/** request_binary
		*/
		[SerializeField]
		private byte[] request_binary;

		/** request_text
		*/
		[SerializeField]
		private string request_text;

		/** request_texture
		*/
		[SerializeField]
		private Texture2D request_texture;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_filename
			this.request_filename = null;

			//request_binary
			this.request_binary = null;

			//request_text
			this.request_text = null;

			//request_texture
			this.request_texture = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.LoadLocalBinaryFile:
			case RequestType.LoadLocalTextFile:
			case RequestType.LoadLocalTextureFile:
			case RequestType.SaveLocalBinaryFile:
			case RequestType.SaveLocalTextFile:
			case RequestType.SaveLocalTextureFile:
			case RequestType.LoadStreamingAssetsBinaryFile:
				{
					Tool.Log("MonoBehaviour_Io",this.request_type.ToString());
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();

			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.LoadLocalBinaryFile:
				{
					Coroutine_LoadLocalBinaryFile t_coroutine = new Coroutine_LoadLocalBinaryFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.LoadLocalTextFile:
				{
					Coroutine_LoadLocalTextFile t_coroutine = new Coroutine_LoadLocalTextFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename);

					if(t_coroutine.result.text != null){
						this.SetResultText(t_coroutine.result.text);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.LoadLocalTextureFile:
				{
					Coroutine_LoadLocalTextureFile t_coroutine = new Coroutine_LoadLocalTextureFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename);

					if(t_coroutine.result.texture != null){
						this.SetResultTexture(t_coroutine.result.texture);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.SaveLocalBinaryFile:
				{
					Coroutine_SaveLocalBinaryFile t_coroutine = new Coroutine_SaveLocalBinaryFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename,this.request_binary);

					if(t_coroutine.result.saveend == true){
						this.SetResultSaveEnd();
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.SaveLocalTextFile:
				{
					Coroutine_SaveLocalTextFile t_coroutine = new Coroutine_SaveLocalTextFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename,this.request_text);

					if(t_coroutine.result.saveend == true){
						this.SetResultSaveEnd();
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.SaveLocalTextureFile:
				{
					Coroutine_SaveLocalTextureFile t_coroutine = new Coroutine_SaveLocalTextureFile();
					yield return t_coroutine.CoroutineMain(this,Application.persistentDataPath + "/" + this.request_filename,this.request_texture);

					if(t_coroutine.result.saveend == true){
						this.SetResultSaveEnd();
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.LoadStreamingAssetsBinaryFile:
				{
					Coroutine_LoadLocalBinaryFile t_coroutine = new Coroutine_LoadLocalBinaryFile();
					yield return t_coroutine.CoroutineMain(this,Application.streamingAssetsPath + "/" + this.request_filename);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public bool RequestLoadLocalBinaryFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadLocalBinaryFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public bool RequestLoadLocalTextFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadLocalTextFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}

		/** リクエスト。ロードローカル。テクスチャファイル。
		*/
		public bool RequestLoadLocalTextureFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadLocalTextureFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public bool RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.SaveLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = a_binary;

				return true;
			}

			return false;
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public bool RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.SaveLocalTextFile;
				this.request_filename = a_filename;
				this.request_text = a_text;

				return true;
			}

			return false;
		}

		/** リクエスト。セーブローカル。テクスチャファイル。
		*/
		public bool RequestSaveLocalTextureFile(string a_filename,Texture2D a_texture)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.SaveLocalTextureFile;
				this.request_filename = a_filename;
				this.request_texture = a_texture;

				return true;
			}

			return false;
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public bool LoadStreamingAssetsBinaryFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}
	}
}

