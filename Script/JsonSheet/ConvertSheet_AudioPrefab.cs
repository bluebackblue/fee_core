

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。オーディオプレハブ。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** ConvertSheet_AudioPrefab
	*/
	#if(UNITY_EDITOR)
	public class ConvertSheet_AudioPrefab
	{
		/** COMMAND
		*/
		public const string COMMAND = "<audioprefab>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** audioprefab_command
			*/
			public string audioprefab_command;

			/** audioprefab_tag
			*/
			public string audioprefab_tag;

			/** audioprefab_assetspath
			*/
			public string audioprefab_assetspath;

			/** audioprefab_volume
			*/
			public float audioprefab_volume;
		}

		/** コマンド。
		*/
		private const string COMMAND_ITEM = "<item>";

		/** コンバート。
		*/
		public static void Convert(string a_convert_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet,Fee.JsonSheet.ConvertParam a_convertparam)
		{
			try{
				if(a_sheet != null){
					System.Collections.Generic.List<string> t_tag_list = new System.Collections.Generic.List<string>();
					System.Collections.Generic.List<Fee.File.Path> t_path_list = new System.Collections.Generic.List<Fee.File.Path>();
					System.Collections.Generic.List<float> t_volume_list = new System.Collections.Generic.List<float>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){
									if(ConvertSheet_AudioPrefab.COMMAND_ITEM == t_sheet[jj].audioprefab_command){
										//<item>

										t_tag_list.Add(t_sheet[jj].audioprefab_tag);
										t_path_list.Add(new File.Path(t_sheet[jj].audioprefab_assetspath));
										t_volume_list.Add(t_sheet[jj].audioprefab_volume);
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
						Fee.Instantiate.AudioClipList_Tool.ResourceItem[] t_audioclip_list = new Instantiate.AudioClipList_Tool.ResourceItem[t_tag_list.Count];
						Fee.Instantiate.AudioVolumeList_Tool.ResourceItem[] t_audiovolume_list = new Instantiate.AudioVolumeList_Tool.ResourceItem[t_tag_list.Count];
						for(int ii=0;ii<t_tag_list.Count;ii++){
							t_audioclip_list[ii] = new Instantiate.AudioClipList_Tool.ResourceItem(t_tag_list[ii],t_path_list[ii]);
							t_audiovolume_list[ii] = new Instantiate.AudioVolumeList_Tool.ResourceItem(t_tag_list[ii],t_volume_list[ii]);
						}

						UnityEngine.GameObject t_prefab = new UnityEngine.GameObject("prefab_temp");
						Fee.Instantiate.AudioClipList_Tool.Add(t_prefab,t_audioclip_list);
						Fee.Instantiate.AudioVolumeList_Tool.Add(t_prefab,t_audiovolume_list);
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

