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

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static SaveLoad GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

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

		/** io
		*/
		private GameObject io_gameobject;
		private MonoBehaviour_Io io_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** [シングルトン]constructor
		*/
		private SaveLoad()
		{
			//io
			{
				this.io_gameobject = new GameObject();
				this.io_gameobject.name = "SaveLoad";
				this.io_script = this.io_gameobject.AddComponent<MonoBehaviour_Io>();

				GameObject.DontDestroyOnLoad(this.io_gameobject);
			}

			//work_list
			this.work_list = new List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.io_script.DeleteRequest();
		}

		/** Io。取得。
		*/
		public MonoBehaviour_Io GetIo()
		{
			return this.io_script;
		}

		/** リクエスト。セーブローカル。バイナリファイル。
		*/
		public Item RequestSaveLocalBinaryFile(string a_filename,byte[] a_binary)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalBinaryFile(a_filename,a_binary);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public Item RequestLoadLoaclBinaryFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalBinaryFile(a_filename);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}
	
		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public Item RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextFile(a_filename,a_text);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public Item RequestLoadLoaclTextFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextFile(a_filename);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。ＰＮＧファイル。
		*/
		public Item RequestSaveLocalPngFile(string a_filename,Texture2D a_texture)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalPngFile(a_filename,a_texture);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。ＰＮＧファイル。
		*/
		public Item RequestLoadLocalPngFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalPngFile(a_filename);

			this.work_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				if(this.work_list.Count > 0){
					if(this.work_list[0].Main() == true){
						this.work_list.RemoveAt(0);
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

