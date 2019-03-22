

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。インプットマネージャ編集。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** InputManage
	*/
	#if(UNITY_EDITOR)
	public class EditInputManager
	{
		/** list
		*/
		private System.Collections.Generic.List<EditInputManager_Item> list;

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
		public EditInputManager()
		{
			this.list = new System.Collections.Generic.List<EditInputManager_Item>();

			this.asset = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("ProjectSettings/InputManager.asset");
			this.serialized_root = new UnityEditor.SerializedObject(this.asset);
			this.serialized_list = this.serialized_root.FindProperty("m_Axes");

			for(int ii=0;ii<this.serialized_list.arraySize;ii++){
				EditInputManager_Item t_item = new EditInputManager_Item();
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
						t_item.type = (EditInputManager_Item.Type)t_serialized_it.intValue;
					}
					if(t_serialized_it.name == "axis"){
						t_item.axis = (EditInputManager_Item.Axis)t_serialized_it.intValue;
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
				
				EditInputManager_Item t_item = this.list[ii];

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
		public System.Collections.Generic.List<EditInputManager_Item> GetList()
		{
			return this.list;
		}

		/** 追加。
		*/
		public void Add(EditInputManager_Item a_item)
		{
			this.list.Add(a_item);
		}

		/** インプットマネージャ初期化。
		*/
		#if((USE_DEF_FEE_EDITORMENU)&&(UNITY_EDITOR))
		[UnityEditor.MenuItem("Fee/Initialize/InputManager")]
		private static void MakeInputManager()
		{
			Fee.Input.EditInputManager t_inputmaanger = new Fee.Input.EditInputManager();
			{
				System.Collections.Generic.List<Fee.Input.EditInputManager_Item> t_list = t_inputmaanger.GetList();

				System.Collections.Generic.Dictionary<string,Fee.Input.EditInputManager_Item> t_flag_list = new System.Collections.Generic.Dictionary<string,Fee.Input.EditInputManager_Item>();
				{
					//トリガー。
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateLeftTrigger1Button();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT1,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateRightTrigger1Button();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT1,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateLeftTrigger2Button();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LT2,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateRightTrigger2Button();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RT2,t_item);
					}

					//ボタン。
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonLeft();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LEFT,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonRight();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RIGHT,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonUp();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_UP,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonDown();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_DOWN,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonEnter();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ENTER,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonEscape();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_ESCAPE,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonSub1();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB1,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonSub2();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_SUB2,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonLeftMenu();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LMENU,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateDigitalButtonRightMenu();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RMENU,t_item);
					}

					//スティック。
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateLeftStickAxisX();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSX,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateLeftStickAxisY();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSY,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateRightStickAxisX();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSX,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateRightStickAxisY();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSY,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateLeftStickButton();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_LSB,t_item);
					}
					{
						Fee.Input.EditInputManager_Item t_item = new Fee.Input.EditInputManager_Item();
						t_item.CreateRightStickButton();
						t_flag_list.Add(Fee.Input.Config.INPUTMANAGER_RSB,t_item);
					}
				}

				//すでにリストに存在するものはリストから外す。
				for(int ii=0;ii<t_list.Count;ii++){
					Fee.Input.EditInputManager_Item t_item;
					if(t_flag_list.TryGetValue(t_list[ii].m_Name,out t_item) == true){
						//すでに存在する。
						t_flag_list[t_list[ii].m_Name] = null;
					}
				}

				//リストに追加。
				foreach(System.Collections.Generic.KeyValuePair<string,Fee.Input.EditInputManager_Item> t_pair in t_flag_list){
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
	#endif
}

