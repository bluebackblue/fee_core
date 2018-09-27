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

		/** 処理中チェック。
		*/
		public bool IsBusy()
		{
			if(this.mode == Mode.WaitRequest){
				return false;
			}
			return true;
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
		private void SetProgressFromTask(float a_progress)
		{
			this.result_progress = a_progress;
		}

		/** [タスク]セーブローカル。バイナリファイル。
		*/
		private async System.Threading.Tasks.Task<bool> Task_Do_SaveLocalBinaryFile(string a_full_path,byte[] a_binary,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			bool t_ret = true;

			//開く。
			System.IO.FileStream t_filestream = null;
			try{
				t_filestream = t_fileinfo.Create();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalBinaryFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//書き込み。
			try{
				if(t_filestream != null){
					await t_filestream.WriteAsync(a_binary,0,a_binary.Length,a_cancel);
					await t_filestream.FlushAsync(a_cancel);
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalBinaryFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** [タスク]ロードローカル。バイナリファイル。
		*/
		private async System.Threading.Tasks.Task<byte[]> Task_Do_LoadLocalBinaryFile(string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			byte[] t_ret = null;

			//開く。
			System.IO.FileStream t_filestream = null;
			try{
				t_filestream = t_fileinfo.OpenRead();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalBinaryFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//書き込み。
			try{
				if(t_filestream != null){
					t_ret = new byte[t_filestream.Length];
					await t_filestream.ReadAsync(t_ret,0,t_ret.Length,a_cancel);
					await t_filestream.FlushAsync(a_cancel);
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalBinaryFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** [タスク]セーブローカル。テキストファイル。
		*/
		private async System.Threading.Tasks.Task<bool> Task_Do_SaveLocalTextFile(string a_full_path,string a_text,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			bool t_ret = true;

			//開く。
			System.IO.StreamWriter t_filestream = null;
			try{
				t_filestream = t_fileinfo.CreateText();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalTextFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//書き込み。
			try{
				if(t_filestream != null){
					//TODO:キャンセルトークン渡せない。
					await t_filestream.WriteAsync(a_text);
					await t_filestream.FlushAsync();
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalTextFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** [タスク]ロードローカル。テキストファイル。
		*/
		private async System.Threading.Tasks.Task<string> Task_Do_LoadLocalTextFile(string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			string t_ret = null;

			//開く。
			System.IO.StreamReader t_filestream = null;
			try{
				t_filestream = t_fileinfo.OpenText();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalTextFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//読み込み。
			try{
				if(t_filestream != null){
					//TODO:キャンセルトークン渡せない。
					t_ret = await t_filestream.ReadToEndAsync();
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalTextFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** [タスク]セーブローカル。ＰＮＧファイル。
		*/
		private async System.Threading.Tasks.Task<bool> Task_Do_SaveLocalPngFile(string a_full_path,byte[] a_binary,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			bool t_ret = true;

			//開く。
			System.IO.FileStream t_filestream = null;
			try{
				t_filestream = t_fileinfo.Create();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalPngFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//書き込み。
			try{
				if(t_filestream != null){
					await t_filestream.WriteAsync(a_binary,0,a_binary.Length,a_cancel);
					await t_filestream.FlushAsync(a_cancel);
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_SaveLocalPngFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = false;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** [タスク]ロードローカル。ＰＮＧファイル。
		*/
		private async System.Threading.Tasks.Task<byte[]> Task_Do_LoadLocalPngFile(string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			byte[] t_ret = null;

			//開く。
			System.IO.FileStream t_filestream = null;
			try{
				t_filestream = t_fileinfo.OpenRead();
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalPngFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//TODO:プログレス。
			NTaskW.TaskW.GetInstance().GetTaskSync().Post((a_state) => {SetProgressFromTask(0.0f);},null);

			//書き込み。
			try{
				if(t_filestream != null){
					t_ret = new byte[t_filestream.Length];
					await t_filestream.ReadAsync(t_ret,0,t_ret.Length,a_cancel);
					await t_filestream.FlushAsync(a_cancel);
				}
			}catch(System.Exception t_exception){
				Tool.Log("Task_Do_LoadLocalPngFile",t_exception.StackTrace + "\n\n" + t_exception.Message);
				t_ret = null;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			return t_ret;
		}

		/** Start
		*/
		private IEnumerator Start()
		{
			while(this.delete_flag == false){
				switch(this.mode){
				case Mode.WaitRequest:
					{
					}break;
				case Mode.Start:
					{
						if(this.request_type != RequestType.None){
							//リクエストあり。
							Tool.Log("MonoBehaviour_Io",this.request_type.ToString());

							this.mode = Mode.Do;
						}
					}break;
				case Mode.Do:
					{
						yield return this.Do();

						this.mode = Mode.WaitRequest;
					}break;
				}

				yield return null;
			}

			Tool.Log("SaveLoad","GameObject.Destroy");
			GameObject.Destroy(this.gameObject);
		}

		/** 実行。
		*/
		private IEnumerator Do()
		{
			string t_full_path = Application.persistentDataPath + "/" + this.request_filename;

			Tool.Log("fiename",t_full_path);

			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(t_full_path);

			if(this.request_type == RequestType.SaveLocalBinaryFile){
				//セーブローカル。バイナリファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = new NTaskW.Task<bool>(() => {return this.Task_Do_SaveLocalBinaryFile(t_full_path,this.request_binary,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);
			
				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() == true){
					this.result_datatype = DataType.SaveEnd;
				}else{
					this.result_datatype = DataType.Error;
				}
			}else if(this.request_type == RequestType.LoadLocalBinaryFile){
				//ロードローカル。バイナリファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = new NTaskW.Task<byte[]>(() => {return this.Task_Do_LoadLocalBinaryFile(t_full_path,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() != null){
					this.result_binary = t_task.GetResult();
					this.result_datatype = DataType.Binary;
				}else{
					this.result_datatype = DataType.Error;
				}
			}else if(this.request_type == RequestType.SaveLocalTextFile){
				//セーブローカル。テキストファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//タスク起動。
				NTaskW.Task<bool> t_task = new NTaskW.Task<bool>(() => {return this.Task_Do_SaveLocalTextFile(t_full_path,this.request_text,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() == true){
					this.result_datatype = DataType.SaveEnd;
				}else{
					this.result_datatype = DataType.Error;
				}
			}else if(this.request_type == RequestType.LoadLocalTextFile){
				//ロードローカル。テキストファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//タスク起動。
				NTaskW.Task<string> t_task = new NTaskW.Task<string>(() => {return this.Task_Do_LoadLocalTextFile(t_full_path,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() != null){
					this.result_text = t_task.GetResult();
					this.result_datatype = DataType.Text;
				}else{
					this.result_datatype = DataType.Error;
				}
			}else if(this.request_type == RequestType.SaveLocalPngFile){
				//セーブローカル。ＰＮＧファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//TODO:busy
				byte[] t_binary = null;
				{
					t_binary = this.request_texture.EncodeToPNG();
				}

				//タスク起動。
				NTaskW.Task<bool> t_task = new NTaskW.Task<bool>(() => {return this.Task_Do_SaveLocalPngFile(t_full_path,t_binary,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() == true){
					this.result_datatype = DataType.SaveEnd;
				}else{
					this.result_datatype = DataType.Error;
				}
			}else if(this.request_type == RequestType.LoadLocalPngFile){
				//ロードローカル。ＰＮＧファイル。

				//キャンセルトークン。
				System.Threading.CancellationToken t_cancel = new System.Threading.CancellationToken();

				//タスク起動。
				NTaskW.Task<byte[]> t_task = new NTaskW.Task<byte[]>(() => {return this.Task_Do_LoadLocalPngFile(t_full_path,t_cancel);});

				//終了待ち。
				do{
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				this.result_progress = 1.0f;
				if(t_task.GetResult() != null){
					byte[] t_binary = t_task.GetResult();

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

			yield break;
		}
	}
}

