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

		/** ルート。
		*/
		public GameObject root_gameobject;
		public Transform root_transform;

		/** io
		*/
		private GameObject io_gameobject;
		private MonoBehaviour_Io io_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** add_list
		*/
		private List<Work> add_list;

		/** [シングルトン]constructor
		*/
		private SaveLoad()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "SaveLoad";
			GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<Transform>();

			//io
			{
				this.io_gameobject = new GameObject();
				this.io_gameobject.name = "SaveLoad_Io";
				this.io_script = this.io_gameobject.AddComponent<MonoBehaviour_Io>();
				this.io_gameobject.GetComponent<Transform>().SetParent(this.root_transform);
			}

			//work_list
			this.work_list = new List<Work>();

			//add_list
			this.add_list = new List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//削除リクエスト。
			this.io_gameobject.GetComponent<Transform>().SetParent(null);
			GameObject.DontDestroyOnLoad(this.io_gameobject);
			this.io_script.DeleteRequest();

			//ルート削除。
			GameObject.Destroy(this.root_gameobject);
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

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public Item RequestLoadLoaclBinaryFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalBinaryFile(a_filename);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}
	
		/** リクエスト。セーブローカル。テキストファイル。
		*/
		public Item RequestSaveLocalTextFile(string a_filename,string a_text)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalTextFile(a_filename,a_text);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public Item RequestLoadLoaclTextFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalTextFile(a_filename);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。ＰＮＧファイル。
		*/
		public Item RequestSaveLocalPngFile(string a_filename,Texture2D a_texture)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalPngFile(a_filename,a_texture);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。ＰＮＧファイル。
		*/
		public Item RequestLoadLocalPngFile(string a_filename)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalPngFile(a_filename);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.add_list.Count > 0)){
				return true;
			}
			return false;
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

