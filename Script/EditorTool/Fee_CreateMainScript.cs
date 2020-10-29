

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
	/** Fee_CreateMainScript
	*/
	#if(UNITY_EDITOR)
	public class Fee_CreateMainScript
	{
		/** メインスクリプト作成。
		*/
		#if(!NOUSE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Initialize/CreateMainScript")]
		private static void MenuItem_CreateMainScript()
		{
			Fee.File.Path t_path = new Fee.File.Path("Main.cs");

			//存在チェック。
			if(AssetTool.IsExistFile(t_path) == true){
				return;
			}

			//スクリプトテンプレートを読み込み。
			string t_template_string = null;
			{
				Fee.File.Path t_fee_path  = Fee_Tool.FindFeePath();
				if(t_fee_path != null){

					Fee.File.Path t_template_path = AssetTool.FindFile(t_fee_path,new File.Path("Fee_Main.temp.cs"));
					if(t_template_path != null){
						t_template_string = AssetTool.ReadTextFile(t_template_path);
					}else{
						Tool.EditorLogError("Not Found : Fee_Main.temp.cs");
					}

					if(t_template_string != null){
						t_template_string = t_template_string.Replace("USE_DEF_FEE_TEMP","true");
					}else{
						Tool.EditorLogError("Read Error : Fee_Main.temp.cs");
					}
				}else{
					Tool.EditorLogError("Not Found : fee_buildtarget");
				}
			}

			//スクリプトの書き込み。
			AssetTool.WriteTextFile(t_path,t_template_string);

			//更新。
			AssetTool.Refresh();
		}
		#endif
	}
	#endif
}

