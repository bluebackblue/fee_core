

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。インプットマネージャ編集。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
{
	/** InputManagerTool
	*/
	public class InputManagerTool
	{
		/** list
		*/
		private System.Collections.Generic.List<InputManagerTool_Item> list;

		/** asset
		*/
		private UnityEngine.Object asset;

		/** serialized_root
		*/
		private UnityEditor.SerializedObject serialized_root;

		/** serialized_list
		*/
		private UnityEditor.SerializedProperty serialized_list;

		/** constructor
		*/
		public InputManagerTool()
		{
			this.list = new System.Collections.Generic.List<InputManagerTool_Item>();

			#if(UNITY_5)
			this.asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
			#else
			this.asset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/InputManager.asset");
			#endif

			this.serialized_root = new UnityEditor.SerializedObject(this.asset);
			this.serialized_list = this.serialized_root.FindProperty("m_Axes");

			for(int ii=0;ii<this.serialized_list.arraySize;ii++){
				InputManagerTool_Item t_item = new InputManagerTool_Item();
				UnityEditor.SerializedProperty t_serialized_it = this.serialized_list.GetArrayElementAtIndex(ii);
				t_serialized_it.Next(true);
				do{
					if(t_serialized_it.name == "m_Name"){
						t_item.m_Name = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "descriptiveName"){
						t_item.descriptiveName = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "descriptiveNegativeName"){
						t_item.descriptiveNegativeName = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "negativeButton"){
						t_item.negativeButton = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "positiveButton"){
						t_item.positiveButton = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "altNegativeButton"){
						t_item.altNegativeButton = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "altPositiveButton"){
						t_item.altPositiveButton = t_serialized_it.stringValue;
					}
					if(t_serialized_it.name == "gravity"){
						t_item.gravity = t_serialized_it.floatValue;
					}
					if(t_serialized_it.name == "dead"){
						t_item.dead = t_serialized_it.floatValue;
					}
					if(t_serialized_it.name == "sensitivity"){
						t_item.sensitivity = t_serialized_it.floatValue;
					}
					if(t_serialized_it.name == "snap"){
						t_item.snap = t_serialized_it.boolValue;
					}
					if(t_serialized_it.name == "invert"){
						t_item.invert = t_serialized_it.boolValue;
					}
					if(t_serialized_it.name == "type"){
						t_item.type = (InputManagerTool_Item.Type)t_serialized_it.intValue;
					}
					if(t_serialized_it.name == "axis"){
						t_item.axis = (InputManagerTool_Item.Axis)t_serialized_it.intValue;
					}
					if(t_serialized_it.name == "joyNum"){
						t_item.joyNum = t_serialized_it.intValue;
					}
				}while(t_serialized_it.Next(false));

				this.list.Add(t_item);
			}
		}

		/** セーブ。
		*/
		public void Save()
		{
			//リストを空にする。
			this.serialized_list.ClearArray();

			for(int ii=0;ii<this.list.Count;ii++){
				this.serialized_list.arraySize++;
				
				InputManagerTool_Item t_item = this.list[ii];

				UnityEditor.SerializedProperty t_serialized_it = this.serialized_list.GetArrayElementAtIndex(this.serialized_list.arraySize - 1);
				t_serialized_it.Next(true);
				do{
					if(t_serialized_it.name == "m_Name"){
						t_serialized_it.stringValue = t_item.m_Name;
					}
					if(t_serialized_it.name == "descriptiveName"){
						t_serialized_it.stringValue = t_item.descriptiveName;
					}
					if(t_serialized_it.name == "descriptiveNegativeName"){
						t_serialized_it.stringValue = t_item.descriptiveNegativeName;
					}
					if(t_serialized_it.name == "negativeButton"){
						t_serialized_it.stringValue = t_item.negativeButton;
					}
					if(t_serialized_it.name == "positiveButton"){
						t_serialized_it.stringValue = t_item.positiveButton;
					}
					if(t_serialized_it.name == "altNegativeButton"){
						t_serialized_it.stringValue = t_item.altNegativeButton;
					}
					if(t_serialized_it.name == "altPositiveButton"){
						t_serialized_it.stringValue = t_item.altPositiveButton;
					}
					if(t_serialized_it.name == "gravity"){
						t_serialized_it.floatValue = t_item.gravity;
					}
					if(t_serialized_it.name == "dead"){
						t_serialized_it.floatValue = t_item.dead;
					}
					if(t_serialized_it.name == "sensitivity"){
						t_serialized_it.floatValue = t_item.sensitivity;
					}
					if(t_serialized_it.name == "snap"){
						t_serialized_it.boolValue = t_item.snap;
					}
					if(t_serialized_it.name == "invert"){
						t_serialized_it.boolValue = t_item.invert;
					}
					if(t_serialized_it.name == "type"){
						t_serialized_it.intValue = (int)t_item.type;
					}
					if(t_serialized_it.name == "axis"){
						t_serialized_it.intValue = (int)t_item.axis;
					}
					if(t_serialized_it.name == "joyNum"){
						t_serialized_it.intValue = t_item.joyNum;
					}
				}while(t_serialized_it.Next(false));
			}

			this.serialized_root.ApplyModifiedProperties();
		}

		/** リスト。取得。
		*/
		public System.Collections.Generic.List<InputManagerTool_Item> GetList()
		{
			return this.list;
		}

		/** 追加。
		*/
		public void Add(InputManagerTool_Item a_item)
		{
			this.list.Add(a_item);
		}

		/** インプットマネージャ初期化。
		*/
		#if(!NOUSE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Initialize/InitializeInputManager")]
		private static void MenuItem_InitializeInputManager()
		{
			InputManagerTool t_inputmaanger = new InputManagerTool();
			{
				System.Collections.Generic.List<InputManagerTool_Item> t_list = t_inputmaanger.GetList();

				System.Collections.Generic.Dictionary<string,InputManagerTool_Item> t_flag_list = new System.Collections.Generic.Dictionary<string,InputManagerTool_Item>();
				{
					for(Fee.Input.Pad.PadType ii=0;ii<Fee.Input.Pad.PadType.Max;ii++){

						//トリガー。
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateLeftTrigger1Button(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT1[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateRightTrigger1Button(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT1[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateLeftTrigger2Button(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT2[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateRightTrigger2Button(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT2[(int)ii],t_item);
						}

						//ボタン。
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonLeft(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LEFT[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonRight(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RIGHT[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonUp(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_UP[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonDown(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_DOWN[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonEnter(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ENTER[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonEscape(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ESCAPE[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonSub1(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB1[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonSub2(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB2[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonLeftMenu(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LMENU[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateDigitalButtonRightMenu(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RMENU[(int)ii],t_item);
						}

						//スティック。
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateLeftStickAxisX(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSX[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateLeftStickAxisY(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSY[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateRightStickAxisX(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSX[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateRightStickAxisY(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSY[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateLeftStickButton(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSB[(int)ii],t_item);
						}
						{
							InputManagerTool_Item t_item = new InputManagerTool_Item();
							t_item.CreateRightStickButton(ii);
							t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSB[(int)ii],t_item);
						}
					}
				}

				//すでにリストに存在しているものはリストから外す。
				for(int ii=0;ii<t_list.Count;ii++){
					InputManagerTool_Item t_item;
					if(t_flag_list.TryGetValue(t_list[ii].m_Name,out t_item) == true){
						//すでに存在している。
						t_flag_list[t_list[ii].m_Name] = null;
					}
				}

				//リストに追加。
				foreach(System.Collections.Generic.KeyValuePair<string,InputManagerTool_Item> t_pair in t_flag_list){
					if(t_pair.Value != null){
						t_list.Add(t_pair.Value);
					}
				}
			}

			//セーブ。
			t_inputmaanger.Save();
		}
		#endif
	}
}
#endif

