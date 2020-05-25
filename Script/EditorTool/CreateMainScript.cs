

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
	/** CreateMainScript
	*/
	#if(UNITY_EDITOR)
	public class CreateMainScript
	{
		/** メインスクリプト作成。
		*/
		#if(!NOUSE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Initialize/CreateMainScript")]
		private static void MenuItem_CreateMainScript()
		{
			Fee.File.Path t_path = new Fee.File.Path("Main.cs");

			//存在チェック。
			if(Utility.IsExistFile(t_path) == true){
				return;
			}

			//スクリプトテンプレートを読み込み。
			string t_template_string = null;
			{
				Fee.File.Path t_fee_path  = Fee.EditorTool.Utility.FindFeePath();
				if(t_fee_path != null){

					Fee.File.Path t_template_path = Utility.FindFile(t_fee_path,new File.Path("Main.temp.cs"));
					if(t_template_path != null){
						t_template_string = Utility.ReadTextFile(t_template_path);
					}else{
						UnityEngine.Debug.LogError("Not Found : Main.temp.cs");
					}

					if(t_template_string != null){
						t_template_string = t_template_string.Replace("USE_DEF_FEE_TEMP","true");
					}else{
						UnityEngine.Debug.LogError("Read Error : Main.temp.cs");
					}
				}else{
					UnityEngine.Debug.LogError("Not Found : fee_buildtarget");
				}
			}

			//スクリプトの書き込み。
			Utility.WriteTextFile(t_path,t_template_string);

			//更新。
			UnityEditor.AssetDatabase.Refresh();
		}
		#endif
	}
	#endif
}

