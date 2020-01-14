

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。ＪＳＯＮシート。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** Convert_DataSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_DataSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<data>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** data_command
			*/
			public string data_command;

			/** data_id
			*/
			public string data_id;

			/** data_path
			*/
			public string data_path;

			/** data_assetbundle_name
			*/
			public string data_assetbundle_name;
		}

		/** データコマンド。
		*/
		public const string DATACOMMAND_RESOURCES_PREFAB = "<resources_prefab>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_RESOURCES_TEXTURE = "<resources_texture>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_RESOURCES_TEXT = "<resources_text>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_STREAMINGASSETS_TEXTURE = "<streamingassets_texture>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_STREAMINGASSETS_TEXT = "<streamingassets_text>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_STREAMINGASSETS_BINARY = "<streamingassets_binary>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_URL_TEXTURE = "<url_texture>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_URL_TEXT = "<url_text>";

		/** データコマンド。
		*/
		public const string DATACOMMAND_URL_BINARY = "<url_binary>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_DEBUG = "<debug>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_RELEASE = "<release>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_DUMMY = "<dummy>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_STANDALONEWINDOWS = "<standalonewindows>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_ANDROID = "<android>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_WEBGL = "<webgl>";

		/** データパラメータ。
		*/
		public const string DATAPARAM_IOS = "<ios>";

		/** コンバート。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＪＳＯＮシート。

		*/
		public static void Convert(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet)
		{
			try{
				if(a_sheet != null){
					if((a_param == Convert_DataSheet.DATAPARAM_DEBUG)||(a_param == Convert_DataSheet.DATAPARAM_RELEASE)){
						Convert_DataSheet.Convert_WriteJson(a_param,a_assets_path,a_sheet);
					}else{
						Convert_DataSheet.Convert_CreateAssetBundle(a_param,a_assets_path,a_sheet);
					}
				}else{
					Tool.Assert(false);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** コンバート。ＪＳＯＮ出力。
		*/
		public static void Convert_WriteJson(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet)
		{
			try{
				if(a_sheet != null){
					System.Collections.Generic.Dictionary<string,Data.JsonListItem> t_list = new System.Collections.Generic.Dictionary<string,Data.JsonListItem>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){

									Data.JsonListItem t_item = null;

									switch(t_sheet[jj].data_command){
									case Convert_DataSheet.DATACOMMAND_RESOURCES_PREFAB:
										{
											//リソース。プレハブ。

											if(a_param == Convert_DataSheet.DATAPARAM_RELEASE){
												if(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == false){
													//アセットバンドル使用。
													t_item = new Data.JsonListItem(Data.PathType.AssetBundle_Prefab,"",t_sheet[jj].data_assetbundle_name);
												}else{
													//リソース使用。
													t_item = new Data.JsonListItem(Data.PathType.Resources_Prefab,t_sheet[jj].data_path,"");
												}
											}else if(a_param == Convert_DataSheet.DATAPARAM_DEBUG){
												//リソース使用。
												t_item = new Data.JsonListItem(Data.PathType.Resources_Prefab,t_sheet[jj].data_path,"");
											}else{
												Tool.Assert(false);
											}
										}break;
									case Convert_DataSheet.DATACOMMAND_RESOURCES_TEXTURE:
										{
											//リソース。テクスチャ。

											if(a_param == Convert_DataSheet.DATAPARAM_RELEASE){
												if(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == false){
													//アセットバンドル使用。
													t_item = new Data.JsonListItem(Data.PathType.AssetBundle_Texture,"",t_sheet[jj].data_assetbundle_name);
												}else{
													//リソース使用。
													t_item = new Data.JsonListItem(Data.PathType.Resources_Texture,t_sheet[jj].data_path,"");
												}
											}else if(a_param == Convert_DataSheet.DATAPARAM_DEBUG){
												//リソース使用。
												t_item = new Data.JsonListItem(Data.PathType.Resources_Texture,t_sheet[jj].data_path,"");
											}else{
												Tool.Assert(false);
											}
										}break;
									case Convert_DataSheet.DATACOMMAND_RESOURCES_TEXT:
										{
											//リソース。テキスト。

											if(a_param == Convert_DataSheet.DATAPARAM_RELEASE){
												if(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == false){
													//アセットバンドル使用。
													t_item = new Data.JsonListItem(Data.PathType.AssetBundle_Text,"",t_sheet[jj].data_assetbundle_name);
												}else{
													//リソース使用。
													t_item = new Data.JsonListItem(Data.PathType.Resources_Text,t_sheet[jj].data_path,"");
												}
											}else if(a_param == Convert_DataSheet.DATAPARAM_DEBUG){
												//リソース使用。
												t_item = new Data.JsonListItem(Data.PathType.Resources_Text,t_sheet[jj].data_path,"");
											}else{
												Tool.Assert(false);
											}
										}break;
									case Convert_DataSheet.DATACOMMAND_STREAMINGASSETS_TEXTURE:
										{
											//ストリーミングアセット。テクスチャ。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.StreamingAssets_Texture,t_sheet[jj].data_path,"");
										}break;
									case Convert_DataSheet.DATACOMMAND_STREAMINGASSETS_TEXT:
										{
											//ストリーミングアセット。テキスト。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.StreamingAssets_Text,t_sheet[jj].data_path,"");
										}break;
									case Convert_DataSheet.DATACOMMAND_STREAMINGASSETS_BINARY:
										{
											//ストリーミングアセット。テキスト。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.StreamingAssets_Binary,t_sheet[jj].data_path,"");
										}break;
									case Convert_DataSheet.DATACOMMAND_URL_TEXTURE:
										{
											//ＵＲＬ。テクスチャ。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.Url_Texture,t_sheet[jj].data_path,"");
										}break;
									case Convert_DataSheet.DATACOMMAND_URL_TEXT:
										{
											//ＵＲＬ。テキスト。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.Url_Text,t_sheet[jj].data_path,"");
										}break;
									case Convert_DataSheet.DATACOMMAND_URL_BINARY:
										{
											//ＵＲＬ。バイナリ。

											Tool.Assert(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == true);
											t_item = new Data.JsonListItem(Data.PathType.Url_Binary,t_sheet[jj].data_path,"");
										}break;
									}

									if(t_item != null){
										t_list.Add(t_sheet[jj].data_id,t_item);
									}else{
										Tool.Assert(false);
									}
								}
							}else{
								Tool.Assert(false);
							}
						}
					}

					//ＪＳＯＮ。出力。
					Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.ObjectToJsonItem(t_list);
					string t_jsonstring = t_jsonitem.ConvertJsonString();
					Fee.EditorTool.Utility.WriteTextFile(Fee.File.Path.CreateAssetsPath(a_assets_path),t_jsonstring,true);
				}else{
					Tool.Assert(false);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** コンバート。アセットバンドル作成。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＪＳＯＮシート。

		*/
		public static void Convert_CreateAssetBundle(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet)
		{
			try{
				if(a_sheet != null){

					//アセットバンドルリスト作成
					/*
					{
						"se.assetbundle" : {
							{
								"SE" :
								"Editor/Test12/se"
							},
							{
								"UI_BUTTON" :
								"Common/Texture/ui_button"
							},
							{
								"IMAGE" :
								"Common/Texture/image.jpg"
							},

							...
						},
						"xx.assetbundle" : {
				
						},
						...
					}
					*/
					System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,ListItem>> t_assetbundlelist = new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,ListItem>>();
					{
						for(int ii=0;ii<a_sheet.Length;ii++){
							if(a_sheet[ii] != null){
								System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
								if(t_sheet != null){
									for(int jj=0;jj<t_sheet.Count;jj++){
										if(string.IsNullOrEmpty(t_sheet[jj].data_assetbundle_name) == false){
											switch(t_sheet[jj].data_command){
											case Convert_DataSheet.DATACOMMAND_RESOURCES_PREFAB:
											case Convert_DataSheet.DATACOMMAND_RESOURCES_TEXTURE:
											case Convert_DataSheet.DATACOMMAND_RESOURCES_TEXT:
												{
													System.Collections.Generic.Dictionary<string,ListItem> t_item_list = null;
													if(t_assetbundlelist.TryGetValue(t_sheet[jj].data_assetbundle_name,out t_item_list) == false){
														t_item_list = new System.Collections.Generic.Dictionary<string,ListItem>();
														t_assetbundlelist.Add(t_sheet[jj].data_assetbundle_name,t_item_list);
													}
													t_item_list.Add(t_sheet[jj].data_id,t_sheet[jj]);
												}break;
											default:
												{
													Tool.Assert(false);
												}break;
											}
										}else{
											//アセットバンドルなし。
										}
									}
								}else{
									Tool.Assert(false);
								}
							}
						}
					}

					switch(a_param){
					case Convert_DataSheet.DATAPARAM_DUMMY:
						{
							//ダミー。

							foreach(System.Collections.Generic.KeyValuePair<string,System.Collections.Generic.Dictionary<string,ListItem>> t_pair in t_assetbundlelist){
								Fee.AssetBundleList.DummryAssetBundle t_dummy_assetbundle = new AssetBundleList.DummryAssetBundle();

								//asset_list
								t_dummy_assetbundle.asset_list = new System.Collections.Generic.Dictionary<string,string>();

								//key_list
								System.Collections.Generic.List<string> t_key_list = new System.Collections.Generic.List<string>(t_pair.Value.Keys);
								for(int ii=0;ii<t_key_list.Count;ii++){

									//asset_name
									string t_asset_name = t_key_list[ii];

									ListItem t_listitem;
									if(t_pair.Value.TryGetValue(t_asset_name,out t_listitem) == true){
										t_dummy_assetbundle.asset_list.Add(t_asset_name,t_listitem.data_path);
									}else{
										Tool.Assert(false);
									}
								}

								Fee.File.Path t_path = new File.Path(a_assets_path.GetPath() + t_pair.Key + ".json");
								string t_jsonstring = Fee.JsonItem.Convert.ObjectToJsonString<Fee.AssetBundleList.DummryAssetBundle>(t_dummy_assetbundle);
								EditorTool.Utility.WriteTextFile(Fee.File.Path.CreateAssetsPath(t_path),t_jsonstring,true);
							}
						}break;
					case Convert_DataSheet.DATAPARAM_STANDALONEWINDOWS:
					case Convert_DataSheet.DATAPARAM_ANDROID:
					case Convert_DataSheet.DATAPARAM_WEBGL:
					case Convert_DataSheet.DATAPARAM_IOS:
						{
							//アセットバンドル。

							//t_assetbundle_build
							UnityEditor.AssetBundleBuild[] t_assetbundle_build = new UnityEditor.AssetBundleBuild[t_assetbundlelist.Count];
							{
								int t_count = 0;
								foreach(System.Collections.Generic.KeyValuePair<string,System.Collections.Generic.Dictionary<string,ListItem>> t_pair in t_assetbundlelist){

									//パック名。
									t_assetbundle_build[t_count].assetBundleName = t_pair.Key;

									//assetBundleVariant
									t_assetbundle_build[t_count].assetBundleVariant = null;

									//key_list
									System.Collections.Generic.List<string> t_key_list = new System.Collections.Generic.List<string>(t_pair.Value.Keys);
									t_assetbundle_build[t_count].assetNames = new string[t_key_list.Count];

									#if(UNITY_5)
									//未対応。
									#else
									t_assetbundle_build[t_count].addressableNames = new string[t_key_list.Count];
									#endif

									for(int ii=0;ii<t_key_list.Count;ii++){
										ListItem t_listitem;
										if(t_pair.Value.TryGetValue(t_key_list[ii],out t_listitem) == true){

											string t_asset_path = null;
											{
												try{
													UnityEngine.Object t_object = UnityEngine.Resources.Load(t_listitem.data_path);
													if(t_object != null){
														t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_object);
													}else{
														Tool.Assert(false);
													}
												}catch(System.Exception t_exception){
													Tool.DebugReThrow(t_exception);
												}
											}

											//assetNames
											t_assetbundle_build[t_count].assetNames[ii] = t_asset_path;

											//addressableNames
											#if(UNITY_5)
											//未対応。
											#else
											t_assetbundle_build[t_count].addressableNames[ii] = t_key_list[ii];
											#endif

										}else{
											Tool.Assert(false);
										}
									}

									t_count++;
								}
							}

							//BuildAssetBundleOptions
							UnityEditor.BuildAssetBundleOptions t_option = UnityEditor.BuildAssetBundleOptions.ForceRebuildAssetBundle;

							//BuildTarget
							UnityEditor.BuildTarget t_buildtarget = UnityEditor.BuildTarget.StandaloneWindows;
							switch(a_param){
							case Convert_DataSheet.DATAPARAM_STANDALONEWINDOWS:
								{
									t_buildtarget = UnityEditor.BuildTarget.StandaloneWindows;
								}break;
							case Convert_DataSheet.DATAPARAM_ANDROID:
								{
									t_buildtarget = UnityEditor.BuildTarget.Android;
								}break;
							case Convert_DataSheet.DATAPARAM_WEBGL:
								{
									t_buildtarget = UnityEditor.BuildTarget.WebGL;
								}break;
							case Convert_DataSheet.DATAPARAM_IOS:
								{
									t_buildtarget = UnityEditor.BuildTarget.iOS;
								}break;
							default:
								{
									Tool.Assert(false);
								}break;
							}

							UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + a_assets_path.GetPath(),t_assetbundle_build,t_option,t_buildtarget);
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
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

