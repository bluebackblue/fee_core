using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。インプットマネージャ。
*/


/** NInput
*/
namespace NInput
{
	/** InputManage
	*/
	public class InputManage
	{
		/** list
		*/
		private List<InputManager_Item> list;

		/** asset
		*/
		UnityEngine.Object asset;

		/** serialized_root
		*/
		UnityEditor.SerializedObject serialized_root;

		/** serialized_axes
		*/
		UnityEditor.SerializedProperty serialized_axes;

		/** constructor
		*/
		public InputManage()
		{
			this.list = new List<InputManager_Item>();
		}

		/** ロード。
		*/
		public void Load()
		{
			this.list.Clear();

			UnityEngine.Object[] t_asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset");
			if(t_asset != null){
				this.asset = t_asset[0];

				this.serialized_root = new UnityEditor.SerializedObject(t_asset[0]);
				this.serialized_axes = this.serialized_root.FindProperty("m_Axes");
				for(int ii=0;ii<this.serialized_axes.arraySize;ii++){
					InputManager_Item t_item = new InputManager_Item();

					UnityEditor.SerializedProperty t_serialized_listitem = this.serialized_axes.GetArrayElementAtIndex(ii);
					UnityEditor.SerializedProperty t_serialized_temp = t_serialized_listitem.Copy();
					t_serialized_temp.Next(true);
					do{
						Debug.Log(ii.ToString() + " : " + t_serialized_temp.name + " : " + t_serialized_temp.propertyType.ToString());

						if(t_serialized_temp.name == "m_Name"){
							t_item.m_Name = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "descriptiveName"){
							t_item.descriptiveName = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "descriptiveNegativeName"){
							t_item.descriptiveNegativeName = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "negativeButton"){
							t_item.negativeButton = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "positiveButton"){
							t_item.positiveButton = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "altNegativeButton"){
							t_item.altNegativeButton = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "altPositiveButton"){
							t_item.altPositiveButton = t_serialized_temp.stringValue;
						}
						if(t_serialized_temp.name == "gravity"){
							t_item.gravity = t_serialized_temp.floatValue;
						}
						if(t_serialized_temp.name == "dead"){
							t_item.dead = t_serialized_temp.floatValue;
						}
						if(t_serialized_temp.name == "sensitivity"){
							t_item.sensitivity = t_serialized_temp.floatValue;
						}
						if(t_serialized_temp.name == "snap"){
							t_item.snap = t_serialized_temp.boolValue;
						}
						if(t_serialized_temp.name == "invert"){
							t_item.invert = t_serialized_temp.boolValue;
						}
						if(t_serialized_temp.name == "type"){
							t_item.type = t_serialized_temp.intValue;
						}
						if(t_serialized_temp.name == "axis"){
							t_item.axis = t_serialized_temp.intValue;
						}
						if(t_serialized_temp.name == "joyNum"){
							t_item.joyNum = t_serialized_temp.intValue;
						}

						Debug.Log(t_item.m_Name);

						this.list.Add(t_item);
					}while(t_serialized_temp.Next(false));
				}
			}
		}

		/** 検索。
		*/
		public List<InputManager_Item> GetList()
		{
			return this.list;
		}

		/** 追加。
		*/
		public void Add(InputManager_Item a_item)
		{
			this.list.Add(a_item);
		}

		/**
		*/
		public void ReCreate()
		{
			
		}

		/** クリア。
		*/
		public void Clear()
		{
			this.serialized_axes.ClearArray();
			this.serialized_root.ApplyModifiedProperties();
		}

		[UnityEditor.MenuItem("InputManager/Save")]
		private static void Save()
		{
			InputManage t_inputmaanger = new InputManage();

			t_inputmaanger.Load();

			t_inputmaanger.Clear();

			/*
			UnityEditor.EditorUtility.SetDirty(this.asset);
			UnityEditor.AssetDatabase.SaveAssets();
			UnityEditor.AssetDatabase.Refresh();
			*/
		}

		/*
	 - serializedVersion: 3
		m_Name: Axis7
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: 
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1000
		dead: 0.001
		sensitivity: 1000
		snap: 0
		invert: 0
		type: 2
		axis: 6
		joyNum: 0
	  - serializedVersion: 3
		m_Name: Axis8
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: 
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1000
		dead: 0.001
		sensitivity: 1000
		snap: 0
		invert: 0
		type: 2
		axis: 7
		joyNum: 0
	  - serializedVersion: 3
		m_Name: Axis6
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: 
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1000
		dead: 0.001
		sensitivity: 1000
		snap: 0
		invert: 0
		type: 2
		axis: 5
		joyNum: 0
	  - serializedVersion: 3
		m_Name: Button2
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: joystick button 2
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1
		dead: 0.001
		sensitivity: 1
		snap: 0
		invert: 0
		type: 0
		axis: 0
		joyNum: 0
	  - serializedVersion: 3
		m_Name: Button0
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: joystick button 0
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1
		dead: 0.001
		sensitivity: 1
		snap: 0
		invert: 0
		type: 0
		axis: 0
		joyNum: 0
	  - serializedVersion: 3
		m_Name: Button1
		descriptiveName: 
		descriptiveNegativeName: 
		negativeButton: 
		positiveButton: joystick button 1
		altNegativeButton: 
		altPositiveButton: 
		gravity: 1
		dead: 0.001
		sensitivity: 1
		snap: 0
		invert: 0
		type: 0
		axis: 0
		joyNum: 0
		*/

	}
}


