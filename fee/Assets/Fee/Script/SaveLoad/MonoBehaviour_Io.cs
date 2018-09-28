using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。ＩＯ。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** MonoBehaviour_Io
	*/
	public class MonoBehaviour_Io : MonoBehaviour
	{
		/** Mode
		*/
		private enum Mode
		{
			/** リクエスト待ち。
			*/
			WaitRequest,

			/** 開始。
			*/
			Start,

			/** 実行中。
			*/
			Do,

			/** 完了。
			*/
			Fix,
		};

		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** セーブローカル。バイナリファイル。
			*/
			SaveLocalBinaryFile,

			/** ロードローカル。バイナリファイル。
			*/
			LoadLocalBinaryFile,

			/** セーブローカル。テキストファイル。
			*/
			SaveLocalTextFile,

			/** ロードローカル。テキストファイル。
			*/
			LoadLocalTextFile,

			/** セーブローカル。ＰＮＧファイル。
			*/
			SaveLocalPngFile,

			/** ロードローカル。ＰＮＧファイル。
			*/
			LoadLocalPngFile,	
		}

		/** delete_flag
		*/
		[SerializeField]
		private bool delete_flag;

		/** mode
		*/
		[SerializeField]
		private Mode mode;

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** reqest_filename
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

		/** result_errorstring
		*/
		[SerializeField]
		private string result_errorstring;

		/** result_progress 
		*/
		private float result_progress;

		/** result_datatype
		*/
		[SerializeField]
		private DataType result_datatype;

		/** result_binary
		*/
		[SerializeField]
		private byte[] result_binary;

		/** result_text
		*/
		[SerializeField]
		private string result_text;

		/** result_texture
		*/
		[SerializeField]
		private Texture2D result_texture;

		/** Awake
		*/
		private void Awake()
		{
			//delete_flag
			this.delete_flag = false;

			//mode
			this.mode = Mode.WaitRequest;

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

			//result_errorstring
			this.result_errorstring = "";

			//result_progress
			this.result_progress = 0.0f;

			//result_datatype
			this.result_datatype = DataType.None;

			//result_binary
			this.result_binary = null;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.SaveLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = a_binary;
				this.request_text = null;
				this.request_texture = null;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalBinaryFile(string a_filename)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.LoadLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.SaveLocalTextFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = a_text;
				this.request_texture = null;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalTextFile(string a_filename)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.LoadLocalTextFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalPngFile(string a_filename,Texture2D a_texture)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.SaveLocalPngFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = a_texture;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalPngFile(string a_filename)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;

				this.request_type = RequestType.LoadLocalPngFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				this.result_progress = 0.0f;
				this.result_datatype = DataType.None;
				this.result_binary = null;
				this.result_text = null;
				this.result_texture = null;

				return true;
			}else{
				return false;
			}
		}

		/** 完了チェック。
		*/
		public bool IsFix()
		{
			if(this.mode == Mode.Fix){
				return true;
			}
			return false;
		}

		/** リクエスト待ち開始。
		*/
		public void WaitRequest()
		{
			if(this.mode == Mode.Fix){
				this.mode = Mode.WaitRequest;
			}
		}

		/** DeleteRequest
		*/
		public void DeleteRequest()
		{
			this.delete_flag = true;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** 結果。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** ＰＮＧのサイズをバイトバイナリから取得する。
		*/
		private void GetSizeFromPngBinary(byte[] a_png,out int a_width,out int a_height)
		{
			int t_width = 0;
			int t_height = 0;

			if(a_png != null){
				if(a_png.Length > 23){
					t_width += a_png[16] * 256 * 256 * 256;
					t_width += a_png[17] * 256 * 256;
					t_width += a_png[18] * 256;
					t_width += a_png[19];

					t_height += a_png[20] * 256 * 256 * 256;
					t_height += a_png[21] * 256 * 256;
					t_height += a_png[22] * 256;
					t_height += a_png[23];
				}
			}

			a_width = t_width;
			a_height = t_height;
		}

		/** プログレス。設定。
		*/
		public void SetProgressFromTask(float a_progress)
		{
			Tool.Log("MonoBehaviour_Io","progress = " + a_progress.ToString());
			this.result_progress = a_progress;
		}

		/** [内部からの呼び出し]実行中。セーブローカル。バイナリファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalBinaryFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = Task_SaveLocalBinaryFile.Run(this,t_full_path,this.request_binary,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_SaveLocalBinaryFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalBinaryFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result == true){
				this.result_datatype = DataType.SaveEnd;
			}else{
				this.result_datatype = DataType.Error;
			}
		}

		/** [内部からの呼び出し]実行中。ロードローカル。バイナリファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalBinaryFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			byte[] t_result = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = Task_LoadLocalBinaryFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_LoadLocalBinaryFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_LoadLocalBinaryFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = null;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result != null){
				this.result_binary = t_result;
				this.result_datatype = DataType.Binary;
			}else{
				if(this.result_errorstring == null){
					this.result_errorstring = "error == null";
				}
				this.result_datatype = DataType.Error;
			}
		}

		/** [内部からの呼び出し]実行中。セーブローカル。テキストファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalTextFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = Task_SaveLocalTextFile.Run(this,t_full_path,this.request_text,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_SaveLocalTextFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalTextFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result == true){
				this.result_datatype = DataType.SaveEnd;
			}else{
				this.result_datatype = DataType.Error;
			}
		}

		/** [内部からの呼び出し]実行中。ロードローカル。テキストファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalTextFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			string t_result = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<string> t_task = Task_LoadLocalTextFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_LoadLocalTextFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_LoadLocalTextFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = null;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result != null){
				this.result_text = t_result;
				this.result_datatype = DataType.Text;
			}else{
				this.result_datatype = DataType.Error;
			}
		}

		/** [内部からの呼び出し]実行中。セーブローカル。ＰＮＧファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalPngFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;

			//TODO:busy
			byte[] t_binary = null;
			{
				t_binary = this.request_texture.EncodeToPNG();
			}

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = Task_SaveLocalPngFile.Run(this,t_full_path,t_binary,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_SaveLocalPngFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalPngFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result == true){
				this.result_datatype = DataType.SaveEnd;
			}else{
				this.result_datatype = DataType.Error;
			}
		}

		/** [内部からの呼び出し]実行中。ロードローカル。ＰＮＧファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalPngFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			byte[] t_result = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = Task_LoadLocalPngFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					if(this.delete_flag == true){
						Tool.Log("Raw_Do_LoadLocalPngFile","Cancel");
						t_cancel_token.Cancel();
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_LoadLocalPngFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = null;
				}
			}

			//結果。
			this.result_progress = 1.0f;
			if(t_result != null){
				byte[] t_binary = t_result;

				//TODO:busy
				Texture2D t_texture = null;
				{
					int t_width;
					int t_height;
					this.GetSizeFromPngBinary(t_binary,out t_width,out t_height);
					t_texture = new Texture2D(t_width,t_height);
					if(t_texture.LoadImage(t_binary) == false){
						t_texture = null;
					}
				}

				if(t_texture == null){
					this.result_datatype = DataType.Error;
				}else{
					this.result_texture = t_texture;
					this.result_datatype = DataType.Texture;
				}
			}else{
				this.result_datatype = DataType.Error;
			}
		}

		/** Start
		*/
		private IEnumerator Start()
		{
			bool t_loop = true;
			while(t_loop){
				switch(this.mode){
				case Mode.WaitRequest:
					{
						//リクエスト待ち。
						yield return null;

						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				case Mode.Start:
					{
						Tool.Log("MonoBehaviour_Io",this.request_type.ToString());
						this.mode = Mode.Do;
					}break;
				case Mode.Do:
					{
						switch(this.request_type){
						case RequestType.SaveLocalBinaryFile:
							{
								yield return this.Raw_Do_SaveLocalBinaryFile();
							}break;
						case RequestType.LoadLocalBinaryFile:
							{
								yield return this.Raw_Do_LoadLocalBinaryFile();
							}break;
						case RequestType.SaveLocalTextFile:
							{
								yield return this.Raw_Do_SaveLocalTextFile();
							}break;
						case RequestType.LoadLocalTextFile:
							{
								yield return this.Raw_Do_LoadLocalTextFile();
							}break;
						case RequestType.SaveLocalPngFile:
							{
								yield return this.Raw_Do_SaveLocalPngFile();
							}break;
						case RequestType.LoadLocalPngFile:
							{
								yield return this.Raw_Do_LoadLocalPngFile();
							}break;
						default:
							{
								this.result_datatype = DataType.Error;
								this.result_errorstring = "request_type == " + this.request_type.ToString();
								Tool.Assert(false);
							}break;
						}

						if(this.result_datatype == DataType.Error){
							if(this.result_errorstring == null){
								this.result_errorstring = "error == null";
							}
						}

						this.mode = Mode.Fix;
					}break;
				case Mode.Fix:
					{
						yield return null;

						if(this.delete_flag == true){
							t_loop = false;
						}
					}break;
				}
			}

			Tool.Log("MonoBehaviour_Io","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}
	}
}

