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
	public class MonoBehaviour_Io : MonoBehaviour_Base
	{
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
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_filename
		*/
		[SerializeField]
		private string request_filename;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_filename
			this.request_filename = null;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.SaveLocalBinaryFile:
			case RequestType.LoadLocalBinaryFile:
			case RequestType.SaveLocalTextFile:
			case RequestType.LoadLocalTextFile:
			case RequestType.SaveLocalPngFile:
			case RequestType.LoadLocalPngFile:
				{
					Tool.Log("MonoBehaviour_Io",this.request_type.ToString());
					this.mode = Mode.Do;
				}break;
			default:
				{
					//不明なリクエスト。
					this.result_datatype = DataType.Error;
					this.result_errorstring = "request_type == " + this.request_type.ToString();
					Tool.Assert(false);
				}break;
			}

			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.SaveLocalBinaryFile:
				{
					yield return this.Raw_Do_SaveLocalBinaryFile();

					if(this.result_datatype == DataType.SaveEnd){
						this.mode = Mode.Do_Success;
						yield break;
					}
				}break;
			case RequestType.LoadLocalBinaryFile:
				{
					yield return this.Raw_Do_LoadLocalBinaryFile();

					if(this.result_datatype == DataType.Binary){
						if(this.GetResultBinary() != null){
							this.mode = Mode.Do_Success;
							yield break;
						}
					}
				}break;
			case RequestType.SaveLocalTextFile:
				{
					yield return this.Raw_Do_SaveLocalTextFile();

					if(this.result_datatype == DataType.SaveEnd){
						this.mode = Mode.Do_Success;
						yield break;
					}
				}break;
			case RequestType.LoadLocalTextFile:
				{
					yield return this.Raw_Do_LoadLocalTextFile();

					if(this.result_datatype == DataType.Text){
						if(this.GetResultText() != null){
							this.mode = Mode.Do_Success;
							yield break;
						}
					}
				}break;
			case RequestType.SaveLocalPngFile:
				{
					yield return this.Raw_Do_SaveLocalPngFile();

					if(this.result_datatype == DataType.SaveEnd){
						this.mode = Mode.Do_Success;
						yield break;
					}
				}break;
			case RequestType.LoadLocalPngFile:
				{
					yield return this.Raw_Do_LoadLocalPngFile();

					if(this.result_datatype == DataType.Texture){
						if(this.GetResultTexture() != null){
							this.mode = Mode.Do_Success;
							yield break;
						}
					}
				}break;
			}

			{
				this.result_datatype = DataType.Error;
				if(this.result_errorstring == null){
					this.result_errorstring = "errorstring == null";
				}
			}

			this.mode = Mode.Do_Error;
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.result_progress = 1.0f;

			this.mode = Mode.Fix;
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.result_progress = 1.0f;

			this.mode = Mode.Fix;
			yield break;
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			if(this.mode == Mode.WaitRequest){
				this.mode = Mode.Start;
				this.ResetFlag();

				this.request_type = RequestType.SaveLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = a_binary;

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
				this.ResetFlag();

				this.request_type = RequestType.LoadLocalBinaryFile;
				this.request_filename = a_filename;

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
				this.ResetFlag();

				this.request_type = RequestType.SaveLocalTextFile;
				this.request_filename = a_filename;
				this.request_text = a_text;

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
				this.ResetFlag();

				this.request_type = RequestType.LoadLocalTextFile;
				this.request_filename = a_filename;

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
				this.ResetFlag();

				this.request_type = RequestType.SaveLocalPngFile;
				this.request_filename = a_filename;
				this.request_texture = a_texture;

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
				this.ResetFlag();

				this.request_type = RequestType.LoadLocalPngFile;
				this.request_filename = a_filename;

				return true;
			}else{
				return false;
			}
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
			this.result_progress = a_progress;
		}

		/** エラー文字列。設定。
		*/
		public void SetErrorStringFromTask(string a_string)
		{
			this.result_errorstring = a_string;
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
			if(t_result != null){
				this.SetResultBinary(t_result);
				this.result_datatype = DataType.Binary;
			}else{
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
			if(t_result != null){
				this.SetResultText(t_result);
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
					this.SetResultTexture(t_texture);
					this.result_datatype = DataType.Texture;
				}
			}else{
				this.result_datatype = DataType.Error;
			}
		}
	}
}

