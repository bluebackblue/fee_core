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

					UnityEditor.SerializedProperty t_serialized_it = this.serialized_axes.GetArrayElementAtIndex(ii);
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
							t_item.type = t_serialized_it.intValue;
						}
						if(t_serialized_it.name == "axis"){
							t_item.axis = t_serialized_it.intValue;
						}
						if(t_serialized_it.name == "joyNum"){
							t_item.joyNum = t_serialized_it.intValue;
						}

						this.list.Add(t_item);
					}while(t_serialized_it.Next(false));
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

		/** セーブ。
		*/
		public void Save()
		{
			this.serialized_axes.ClearArray();

			for(int ii=0;ii<this.list.Count;ii++){
				this.serialized_axes.arraySize++;
				this.serialized_root.ApplyModifiedProperties();

				InputManager_Item t_item = this.list[ii];

				UnityEditor.SerializedProperty t_serialized_it = this.serialized_axes.GetArrayElementAtIndex(this.serialized_axes.arraySize - 1);
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
						t_serialized_it.intValue = t_item.type;
					}
					if(t_serialized_it.name == "axis"){
						t_serialized_it.intValue = t_item.axis;
					}
					if(t_serialized_it.name == "joyNum"){
						t_serialized_it.intValue = t_item.joyNum;
					}
				}while(t_serialized_it.Next(false));
			}
		}

		[UnityEditor.MenuItem("InputManager/Test")]
		private static void Test()
		{
			InputManage t_inputmaanger = new InputManage();
			t_inputmaanger.Load();
			t_inputmaanger.Save();
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


