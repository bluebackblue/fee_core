

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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
		private System.Collections.Generic.List<WorkItem> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<WorkItem> add_list;

		/** [シングルトン]constructor
		*/
		private SoundPool()
		{
			//main_file
			this.main_file = new Main_File();

			//work_list
			this.work_list = new System.Collections.Generic.List<WorkItem>();

			//add_list
			this.add_list = new System.Collections.Generic.List<WorkItem>();
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

			管理ファイルを直接ロード。

		*/
		public Item RequestLoadLocalSoundPool(Fee.File.Path a_relative_path)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadLocalSoundPool(a_relative_path);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。ロードストリーミングアセット。サウンドプール。

			ロード後ローカルセーブ。

		*/
		public Item RequestLoadStreamingAssetsSoundPool(Fee.File.Path a_relative_path,uint a_data_version)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadStreamingAssetsSoundPool(a_relative_path,a_data_version);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
		}

		/** リクエスト。ロードＵＲＬ。サウンドプール。

			ロード後ローカルセーブ。

		*/
		public Item RequestLoadUrlSoundPool(File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler,uint a_data_version)
		{
			WorkItem t_work_item = new WorkItem();
			t_work_item.RequestLoadUrlBinaryFile(a_path,a_post_data,a_certificate_handler,a_data_version);
			this.add_list.Add(t_work_item);
			return t_work_item.GetItem();
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
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

