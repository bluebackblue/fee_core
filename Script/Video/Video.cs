

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ムービー。
*/


/** Fee.Video
*/
namespace Fee.Video
{
	/** Video
	*/
	public class Video : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Video s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Video();
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
		public static Video GetInstance()
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
		private UnityEngine.GameObject root_gameobject;

		/** リスト。
		*/
		private System.Collections.Generic.HashSet<Fee.Video.Item> list;

		/** [シングルトン]constructor
		*/
		private Video()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Video";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			//list
			this.list = new System.Collections.Generic.HashSet<Item>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			while(this.list.Count > 0){
				Tool.Assert(false);

				Item t_item = this.list.GetEnumerator().Current;
				t_item.OnDelete();
			}

			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** GetRootGameObject
		*/
		public UnityEngine.GameObject GetRootGameObject()
		{
			return this.root_gameobject;
		}

		/** 追加。
		*/
		public void Regist(Fee.Video.Item a_item)
		{
			this.list.Add(a_item);
		}

		/** 削除。
		*/
		public void UnRegist(Fee.Video.Item a_item)
		{
			bool t_ret = this.list.Remove(a_item);
			Tool.Assert(t_ret == true);
		}
	}
}

