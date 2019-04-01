

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief サウンドプール。ツール。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** SoundPool
	*/
	public class SoundPool
	{
		/** [シングルトン]s_instance
		*/
		private static SoundPool s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new SoundPool();
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
		public static SoundPool GetInstance()
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

		/** main_file
		*/
		private Main_File main_file;

		/** work_list
		*/
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** [シングルトン]constructor
		*/
		private SoundPool()
		{
			//main_file
			this.main_file = new Main_File();

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** main_file。取得。
		*/
		public Main_File GetMainFile()
		{
			return this.main_file;
		}

		/** リクエスト。ロードローカル。サウンドプール。
		*/
		public Item RequestLoadLocalSoundPool(Fee.File.Path a_path)
		{
			Work t_work = new Work();
			t_work.RequestLoadLocalSoundPool(a_path);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。セーブローカル。サウンドプール。
		*/
		public Item RequestSaveLocalSoundPool(Fee.File.Path a_path,Fee.Audio.Pack_SoundPool a_soundpool)
		{
			Work t_work = new Work();
			t_work.RequestSaveLocalSoundPool(a_path,a_soundpool);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ダウンロード。サウンドプール。
		*/
		public Item RequestDownLoadSoundPool(File.Path a_path,UnityEngine.WWWForm a_post_data,uint a_data_version)
		{
			Work t_work = new Work();
			t_work.RequestDownLoadBinaryFile(a_path,a_post_data,a_data_version);
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

