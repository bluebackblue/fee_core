

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。パックアイテム。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** PackItem
	*/
	public class PackItem
	{
		/** assetbundle_raw
		*/
		public UnityEngine.AssetBundle assetbundle_raw;

		/** assetbundle_dummy
		*/
		public Fee.AssetBundleList.DummryAssetBundle assetbundle_dummy;

		/** constructor
		*/
		public PackItem(UnityEngine.AssetBundle a_assetbundle_raw)
		{
			//assetbundle_raw
			this.assetbundle_raw = a_assetbundle_raw;

			//assetbundle_dummy
			this.assetbundle_dummy = null;
		}

		/** constructor
		*/
		public PackItem(Fee.AssetBundleList.DummryAssetBundle a_assetbundle_dummy)
		{
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

