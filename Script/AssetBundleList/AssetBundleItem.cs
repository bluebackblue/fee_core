

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドルアイテム。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundleItem
	*/
	public class AssetBundleItem
	{
		/** pathitem
		*/
		public PathItem pathitem;

		/** assetbundle_raw
		*/
		public UnityEngine.AssetBundle assetbundle_raw;

		/** assetbundle_dummy
		*/
		public Fee.AssetBundleList.DummryAssetBundle assetbundle_dummy;

		/** constructor
		*/
		public AssetBundleItem(UnityEngine.AssetBundle a_assetbundle_raw,PathItem a_pathitem)
		{
			//pathitem
			this.pathitem = a_pathitem;

			//assetbundle_raw
			this.assetbundle_raw = a_assetbundle_raw;

			//assetbundle_dummy
			this.assetbundle_dummy = null;
		}

		/** constructor
		*/
		public AssetBundleItem(Fee.AssetBundleList.DummryAssetBundle a_assetbundle_dummy,PathItem a_pathitem)
		{
			//pathitem
			this.pathitem = a_pathitem;

			//assetbundle_raw
			this.assetbundle_raw = null;

			//assetbundle_dummy
			this.assetbundle_dummy = a_assetbundle_dummy;
		}

		/** アンロード。
		*/
		public void Unload()
		{
			//assetbundle_raw
			if(this.assetbundle_raw != null){
				this.assetbundle_raw.Unload(false);
				this.assetbundle_raw = null;
			}

			//assetbundle_dummy
			if(this.assetbundle_dummy != null){
				this.assetbundle_dummy = null;
			}
		}
	}
}

