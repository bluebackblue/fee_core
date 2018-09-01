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
	/** InputManager_Item
	*/
	#if UNITY_EDITOR
	public class InputManager_Item
	{
		/** Type
		*/
		public enum Type
		{
			KeyOrMouseButton = 0,
			MouseMovement = 1,
			JoyStickAxis = 2,
		};

		/** Axis
		*/
		public enum Axis
		{
			None = 0,
			XAxis = 0,
			YAxis = 1,
			Axis3 = 2,
			Axis4 = 3,
			Axis5 = 4,
			Axis6 = 5,
			Axis7 = 6,
			Axis8 = 7,
			Axis9 = 8,
			Axis10 = 9,
			Axis11 = 10,
			Axis12 = 11,
			Axis13 = 12,
			Axis14 = 13,
			Axis15 = 14,
			Axis16 = 15,
			Axis17 = 16,
			Axis18 = 17,
			Axis19 = 18,
			Axis20 = 19,
			Axis21 = 20,
			Axis22 = 21,
			Axis23 = 22,
			Axis24 = 23,
			Axis25 = 24,
			Axis26 = 25,
			Axis27 = 26,
			Axis28 = 27,
		}

		/** member
		*/
		public string m_Name;
		public string descriptiveName;
		public string descriptiveNegativeName;
		public string negativeButton;
		public string positiveButton;
		public string altNegativeButton;
		public string altPositiveButton;
		public float gravity;
		public float dead;
		public float sensitivity;
		public bool snap;
		public bool invert;
		public Type type;
		public Axis axis;
		public int joyNum;

		/** constructor
		*/
		public InputManager_Item()
		{
			this.m_Name = "";
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.0f;
			this.sensitivity = 0.0f;
			this.snap = false;
			this.invert = false;
			this.type = Type.KeyOrMouseButton;
			this.axis = Axis.XAxis;
			this.joyNum = 0;
		}

		/** ジョイスティック。軸６。
		*/
		public void CreateJoyAixs6()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_AXIS6;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1000.0f;
			this.dead = 0.001f;
			this.sensitivity = 1000.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.JoyStickAxis;
			this.axis = NInput.InputManager_Item.Axis.Axis6;
			this.joyNum = 0;
		}

		/** ジョイスティック。軸７。
		*/
		public void CreateJoyAixs7()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_AXIS7;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1000.0f;
			this.dead = 0.001f;
			this.sensitivity = 1000.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.JoyStickAxis;
			this.axis = NInput.InputManager_Item.Axis.Axis7;
			this.joyNum = 0;
		}

		/** ジョイスティック。軸８。
		*/
		public void CreateJoyAixs8()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_AXIS8;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1000.0f;
			this.dead = 0.001f;
			this.sensitivity = 1000.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.JoyStickAxis;
			this.axis = NInput.InputManager_Item.Axis.Axis8;
			this.joyNum = 0;
		}

		/** ジョイスティック。ボタン０。
		*/
		public void CreateJoyButton0()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_BUTTON0;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 0";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.KeyOrMouseButton;
			this.axis = NInput.InputManager_Item.Axis.None;
			this.joyNum = 0;
		}

		/** ジョイスティック。ボタン１。
		*/
		public void CreateJoyButton1()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_BUTTON1;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 1";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.KeyOrMouseButton;
			this.axis = NInput.InputManager_Item.Axis.None;
			this.joyNum = 0;
		}

		/** ジョイスティック。ボタン２。
		*/
		public void CreateJoyButton2()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_BUTTON2;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 2";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.KeyOrMouseButton;
			this.axis = NInput.InputManager_Item.Axis.None;
			this.joyNum = 0;
		}

		/** ジョイスティック。ボタン３。
		*/
		public void CreateJoyButton3()
		{
			this.m_Name = NInput.Input.JOY_INPUTNAME_BUTTON3;
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 3";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 1.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = NInput.InputManager_Item.Type.KeyOrMouseButton;
			this.axis = NInput.InputManager_Item.Axis.None;
			this.joyNum = 0;
		}
	}
	#endif
}

