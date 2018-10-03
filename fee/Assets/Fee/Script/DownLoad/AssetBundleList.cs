using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。アセットバンドルリスト。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** AssetBundleList
	*/
	public class AssetBundleList
	{
		/** list
		*/
		Dictionary<long,AssetBundle> list;

		/** constructor
		*/
		public AssetBundleList()
		{
			this.list = new Dictionary<long,AssetBundle>();
		}

		/** リスト数。
		*/
		public int GetCount()
		{
			return this.list.Count;
		}

		/** アセットバンドル。登録。
		*/
		public void Regist(long a_assetbundle_id,AssetBundle a_assetbundle)
		{
			this.list.Add(a_assetbundle_id,a_assetbundle);
		}

		/** アセットバンドル。アンロード。
		*/
		public void UnloadAssetBundle(long a_assetbundle_id)
		{
			AssetBundle t_assetbundle = this.GetAssetBundle(a_assetbundle_id);

			this.list.Remove(a_assetbundle_id);

			if(t_assetbundle != null){
				Tool.Log("NDownLoad.AssetBundleList","UnloadAssetbundle : " + string.Format("{0:X}",a_assetbundle_id));
				t_assetbundle.Unload(false);
			}
		}

		/** アセットバンドル。取得。
		*/
		public AssetBundle GetAssetBundle(long a_assetbundle_id)
		{
			AssetBundle t_assetbundle;

			if(this.list.TryGetValue(a_assetbundle_id,out t_assetbundle) == true){
				return t_assetbundle;
			}

			return null;
		}

		/** アセットバンドル。アンロード。
		*/
		public void UnloadAllAssetBundle()
		{
			Dictionary<long,AssetBundle>.KeyCollection t_collection = this.list.Keys;
			long[] t_keylist = new long[t_collection.Count];
			t_collection.CopyTo(t_keylist,0);

			for(int ii=0;ii<t_keylist.Length;ii++){
				this.UnloadAssetBundle(t_keylist[ii]);
			}
		}

		/** メモリ中のすべてのアセットバンドルをアンロード。
		*/
		public static void UnloadAllAssetBundleFromMemory()
		{
			//アセットバンドル。全破棄。
			AssetBundle.UnloadAllAssetBundles(false);
		}

		/** すべてのキャッシュファイル削除。
		*/
		public static void ClearAllCacheFile()
		{
			AssetBundleList.UnloadAllAssetBundleFromMemory();

			bool t_ret = UnityEngine.Caching.ClearCache();
			Tool.Log("NDownLoad.AssetBundleList","UnityEngine.Caching.ClearCache() : " +  t_ret.ToString());
		}

	}
}

