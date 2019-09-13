

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。コルーチン。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** アンロード。パックアイテム。
	*/
	public class Coroutine_UnLoadPackItem
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** unload
			*/
			public bool unload;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//unload
				this.unload = false;

				//errorstring
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_assetbundle_name)
		{
			//result
			this.result = new ResultType();

			PackItem t_pack_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetPackItem(a_assetbundle_name);

			if(t_pack_item == null){
				//失敗。
				this.result.errorstring = "Coroutine_UnLoadPackItem : Not Found ID : " + a_assetbundle_name;
				yield break;
			}

			//Unload
			t_pack_item.Unload();

			//解除。
			Fee.AssetBundleList.AssetBundleList.GetInstance().UnRegistPackItem(a_assetbundle_name);

			//成功。
			this.result.unload = true;
			yield break;
		}
	}
}

