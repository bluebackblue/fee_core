

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
	/** Convert_AudioSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_AudioSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<audio>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** audio_command
			*/
			public string audio_command;

			/** audio_assetspath
			*/
			public string audio_assetspath;

			/** audio_volume
			*/
			public float audio_volume;
		}

		/** オーディオコマンド。
		*/
		public const string AUDIOCOMMAND_ITEM = "<item>";

		/** コンバート。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＪＳＯＮシート。

		*/
		public static void Convert(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet)
		{
			try{
				if(a_sheet != null){
					System.Collections.Generic.List<System.Tuple<UnityEngine.AudioClip,float>> t_list = new System.Collections.Generic.List<System.Tuple<UnityEngine.AudioClip,float>>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){
									if(Convert_AudioSheet.AUDIOCOMMAND_ITEM == t_sheet[jj].audio_command){
										//<item>

										UnityEngine.AudioClip t_audio_clip = null;
										try{
											t_audio_clip = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.AudioClip>("Assets/" + t_sheet[jj].audio_assetspath);
											if(t_audio_clip == null){
												Tool.Log("Convert_AudioSheet","Not Found : " + t_sheet[jj].audio_assetspath);
												Tool.Assert(false);
											}
										}catch(System.Exception t_exception){
											Tool.DebugReThrow(t_exception);
										}

										t_list.Add(new System.Tuple<UnityEngine.AudioClip,float>(t_audio_clip,t_sheet[jj].audio_volume));
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
						UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();

						Fee.Audio.Pack_AudioClip t_pack = t_prefab.AddComponent<Fee.Audio.Pack_AudioClip>();
						for(int ii=0;ii<t_list.Count;ii++){
							t_pack.audioclip_list.Add(t_list[ii].Item1);
							t_pack.volume_list.Add(t_list[ii].Item2);
						}

						try{
							UnityEditor.PrefabUtility.SaveAsPrefabAsset(t_prefab,"Assets/" + a_assets_path.GetPath(),out bool t_ret);
							Tool.Assert(t_ret);
						}catch(System.Exception t_exception){
							Tool.DebugReThrow(t_exception);
						}

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

