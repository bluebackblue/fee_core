

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。コルーチン。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** ロードローカル。パック。
	*/
	public class Coroutine_LoadLocalPack
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** パック。
			*/
			public Pack pack;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.pack = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(OnSoundPoolCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//ローカル、パックＪＳＯＮのロード。
			Fee.JsonItem.JsonItem t_local_pack_json = null;
			{
				Fee.File.Item t_item = Fee.File.File.GetInstance().RequestLoad(File.File.LoadRequestType.LoadLocalTextFile,a_path);

				do{
					if(a_callback_interface != null){
						a_callback_interface.OnSoundPoolCoroutine(t_item.GetResultProgress());
					}
					yield return null;
				}while(t_item.IsBusy() == true);

				if(t_item.GetResultAssetType() == Asset.AssetType.Text){
					t_local_pack_json = new JsonItem.JsonItem(t_item.GetResultAssetText());
					if(t_local_pack_json == null){
						//コンバート失敗。
						this.result.errorstring = "Coroutine_LoadLocalPack : local_pack_json == null";
						yield break;
					}
				}else{
					//ローカルにキャッシュなし。
				}
			}

			Pack t_local_pack = Fee.JsonItem.Convert.JsonItemToObject<Pack>(t_local_pack_json);
			if(t_local_pack == null){
				//コンバート失敗。
				this.result.errorstring = "Coroutine_LoadLocalPack : local_pack == null";
				yield break;
			}

			//パス解決。
			{
				t_local_pack.fullpath_list = new System.Collections.Generic.List<File.Path>();
				for(int ii=0;ii<t_local_pack.name_list.Count;ii++){
					t_local_pack.fullpath_list.Add(File.Path.CreateLocalPath(t_local_pack.name_list[ii],Fee.File.Path.SEPARATOR));
				}
			}

			this.result.pack = t_local_pack;
			yield break;
		}
	}
}

