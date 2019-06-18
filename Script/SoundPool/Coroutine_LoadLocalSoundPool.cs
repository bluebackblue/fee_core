

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief サウンドプール。コルーチン。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** ロードローカル。サウンドプール。
	*/
	public class Coroutine_LoadLocalSoundPool
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** サウンドプール。
			*/
			public Fee.Audio.Pack_SoundPool soundpool;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.soundpool = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//ローカル、サウンドプール管理ファイルのロード。
			Fee.JsonItem.JsonItem t_local_soundpool_json = null;
			{
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoadLocalTextFile(a_path);
				while(t_item.IsBusy() == true){
					yield return null;
				}
				if(t_item.GetResultType()==File.Item.ResultType.Text){
					t_local_soundpool_json = new JsonItem.JsonItem(t_item.GetResultText());
					if(t_local_soundpool_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_LoadLocalSoundPool : local_soundpool_json == null";
						yield break;
					}
				}else{
					//ローカルにキャッシュなし。
				}
			}

			Fee.Audio.Pack_SoundPool t_local_soundpool = Fee.JsonItem.Convert.JsonItemToObject<Fee.Audio.Pack_SoundPool>(t_local_soundpool_json);
			if(t_local_soundpool == null){
				//コンバート失敗。
				this.result.errorstring = "Coroutine_LoadLocalSoundPool : local_soundpool == null";
				yield break;
			}

			//パス解決。
			{
				t_local_soundpool.fullpath_list = new System.Collections.Generic.List<File.Path>();
				for(int ii=0;ii<t_local_soundpool.name_list.Count;ii++){
					t_local_soundpool.fullpath_list.Add(File.Path.CreateLocalPath(t_local_soundpool.name_list[ii]));
				}
			}

			this.result.soundpool = t_local_soundpool;
			yield break;
		}
	}
}

