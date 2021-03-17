

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。デファインリストアップ。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
{
	/** Fee_DefineListUp
	*/
	public class Fee_DefineListUp
	{
		/** s_list
		*/
		private static System.Collections.Generic.HashSet<string> s_list = null;

		/** constructor
		*/
		public Fee_DefineListUp()
		{
		}

		/** インプットマネージャ初期化。
		*/
		#if(!NOUSE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Code/DefineListup")]
		private static void MenuItem_InitializeInputManager()
		{
			s_list = new System.Collections.Generic.HashSet<string>();
			
			System.Collections.Generic.List<Fee.File.Path> t_list = Fee.EditorTool.AssetTool.CreateAllFilePathList(new File.Path(""));
			for(int ii=0;ii<t_list.Count;ii++){
				string t_path = t_list[ii].GetPath();
				if(System.Text.RegularExpressions.Regex.IsMatch(t_path,"^.*\\.cs$") == true){
					FileMain(t_list[ii]);
				}
			}

			foreach(string t_item in s_list){
				#if(UNITY_EDITOR)
				Tool.EditorLog("-define:" + t_item);
				#endif
			}

			s_list = null;
		}
		#endif

		/** FileMain
		*/
		private static void FileMain(Fee.File.Path a_assets_path)
		{
			string t_full_path = Fee.File.Path.CreateAssetsPath(a_assets_path,Fee.File.Path.SEPARATOR).GetPath();
			string t_text = Fee.EditorTool.AssetTool.ReadTextFile(a_assets_path);
			if(t_text != null){
				string[] t_list_text = t_text.Split(new char[]{'\n'});
				for(int ii=0;ii<t_list_text.Length;ii++){
					string t_temp = t_list_text[ii];

					//最後についている「\r」を削除。
					{
						if(t_temp.Length > 0){
							t_temp = System.Text.RegularExpressions.Regex.Replace(t_temp,"^(.*)\r$","$1");
						}
					}

					//「//」タイプのコメントを削除。
					{
						if(t_temp.Length > 0){
							System.Text.RegularExpressions.Regex t_regex = new System.Text.RegularExpressions.Regex("^(?<capture>.*)\\/\\/.*$");
							System.Text.RegularExpressions.Match t_match = t_regex.Match(t_temp);
							if(t_match.Success == true){
								string[] t_group_list = new string[]{"0","capture"};
								for(int jj=0;jj<t_group_list.Length;jj++){
									string t_group_name = t_group_list[jj];
									System.Text.RegularExpressions.Group t_group = t_match.Groups[t_group_name];
									if(t_group.Success == true){
										switch(t_group_name){
										case "0":
											{
											}break;
										case "capture":
											{
												if(t_temp != t_group.Value){
													t_temp = t_group.Value;
												}
											}break;
										default:
											{
												#if(UNITY_EDITOR)
												Tool.EditorLogError(a_assets_path.GetPath() + ":" + ii.ToString() + ":" + t_group_name + ":" + t_group.Value + ":" + t_list_text[ii]);
												#endif
											}break;
										}
									}
								}
							}
						}
					}

					//「#if」「#elif」使用箇所検索。
					{
						System.Text.RegularExpressions.Regex t_regex = new System.Text.RegularExpressions.Regex("^\\s*\\#\\s*(?<deftype>if|elif)\\s*(?<capture>.*)\\s*$");
						System.Text.RegularExpressions.Match t_match = t_regex.Match(t_temp);
						if(t_match.Success == true){
							string[] t_group_list = new string[]{"0","deftype","capture"};
							for(int jj=0;jj<t_group_list.Length;jj++){
								string t_group_name = t_group_list[jj];
								System.Text.RegularExpressions.Group t_group = t_match.Groups[t_group_name];
								if(t_group.Success == true){
									switch(t_group_name){
									case "0":
										{
										}break;
									case "deftype":
										{
										}break;
									case "capture":
										{
											string[] t_list_define = t_group.Value.Split(new char[]{'(',')','|','&','!',' '});
											for(int kk=0;kk<t_list_define.Length;kk++){
												if(t_list_define[kk].Length > 0){
													DefineMain(t_temp,t_list_define[kk]);
												}
											}
										}break;
									default:
										{
											#if(UNITY_EDITOR)
											Tool.EditorLogError(a_assets_path.GetPath() + ":" + ii.ToString() + ":" + t_group_name + ":" + t_group.Value + ":" + t_list_text[ii]);
											#endif
										}break;
									}
								}
							}
						}
					}
				}
			}
		}

		/** DefineMain
		*/
		private static void DefineMain(string a_line,string a_define)
		{
			//デファイン名発見。
			if(System.Text.RegularExpressions.Regex.IsMatch(a_define,"^[a-zA-Z0-9_]*$") == true){
				switch(a_define){
				case "true":
				case "false":
					{
						/*
						#if(UNITY_EDITOR)
						Tool.EditorLogError(a_line);
						#endif
						*/
					}break;
				case "":
					{
						/*
						#if(UNITY_EDITOR)
						Tool.EditorLogError(a_line);
						#endif
						*/
					}break;
				default:
					{
						s_list.Add(a_define);
					}break;
				}
				return;
			}

			//不明。
			#if(UNITY_EDITOR)
			Tool.EditorLogError(a_line);
			#endif
		}
	}
}
#endif

