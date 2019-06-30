

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
	/** アンロードパス。アセットバンドルアイテム。
	*/
	public class Coroutine_UnloadPathAssetBundleItem
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
		public System.Collections.IEnumerator CoroutineMain(Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface a_callback_interface,string a_id)
		{
			//result
			this.result = new ResultType();

			AssetBundlePackList_AssetBundleItem t_assetbundle_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_id);

			if(t_assetbundle_item == null){
				//失敗。
				this.result.errorstring = "Coroutine_UnloadPathAssetBundleItem : Not Found ID : " + a_id;
				yield break;
			}

			//Unload
			if(t_assetbundle_item.assetbundle != null){
				t_assetbundle_item.Unload();
			}

			//成功。
			this.result.unload = true;
			yield break;
		}
	}
}

