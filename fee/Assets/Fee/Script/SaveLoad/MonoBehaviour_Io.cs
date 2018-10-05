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

		/** cancel_flag
		*/
		[SerializeField]
		private bool cancel_flag;

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
			//cancel_flag
			this.cancel_flag = false;

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
			case RequestType.SaveLocalBinaryFile:
			case RequestType.LoadLocalBinaryFile:
			case RequestType.SaveLocalTextFile:
			case RequestType.LoadLocalTextFile:
			case RequestType.SaveLocalPngFile:
			case RequestType.LoadLocalPngFile:
				{
					Tool.Log("MonoBehaviour_Io",this.request_type.ToString());
					this.SetModeDo();
				}break;
			default:
				{
					//不明なリクエスト。
					this.SetResultErrorString("request_type == " + this.request_type.ToString());
					this.SetModeDoError();
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

					if(this.GetResultDataType() == DataType.SaveEnd){
						this.SetModeDoSuccess();
						yield break;
					}
				}break;
			case RequestType.LoadLocalBinaryFile:
				{
					yield return this.Raw_Do_LoadLocalBinaryFile();

					if(this.GetResultDataType() == DataType.Binary){
						if(this.GetResultBinary() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			case RequestType.SaveLocalTextFile:
				{
					yield return this.Raw_Do_SaveLocalTextFile();

					if(this.GetResultDataType() == DataType.SaveEnd){
						this.SetModeDoSuccess();
						yield break;
					}
				}break;
			case RequestType.LoadLocalTextFile:
				{
					yield return this.Raw_Do_LoadLocalTextFile();

					if(this.GetResultDataType() == DataType.Text){
						if(this.GetResultText() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			case RequestType.SaveLocalPngFile:
				{
					yield return this.Raw_Do_SaveLocalPngFile();

					if(this.GetResultDataType() == DataType.SaveEnd){
						this.SetModeDoSuccess();
						yield break;
					}
				}break;
			case RequestType.LoadLocalPngFile:
				{
					yield return this.Raw_Do_LoadLocalPngFile();

					if(this.GetResultDataType() == DataType.Texture){
						if(this.GetResultTexture() != null){
							this.SetModeDoSuccess();
							yield break;
						}
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

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.cancel_flag = true;
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.SaveLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = a_binary;
				this.request_text = null;
				this.request_texture = null;

				return true;
			}

			return false;
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalBinaryFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.LoadLocalBinaryFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				return true;
			}

			return false;
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.SaveLocalTextFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = a_text;
				this.request_texture = null;

				return true;
			}

			return false;
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalTextFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.LoadLocalTextFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				return true;
			}

			return false;
		}

		/** リクエスト。
		*/
		public bool RequestSaveLocalPngFile(string a_filename,Texture2D a_texture)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.SaveLocalPngFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = a_texture;

				return true;
			}

			return false;
		}

		/** リクエスト。
		*/
		public bool RequestLoadLocalPngFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.cancel_flag = false;

				this.request_type = RequestType.LoadLocalPngFile;
				this.request_filename = a_filename;
				this.request_binary = null;
				this.request_text = null;
				this.request_texture = null;

				return true;
			}

			return false;
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
			this.SetResultProgress(a_progress);
		}

		/** TODO:エラー文字列。設定。
		*/
		public void SetErrorStringFromTask(string a_error_string)
		{
			this.SetResultErrorString(a_error_string);
		}

		/** [内部からの呼び出し]セーブローカル。バイナリファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalBinaryFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;
			string t_errorstring = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = Task_SaveLocalBinaryFile.Run(this,t_full_path,this.request_binary,t_cancel_token);

				//終了待ち。
				do{
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalBinaryFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
					t_errorstring = "error";
				}
			}

			//結果。
			if(t_result == true){
				this.SetResultSaveEnd();
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ロードローカル。バイナリファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalBinaryFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			byte[] t_result = null;
			string t_errorstring = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = Task_LoadLocalBinaryFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_LoadLocalBinaryFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = null;
					t_errorstring = "error";
				}
			}

			//結果。
			if(t_result != null){
				this.SetResultBinary(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]セーブローカル。テキストファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalTextFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;
			string t_errorstring = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = Task_SaveLocalTextFile.Run(this,t_full_path,this.request_text,t_cancel_token);

				//終了待ち。
				do{
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalTextFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
					t_errorstring = "error";
				}
			}

			//結果。
			if(t_result == true){
				this.SetResultSaveEnd();
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ロードローカル。テキストファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalTextFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			string t_result = null;
			string t_errorstring = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<string> t_task = Task_LoadLocalTextFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_LoadLocalTextFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = null;
					t_errorstring = "error";
				}
			}

			//結果。
			if(t_result != null){
				this.SetResultText(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]セーブローカル。ＰＮＧファイル。
		*/
		private IEnumerator Raw_Do_SaveLocalPngFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			bool t_result = false;
			string t_errorstring = null;

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
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				Tool.Log("Raw_Do_SaveLocalPngFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result = t_task.GetResult();
				}else{
					t_result = false;
					t_errorstring = "error";
				}
			}

			//結果。
			if(t_result == true){
				this.SetResultSaveEnd();
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ロードローカル。ＰＮＧファイル。
		*/
		private IEnumerator Raw_Do_LoadLocalPngFile()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;
			Tool.Log(this.request_type.ToString(),t_full_path);

			Texture2D t_result = null;
			string t_errorstring = null;

			{
				//キャンセルトークン。
				NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = Task_LoadLocalPngFile.Run(this,t_full_path,t_cancel_token);

				//終了待ち。
				do{
					//プログレス。
					this.SetResultProgress(0.5f);

					//キャンセル。
					if((this.cancel_flag == true)||(this.IsDeleteRequest() == true)){
						t_cancel_token.Cancel();
					}

					yield return null;
				}while(t_task.IsEnd() == false);

				byte[] t_result_binary = null;

				Tool.Log("Raw_Do_LoadLocalPngFile","Completed = " + t_task.IsCompleted() + " Canceled = " + t_task.IsCanceled().ToString() + " Faulted = " + t_task.IsFaulted().ToString());
				if(t_task.IsSuccess()){
					t_result_binary = t_task.GetResult();
				}else{
					t_result_binary = null;
					t_errorstring = "error";
				}

				//TODO:busy
				if(t_result_binary != null){
					int t_width;
					int t_height;
					this.GetSizeFromPngBinary(t_result_binary,out t_width,out t_height);
					t_result = new Texture2D(t_width,t_height);
					if(t_result.LoadImage(t_result_binary) == false){
						t_result = null;
						t_errorstring = "error : LoadImage";
					}
				}
			}

			//結果。
			if(t_result != null){
				this.SetResultTexture(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}
	}
}

