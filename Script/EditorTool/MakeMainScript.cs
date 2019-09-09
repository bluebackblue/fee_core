

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。メインスクリプト作成。
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
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Initialize/CreateMainScript")]
		private static void MenuItem_CreateMainScript()
		{
			Fee.File.Path t_path = Fee.File.Path.CreateAssetsPath("Main.cs");

			//存在チェック。
			if(Utility.IsExistFile(t_path) == true){
				return;
			}

			//スクリプトテンプレートを読み込み。
			string t_template_string = null;
			{
				Fee.File.Path t_fullpath_temp = Utility.FindFile("EditorTool","Main.temp.cs");
				if(t_fullpath_temp != null){
					t_template_string = Utility.ReadTextFile(t_fullpath_temp);
				}
				if(t_template_string != null){
					t_template_string = t_template_string.Replace("USE_DEF_FEE_TEMP","true");
				}else{
					return;
				}
			}

			//スクリプトの書き込み。
			Utility.WriteTextFile(t_path,t_template_string,true);

			//更新。
			UnityEditor.AssetDatabase.Refresh();
		}
		#endif
	}
	#endif
}

