using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** SaveLoad
	*/
	public class SaveLoad : Config
	{
		/** [シングルトン]s_instance
		*/
		private static SaveLoad s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new SaveLoad();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static SaveLoad GetInstance()
		{
			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** work_list
		*/
		private List<Work> work_list;

		/** request_current
		*/
		private Work request_current;

		/** [シングルトン]constructor
		*/
		private SaveLoad()
		{
			this.work_list = new List<Work>();
			this.request_current = null;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				if(this.request_current == null){
					if(this.work_list[0].Main() == true){
						this.work_list.RemoveAt(0);
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** セーブローカル。テキストファイル。リクエスト。
		*/
		public Item SaveLocalTextFileRequest(string a_filename,string a_text)
		{
			Work t_work = new Work();
			t_work.SaveLocal_TextFile(a_filename,a_text);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードローカル。テキストファイル。リクエスト。
		*/
		public Item LoadLoaclTextFileRequest(string a_filename)
		{
			Work t_work = new Work();
			t_work.LoadLocal_TextFile(a_filename);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** セーブローカル。ＰＮＧファイル。リクエスト。
		*/
		public Item SaveLocalPngFileRequest(string a_filename,Texture2D a_texture)
		{
			Work t_work = new Work();
			t_work.SaveLocal_PngFile(a_filename,a_texture);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** ロードローカル。ＰＮＧファイル。リクエスト。
		*/
		public Item LoadLocalPngFileRequest(string a_filename)
		{
			Work t_work = new Work();
			t_work.LoadLocal_PngFile(a_filename);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}
	}
}

