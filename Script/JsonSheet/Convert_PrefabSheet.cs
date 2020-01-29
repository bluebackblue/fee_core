

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。プレハブシート。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** System_Tuple
	*/
	#if(UNITY_5)
	using System_Tuple = Fee.Unity5;
	#else
	using System_Tuple = System;
	#endif

	/** Convert_PrefabSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_PrefabSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<prefab>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** prefab_command
			*/
			public string prefab_command;

			/** prefab_tag
			*/
			public string prefab_tag;

			/** prefab_assetspath
			*/
			public string prefab_assetspath;
		}

		/** コマンド。
		*/
		public const string PREFABCOMMAND_ITEM = "<item>";

		/** コンバート。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＪＳＯＮシート。

		*/
		public static void Convert(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet)
		{
			try{
				if(a_sheet != null){
					System.Collections.Generic.List<string> t_tag_list = new System_Tuple.Collections.Generic.List<string>();
					System.Collections.Generic.List<Fee.File.Path> t_path_list = new System_Tuple.Collections.Generic.List<Fee.File.Path>();
					System.Collections.Generic.List<float> t_volume_list = new System_Tuple.Collections.Generic.List<float>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){
									if(Convert_PrefabSheet.PREFABCOMMAND_ITEM == t_sheet[jj].prefab_command){
										//<item>

										t_tag_list.Add(t_sheet[jj].prefab_tag);
										t_path_list.Add(new File.Path(t_sheet[jj].prefab_assetspath));
									}else{
										//無関係。複合シート。
									}
								}
							}else{
								Tool.Assert(false);
							}
						}
					}

					//保存。
					{
						Fee.Instantiate.PrefabList_Tool.ResourceItem[] t_prefab_list = new Instantiate.PrefabList_Tool.ResourceItem[t_tag_list.Count];
						for(int ii=0;ii<t_tag_list.Count;ii++){
							t_prefab_list[ii] = new Instantiate.PrefabList_Tool.ResourceItem(t_tag_list[ii],t_path_list[ii]);
						}

						Fee.Instantiate.PrefabList_Tool.Create(a_assets_path,t_prefab_list);
					}
				}else{
					Tool.Assert(false);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
	#endif
}

