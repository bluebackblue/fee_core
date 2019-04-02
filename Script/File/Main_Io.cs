

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。ＩＯ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Main_Io
	*/
	public class Main_Io : OnCoroutine_CallBack
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

			/** ロードストリーミングアセット。テキストファイル。
			*/
			LoadStreamingAssetsTextFile,
		};

		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** エラー。
			*/
			Error,

			/** セーブ完了。
			*/
			SaveEnd,

			/** バイナリー。
			*/
			Binary,

			/** テキスト。
			*/
			Text,

			/** テクスチャ。
			*/
			Texture,

			/** アセットバンドル。
			*/
			AssetBundle,
		};

		/** is_busy
		*/
		private bool is_busy;

		/** キャンセル。チェック。
		*/
		private bool is_cancel;

		/** シャットダウン。チェック。
		*/
		private bool is_shutdown;

		/** request_type
		*/
		private RequestType request_type;

		/** request_relative_path
		*/
		private Fee.File.Path request_relative_path;

		/** request_binary
		*/
		private byte[] request_binary;

		/** request_text
		*/
		private string request_text;

		/** request_texture
		*/
		private UnityEngine.Texture2D request_texture;

		/** result_progress_up
		*/
		private float result_progress_up;

		/** result_progress_down
		*/
		private float result_progress_down;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_binary
		*/
		private byte[] result_binary;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private UnityEngine.Texture2D result_texture;

		/** result_assetbundle
		*/
		private UnityEngine.AssetBundle result_assetbundle;

		/** constructor
		*/
		public Main_Io()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_relative_path = null;
			this.request_binary = null;
			this.request_text = null;
			this.request_texture = null;

			//result
			this.result_progress_up = 0.0f;
			this.result_progress_down = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_binary = null;
			this.result_text = null;
			this.result_texture = null;
			this.result_assetbundle = null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.is_shutdown = true;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.is_cancel = true;
		}

		/** 完了。
		*/
		public void Fix()
		{
			this.is_busy = false;
		}

		/** GetResultProgressUp
		*/
		public float GetResultProgressUp()
		{
			return this.result_progress_up;
		}

		/** GetResultProgressDown
		*/
		public float GetResultProgressDown()
		{
			return this.result_progress_down;
		}

		/** GetResultErrorString
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** GetResultType
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** GetResultBinary
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** GetResultText
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** GetResultTexture
		*/
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** GetResultAssetBundle
		*/
		public UnityEngine.AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** [Fee.File.OnCoroutine_CallBack]コルーチンからのコールバック。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress_up,float a_progress_down)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress_up = a_progress_up;
			this.result_progress_down = a_progress_down;
			return true;
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public bool RequestLoadLocalBinaryFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.LoadLocalBinaryFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoLoadLocalBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードローカル。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadLocalBinaryFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadLocalBinaryFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_LoadLocalBinaryFile t_coroutine = new Coroutine_LoadLocalBinaryFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.binary != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public bool RequestLoadLocalTextFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.LoadLocalTextFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoLoadLocalTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードローカル。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadLocalTextFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadLocalTextFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_LoadLocalTextFile t_coroutine = new Coroutine_LoadLocalTextFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.text != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_text = t_coroutine.result.text;
				this.result_type = ResultType.Text;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードローカル。テクスチャファイル。
		*/
		public bool RequestLoadLocalTextureFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.LoadLocalTextureFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoLoadLocalTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードローカル。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoLoadLocalTextureFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadLocalTextureFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_LoadLocalTextureFile t_coroutine = new Coroutine_LoadLocalTextureFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.texture != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_texture = t_coroutine.result.texture;
				this.result_type = ResultType.Texture;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public bool RequestSaveLocalBinaryFile(Fee.File.Path a_relative_path,byte[] a_binary)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.SaveLocalBinaryFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = a_binary;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoSaveLocalBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。セーブローカル。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoSaveLocalBinaryFile()
		{
			Tool.Assert(this.request_type == RequestType.SaveLocalBinaryFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_SaveLocalBinaryFile t_coroutine = new Coroutine_SaveLocalBinaryFile();
			yield return t_coroutine.CoroutineMain(this,t_path,this.request_binary);

			if(t_coroutine.result.saveend == true){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_type = ResultType.SaveEnd;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public bool RequestSaveLocalTextFile(Fee.File.Path a_relative_path,string a_text)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.SaveLocalTextFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = a_text;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoSaveLocalTextFile());
				return true;
			}

			return false;
		}

		/** 実行。セーブローカル。テキストファイル。
		*/
		private System.Collections.IEnumerator DoSaveLocalTextFile()
		{
			Tool.Assert(this.request_type == RequestType.SaveLocalTextFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_SaveLocalTextFile t_coroutine = new Coroutine_SaveLocalTextFile();
			yield return t_coroutine.CoroutineMain(this,t_path,this.request_text);

			if(t_coroutine.result.saveend == true){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_type = ResultType.SaveEnd;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。セーブローカル。テクスチャファイル。
		*/
		public bool RequestSaveLocalTextureFile(Fee.File.Path a_relative_path,UnityEngine.Texture2D a_texture)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.SaveLocalTextureFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = a_texture;

				Function.Function.StartCoroutine(this.DoSaveLocalTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。セーブローカル。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoSaveLocalTextureFile()
		{
			Tool.Assert(this.request_type == RequestType.SaveLocalTextureFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = File.GetLocalPath(this.request_relative_path);

			Coroutine_SaveLocalTextureFile t_coroutine = new Coroutine_SaveLocalTextureFile();
			yield return t_coroutine.CoroutineMain(this,t_path,this.request_texture);

			if(t_coroutine.result.saveend == true){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_type = ResultType.SaveEnd;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public bool RequestLoadStreamingAssetsBinaryFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsBinaryFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadStreamingAssetsBinaryFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = new Path(UnityEngine.Application.streamingAssetsPath + "/",this.request_relative_path.GetPath());

			Coroutine_LoadLocalBinaryFile t_coroutine = new Coroutine_LoadLocalBinaryFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.binary != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_binary = t_coroutine.result.binary;
				this.result_type = ResultType.Binary;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。テキストファイル。
		*/
		public bool RequestLoadStreamingAssetsTextFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.LoadStreamingAssetsTextFile;
				this.request_relative_path = a_relative_path;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsTextFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadStreamingAssetsTextFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = new Path(UnityEngine.Application.streamingAssetsPath + "/",this.request_relative_path.GetPath());

			Coroutine_LoadLocalTextFile t_coroutine = new Coroutine_LoadLocalTextFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.text != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_text = t_coroutine.result.text;
				this.result_type = ResultType.Text;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}


	}
}

