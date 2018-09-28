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

		/** binary
		*/
		private byte[] binary;

		/** text
		*/
		private string text;

		/** texture
		*/
		private Texture2D texture;

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

			//binary
			this.binary = null;

			//text
			this.text = null;

			//texture
			this.texture = null;
		}

		/** セーブローカル。バイナリファイル。
		*/
		public void RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			this.type = Type.SaveLocalBinaryFile;
			this.filename = a_filename;
			this.binary = a_binary;
		}

		/** ロードローカル。バイナリファイル。
		*/
		public void RequestLoadLocalBinaryFile(string a_filename)
		{
			this.type = Type.LoadLocalBinaryFile;
			this.filename = a_filename;
		}

		/** セーブローカル。テキストファイル。
		*/
		public void RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			this.type = Type.SaveLocalTextFile;
			this.filename = a_filename;
			this.text = a_text;
		}

		/** ロードローカル。テキストファイル。
		*/
		public void RequestLoadLocalTextFile(string a_filename)
		{
			this.type = Type.LoadLocalTextFile;
			this.filename = a_filename;
		}

		/** セーブローカル。ＰＮＧファイル。
		*/
		public void RequestSaveLocalPngFile(string a_filename,Texture2D a_texture)
		{
			this.type = Type.SaveLocalPngFile;
			this.filename = a_filename;
			this.texture = a_texture;
		}

		/** ロードローカル。ＰＮＧファイル。
		*/
		public void RequestLoadLocalPngFile(string a_filename)
		{
			this.type = Type.LoadLocalPngFile;
			this.filename = a_filename;
		}

		/** アイテム。
		*/
		public Item GetItem()
		{
			return this.item;
		}

		/** リクエスト。
		*/
		private bool Request()
		{
			MonoBehaviour_Io t_io = NSaveLoad.SaveLoad.GetInstance().GetIo();

			switch(this.type){
			case Type.SaveLocalBinaryFile:
				{
				}return t_io.RequestSaveLocalBinaryFile(this.filename,this.binary);
			case Type.LoadLocalBinaryFile:
				{
				}return t_io.RequestLoadLocalBinaryFile(this.filename);
			case Type.SaveLocalTextFile:
				{
				}return t_io.RequestSaveLocalTextFile(this.filename,this.text);
			case Type.LoadLocalTextFile:
				{
				}return t_io.RequestLoadLocalTextFile(this.filename);
			case Type.SaveLocalPngFile:
				{
				}return t_io.RequestSaveLocalPngFile(this.filename,this.texture);
			case Type.LoadLocalPngFile:
				{
				}return t_io.RequestLoadLocalPngFile(this.filename);
			}

			return false;
		}

		/** 更新。

		戻り値 = true : 完了。

		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Start:
				{
					if(this.Request() == true){
						//開始。
						this.mode = Mode.Do;
					}
				}break;
			case Mode.Do:
				{
					MonoBehaviour_Io t_io = NSaveLoad.SaveLoad.GetInstance().GetIo();

					if(t_io.IsFix() == false){
						//処理中。
						this.item.SetResultProgress(t_io.GetResultProgress());
					}else{

						//結果。
						switch(t_io.GetResultDataType()){
						case DataType.Binary:
							{
								//バイナリ。
								this.item.SetResultBinary(t_io.GetResultBinary());
							}break;
						case DataType.Text:
							{
								//テキスト。
								this.item.SetResultText(t_io.GetResultText());
							}break;
						case DataType.Texture:
							{
								//テクスチャ。
								this.item.SetResultTexture(t_io.GetResultTexture());
							}break;
						case DataType.SaveEnd:
							{
								//セーブ完了。
								this.item.SetResultSaveEnd();
							}break;
						default:
							{
								this.item.SetResultError();
							}break;
						}

						//リクエスト待ち開始。
						t_io.WaitRequest();

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

