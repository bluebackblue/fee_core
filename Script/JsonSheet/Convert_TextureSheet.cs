

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。ＳＥシート。
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

	/** Convert_TextureSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_TextureSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<texture>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** texture_command
			*/
			public string texture_command;

			/** texture_tag
			*/
			public string texture_tag;

			/** texture_assetspath
			*/
			public string texture_assetspath;
		}

		/** コマンド。
		*/
		public const string TEXTURECOMMAND_ITEM = "<item>";

		/** コンバート。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＪＳＯＮシート。

		*/
		public static void Convert(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet,Fee.JsonSheet.ConvertParam a_convertparam)
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
									if(Convert_TextureSheet.TEXTURECOMMAND_ITEM == t_sheet[jj].texture_command){
										//<item>

										t_tag_list.Add(t_sheet[jj].texture_tag);
										t_path_list.Add(new File.Path(t_sheet[jj].texture_assetspath));
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
						Fee.Instantiate.TextureList_Tool.ResourceItem[] t_texture_list = new Instantiate.TextureList_Tool.ResourceItem[t_tag_list.Count];
						for(int ii=0;ii<t_tag_list.Count;ii++){
							t_texture_list[ii] = new Instantiate.TextureList_Tool.ResourceItem(t_tag_list[ii],t_path_list[ii]);
						}

						UnityEngine.GameObject t_prefab = new UnityEngine.GameObject("prefab_temp");
						Fee.Instantiate.TextureList_Tool.Add(t_prefab,t_texture_list);
						Fee.EditorTool.Utility.SavePrefab(t_prefab,a_assets_path);
						UnityEngine.GameObject.DestroyImmediate(t_prefab);
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

