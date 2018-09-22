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

		/** datatype
		*/
		[SerializeField]
		private DataType datatype;

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

			//datatype
			this.datatype = DataType.None;

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

				this.datatype = DataType.None;
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

				this.datatype = DataType.None;
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

				this.datatype = DataType.None;
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

				this.datatype = DataType.None;
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

				this.datatype = DataType.None;
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

				this.datatype = DataType.None;
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

		/** データタイプ。取得。
		*/
		public DataType GetDataType()
		{
			return this.datatype;
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

			DataType t_ret = DataType.Error;

			if(this.request_type == RequestType.SaveLocalBinaryFile){
				//セーブ。バイナリ。

				System.IO.FileStream t_filestream = null;

				//open
				try{
					t_filestream = t_fileinfo.Create();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//write
				if(t_filestream != null){
					System.IO.BinaryWriter t_binarwriter = new System.IO.BinaryWriter(t_filestream);
					t_binarwriter.Write(this.request_binary,0,this.request_binary.Length);
					t_binarwriter.Close();

					t_ret = DataType.SaveEnd;
				}

				//close
				if(t_filestream != null){
					t_filestream.Close();
				}
			}else if(this.request_type == RequestType.LoadLocalBinaryFile){
				//ロード。バイナリ。

				System.IO.FileStream t_filestream = null;

				//open
				try{
					t_filestream = t_fileinfo.OpenRead();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//read
				if(t_filestream != null){
					System.IO.BinaryReader t_binaryreader = new System.IO.BinaryReader(t_filestream);

					this.result_binary = t_binaryreader.ReadBytes((int)t_filestream.Length);

					t_ret = DataType.Binary;
				}

				//close
				if(t_filestream != null){
					t_filestream.Close();
				}
			}else if(this.request_type == RequestType.SaveLocalTextFile){
				//セーブ。テキスト。

				System.IO.StreamWriter t_stream_writer = null;

				//open
				try{
					t_stream_writer = t_fileinfo.CreateText();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//write
				if(t_stream_writer != null){
					t_stream_writer.Write(this.request_text);
					t_stream_writer.Flush();

					t_ret = DataType.SaveEnd;
				}

				//close
				if(t_stream_writer != null){
					t_stream_writer.Close();
				}
			}else if(this.request_type == RequestType.LoadLocalTextFile){
				//ロード。テキスト。

				System.IO.StreamReader t_stream_reader = null;

				//open
				try{
					t_stream_reader = t_fileinfo.OpenText();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//read
				if(t_stream_reader != null){
					this.result_text = t_stream_reader.ReadToEnd();
					t_ret = DataType.Text;
				}

				//close
				if(t_stream_reader != null){
					t_stream_reader.Close();
				}
			}else if(this.request_type == RequestType.SaveLocalPngFile){
				//セーブ。ＰＮＧ。

				System.IO.FileStream t_filestream = null;

				//open
				try{
					t_filestream = t_fileinfo.Create();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//write
				if(t_filestream != null){
					byte[] t_byte = this.request_texture.EncodeToPNG();

					System.IO.BinaryWriter t_binarwriter = new System.IO.BinaryWriter(t_filestream);
					t_binarwriter.Write(t_byte,0,t_byte.Length);
					t_binarwriter.Close();

					t_ret = DataType.SaveEnd;
				}

				//close
				if(t_filestream != null){
					t_filestream.Close();
				}
			}else if(this.request_type == RequestType.LoadLocalPngFile){
				//ロード。ＰＮＧ。

				System.IO.FileStream t_filestream = null;

				//open
				try{
					t_filestream = t_fileinfo.OpenRead();
				}catch(System.Exception /*t_exception*/){
					//Tool.LogError(t_exception);
				}

				yield return null;

				//read
				if(t_filestream != null){
					System.IO.BinaryReader t_binaryreader = new System.IO.BinaryReader(t_filestream);

					byte[] t_bytes = t_binaryreader.ReadBytes((int)t_filestream.Length);

					int t_width;
					int t_height;
					this.GetSizeFromPngBinary(t_bytes,out t_width,out t_height);
					this.result_texture = new Texture2D(t_width,t_height);
					this.result_texture.LoadImage(t_bytes);

					t_ret = DataType.Texture;
				}

				//close
				if(t_filestream != null){
					t_filestream.Close();
				}
			}

			this.datatype = t_ret;

			yield break;
		}
	}
}

