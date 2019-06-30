

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundlePackList_AssetBundleItem
	*/
	public class AssetBundlePackList_AssetBundleItem
	{
		/** assetbundle
		*/
		public UnityEngine.AssetBundle assetbundle;

		/** assetbundle_dummy
		*/
		public Fee.AssetBundleList.DummryAssetBundle assetbundle_dummy;

		/** constructor
		*/
		public AssetBundlePackList_AssetBundleItem(UnityEngine.AssetBundle a_assetbundle)
		{
			this.assetbundle = a_assetbundle;
		}

		/** constructor
		*/
		public AssetBundlePackList_AssetBundleItem(Fee.AssetBundleList.DummryAssetBundle a_assetbundle_dummy)
		{
			this.assetbundle_dummy = a_assetbundle_dummy;
		}

		/** アンロード。
		*/
		public void Unload()
		{
			if(this.assetbundle != null){
				this.assetbundle.Unload(false);
				this.assetbundle = null;
			}
			if(this.assetbundle_dummy != null){
				this.assetbundle_dummy = null;
			}
		}
	}
}

