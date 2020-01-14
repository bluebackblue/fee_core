

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
	/** アンロード。アセットバンドルアイテム。
	*/
	public class Coroutine_UnLoadAssetBundleItem
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

			AssetBundleItem t_assetbundle_item = Fee.AssetBundleList.AssetBundleList.GetInstance().GetAssetBundleItem(a_assetbundle_name);

			if(t_assetbundle_item == null){
				//失敗。
				this.result.errorstring = "Coroutine_UnLoadAssetBundleItem : Not Found ID : " + a_assetbundle_name;
				yield break;
			}

			//Unload
			t_assetbundle_item.Unload();

			//解除。
			Fee.AssetBundleList.AssetBundleList.GetInstance().UnRegistAssetBundleItem(a_assetbundle_name);

			//成功。
			this.result.unload = true;
			yield break;
		}
	}
}

