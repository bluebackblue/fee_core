

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エディター。メインスクリプト作成。
*/


/** Fee.EditorTool
*/
namespace Fee.EditorTool
{
	/** MakeMainScript
	*/
	#if(UNITY_EDITOR)
	public class MakeMainScript
	{
		/** メインスクリプト作成。
		*/
		[UnityEditor.MenuItem("Fee/EditorTool/CreateMainScript")]
		private static void CreateMainScript()
		{
			string t_fullpath = UnityEngine.Application.dataPath + "/" + "Main.cs";

			//存在チェック。
			if(Utility.IsExistFile(t_fullpath) == true){
				return;
			}

			//スクリプトテンプレートを読み込み。
			string t_script_template = null;
			{
				string t_in_fullpath = Utility.FindFile("Editor","Main.temp.cs");
				if(t_in_fullpath != null){
					t_script_template = Utility.ReadTextFile(t_in_fullpath);
				}
				if(t_script_template != null){
					t_script_template = t_script_template.Replace("USE_DEF_FEE_TEMP","true");
				}else{
					return;
				}
			}

			//スクリプトの書き込み。
			Utility.WriteTextFile(t_fullpath,t_script_template);

			//更新。
			UnityEditor.AssetDatabase.Refresh();
		}
	}
	#endif
}

