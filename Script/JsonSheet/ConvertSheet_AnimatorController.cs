

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
	/** ConvertSheet_AnimatorController
	*/
	#if(UNITY_EDITOR)
	public class ConvertSheet_AnimatorController
	{
		/** COMMAND
		*/
		public const string COMMAND = "<animatorcontroller>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** animatorcontroller_command
			*/
			public string animatorcontroller_command;

			/** animatorcontroller_state
			*/
			public string animatorcontroller_state;

			/** animatorcontroller_assetspath
			*/
			public string animatorcontroller_assetspath;
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
					System.Collections.Generic.List<string> t_state_list = new System.Collections.Generic.List<string>();
					System.Collections.Generic.List<Fee.File.Path> t_path_list = new System.Collections.Generic.List<Fee.File.Path>();

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){
									if(ConvertSheet_AnimatorController.COMMAND_ITEM == t_sheet[jj].animatorcontroller_command){
										//<item>

										t_state_list.Add(t_sheet[jj].animatorcontroller_state);
										t_path_list.Add(new File.Path(t_sheet[jj].animatorcontroller_assetspath));
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
						UnityEditor.Animations.AnimatorController t_controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/" + a_assets_path.GetPath());

						for(int ii=0;ii<t_state_list.Count;ii++){
							UnityEditor.Animations.AnimatorState t_state = t_controller.layers[0].stateMachine.AddState(t_state_list[ii]);
							t_state.motion = Fee.EditorTool.Utility.LoadAsset<UnityEngine.AnimationClip>(t_path_list[ii]);
						}

						UnityEditor.AssetDatabase.SaveAssets();
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

