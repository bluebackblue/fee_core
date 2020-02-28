

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。アニメーションシート。
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

	/** Convert_AnimationSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_AnimationSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<animation_prefab>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** animation_command
			*/
			public string animation_command;

			/** animation_tag
			*/
			public string animation_tag;

			/** animation_assetspath
			*/
			public string animation_assetspath;

			/** animation_clipname
			*/
			public string animation_clipname;
		}

		/** コマンド。
		*/
		public const string ANIMATIONCOMMAND_ITEM = "<item>";

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
					System.Collections.Generic.List<string> t_clipname_list = new System_Tuple.Collections.Generic.List<string>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){
									if(Convert_AnimationSheet.ANIMATIONCOMMAND_ITEM == t_sheet[jj].animation_command){
										//<item>

										t_tag_list.Add(t_sheet[jj].animation_tag);
										t_path_list.Add(new File.Path(t_sheet[jj].animation_assetspath));
										t_clipname_list.Add(t_sheet[jj].animation_clipname);
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
						Fee.Instantiate.AnimationClipList_Tool.ResourceItem[] t_animation_list = new Instantiate.AnimationClipList_Tool.ResourceItem[t_tag_list.Count];
						for(int ii=0;ii<t_tag_list.Count;ii++){
							t_animation_list[ii] = new Instantiate.AnimationClipList_Tool.ResourceItem(t_tag_list[ii],t_path_list[ii],t_clipname_list[ii]);
						}

						UnityEngine.GameObject t_prefab = new UnityEngine.GameObject("prefab_temp");
						Fee.Instantiate.AnimationClipList_Tool.Add(t_prefab,t_animation_list);
						Fee.EditorTool.Utility.SavePrefab(t_prefab,a_assets_path,false);
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

