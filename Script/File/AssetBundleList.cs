

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。アセットバンドルリスト。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** AssetBundleList
	*/
	public class AssetBundleList
	{
		/** list
		*/
		private System.Collections.Generic.Dictionary<long,UnityEngine.AssetBundle> list;

		/** constructor
		*/
		public AssetBundleList()
		{
			this.list = new System.Collections.Generic.Dictionary<long,UnityEngine.AssetBundle>();
		}

		/** リスト数。
		*/
		public int GetCount()
		{
			return this.list.Count;
		}

		/** アセットバンドル。登録。
		*/
		public void Regist(long a_assetbundle_id,UnityEngine.AssetBundle a_assetbundle)
		{
			this.list.Add(a_assetbundle_id,a_assetbundle);
		}

		/** アセットバンドル。アンロード。
		*/
		public void UnloadAssetBundle(long a_assetbundle_id)
		{
			UnityEngine.AssetBundle t_assetbundle = this.GetAssetBundle(a_assetbundle_id);

			this.list.Remove(a_assetbundle_id);

			if(t_assetbundle != null){
				Tool.Log("UnloadAssetbundle",string.Format("{0:X}",a_assetbundle_id));
				t_assetbundle.Unload(false);
			}
		}

		/** アセットバンドル。取得。
		*/
		public UnityEngine.AssetBundle GetAssetBundle(long a_assetbundle_id)
		{
			if(this.list.TryGetValue(a_assetbundle_id,out UnityEngine.AssetBundle t_assetbundle) == true){
				return t_assetbundle;
			}

			return null;
		}

		/** アセットバンドル。アンロード。
		*/
		public void UnloadAllAssetBundle()
		{
			System.Collections.Generic.Dictionary<long,UnityEngine.AssetBundle>.KeyCollection t_collection = this.list.Keys;
			long[] t_keylist = new long[t_collection.Count];
			t_collection.CopyTo(t_keylist,0);

			for(int ii=0;ii<t_keylist.Length;ii++){
				this.UnloadAssetBundle(t_keylist[ii]);
			}
		}
	}
}

