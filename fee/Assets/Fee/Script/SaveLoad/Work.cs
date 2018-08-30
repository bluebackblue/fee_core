using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。ワーク。
*/


/** NSaveLoad
*/
namespace NSaveLoad
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

			/**	実行中。
			*/
			Do,
	
			/** 完了。
			*/
			End
		};

		/** タイプ。
		*/
		public enum Type
		{
			None = -1,

			/** セーブローカル。テキストファイル。
			*/
			SaveLocal_TextFile,

			/** ロードローカル。テキストファイル。
			*/
			LoadLocal_TextFile,

			/** セーブローカル。ＰＮＧファイル。
			*/
			SaveLocal_PngFile,

			/** ロードローカル。ＰＮＧファイル。
			*/
			LoadLocal_PngFile,
		}

		/** mode
		*/
		private Mode mode;

		/** type
		*/
		private Type type;

		/** item
		*/
		private Item item;

		/** filename
		*/
		private string filename;

		/** text
		*/
		private string text;

		/** constructor
		*/
		public Work()
		{
			//mode
			this.mode = Mode.Start;

			//type
			this.type = Type.None;

			//item
			this.item = new Item();

			//filename
			this.filename = null;

			//text
			this.text = null;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** セーブローカル。テキストファイル。
		*/
		public bool SaveLocal_TextFile(string a_filename,string a_text)
		{
			string t_full_path = Application.persistentDataPath + "/" + a_filename;

			bool t_ret = false;

			if(a_text != null){
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(t_full_path);
				if(t_fileinfo != null){
					System.IO.StreamWriter t_stream_writer = null;

					//open
					try{
						t_stream_writer = t_fileinfo.CreateText();
					}catch(System.Exception /*t_exception*/){
						//Tool.LogError(t_exception);
					}

					//write
					if(t_stream_writer != null){
						t_stream_writer.Write(a_text);
						t_stream_writer.Flush();

						t_ret = true;
					}

					//close
					if(t_stream_writer != null){
						t_stream_writer.Close();
					}
				}
			}

			return t_ret;
		}

		/** ロードローカル。テキストファイル。
		*/
		public string LoadLocal_TextFile(string a_filename)
		{
			string t_full_path = Application.persistentDataPath + "/" + a_filename;

			string t_ret = null;

			{
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(t_full_path);
				if(t_fileinfo != null){
					System.IO.StreamReader t_stream_reader = null;

					//open
					try{
						t_stream_reader = t_fileinfo.OpenText();
					}catch(System.Exception /*t_exception*/){
						//Tool.LogError(t_exception);
					}

					//read
					if(t_stream_reader != null){
						t_ret = t_stream_reader.ReadToEnd();
					}

					//close
					if(t_stream_reader != null){
						t_stream_reader.Close();
					}
				}
			}

			return t_ret;
		}

		/** セーブローカル。ＰＮＧファイル。
		*/
		public bool SaveLocal_PngFile(string a_filename,Texture2D a_texture)
		{
			string t_full_path = Application.persistentDataPath + "/" + a_filename;

			bool t_ret = false;

			if(a_texture != null){
				//TODO:
			}

			return t_ret;
		}

		/** ロードローカル。ＰＮＧファイル。
		*/
		public Texture2D LoadLocal_PngFile(string a_filename)
		{
			string t_full_path = Application.persistentDataPath + "/" + a_filename;

			Texture2D t_ret = null;

			{
				//TODO:
			}

			return t_ret;
		}

		/** 開始。

		TODO:MonoBehaviour化。

		*/
		public void Start()
		{
			switch(this.type){
			case Type.SaveLocal_TextFile:
				{
					bool t_ret = this.SaveLocal_TextFile(this.filename,this.text);
					if(t_ret == true){
						this.item.SetResultSaveEnd();
					}
				}break;
			case Type.LoadLocal_TextFile:
				{
					string t_text = this.LoadLocal_TextFile(this.filename);
					if(t_text != null){
						this.item.SetResultText(t_text);
					}else{
						this.item.SetResultError();
					}
				}break;
			case Type.SaveLocal_PngFile:
				{
					bool t_ret = this.SaveLocal_TextFile(this.filename,this.text);
					if(t_ret == true){
						this.item.SetResultSaveEnd();
					}
				}break;
			case Type.LoadLocal_PngFile:
				{
					string t_text = this.LoadLocal_TextFile(this.filename);
					if(t_text != null){
						this.item.SetResultText(t_text);
					}else{
						this.item.SetResultError();
					}
				}break;
			}
		}

		/** 実行中。

		TODO:MonoBehaviour化。

		戻り値 = true : 完了。

		*/
		public bool Do()
		{
			return true;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					this.Start();
				}break;
			case Mode.Do:
				{
					if(this.Do() == true){
						this.mode = Mode.End;
					}
				}break;
			case Mode.End:
				{
				}return true;
			}

			return false;
		}
	}
}

