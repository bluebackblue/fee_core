

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
	/** InputManagerTool_Item
	*/
	public class InputManagerTool_Item
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
		};

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
		public InputManagerTool_Item()
		{
			this.m_Name = "";
			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Type.KeyOrMouseButton;
			this.axis = Axis.XAxis;
			this.joyNum = 0;
		}

		/** デジタルボタン。左。
		*/
		public void CreateDigitalButtonLeft(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LEFT.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis7;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis6;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis5;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。右。
		*/
		public void CreateDigitalButtonRight(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RIGHT.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis7;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis6;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis5;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。上。
		*/
		public void CreateDigitalButtonUp(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_UP.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis8;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis7;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = true;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis6;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。下。
		*/
		public void CreateDigitalButtonDown(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_DOWN.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis8;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis7;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = true;
					this.invert = true;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis6;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。Enter。
		*/
		public void CreateDigitalButtonEnter(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_ENTER.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 2";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 1";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 1";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。Escape。
		*/
		public void CreateDigitalButtonEscape(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_ESCAPE.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 1";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 0";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 0";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。Sub1。
		*/
		public void CreateDigitalButtonSub1(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_SUB1.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 3";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 3";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 3";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。Sub2。
		*/
		public void CreateDigitalButtonSub2(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_SUB2.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 0";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 2";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 2";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。左メニュー。
		*/
		public void CreateDigitalButtonLeftMenu(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LMENU.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 8";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 6";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 10";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** デジタルボタン。右メニュー。
		*/
		public void CreateDigitalButtonRightMenu(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RMENU.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 9";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 7";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 11";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。左スティックＸ。
		*/
		public void CreateLeftStickAxisX(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LSX.GetItem(a_pad_index,a_pad_type);

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.1f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Type.JoyStickAxis;
			this.axis = Axis.XAxis;
			this.joyNum = a_pad_index + 1;
		}

		/** ジョイスティック。左スティックＹ。
		*/
		public void CreateLeftStickAxisY(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LSY.GetItem(a_pad_index,a_pad_type);

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.1f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = true;
			this.type = Type.JoyStickAxis;
			this.axis = Axis.YAxis;
			this.joyNum = a_pad_index + 1;
		}

		/** ジョイスティック。右スティックＸ。
		*/
		public void CreateRightStickAxisX(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RSX.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis3;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis4;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis3;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。右スティックＹ。
		*/
		public void CreateRightStickAxisY(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RSY.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = true;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis6;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = true;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis5;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.1f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = true;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis4;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。左スティックボタン。
		*/
		public void CreateLeftStickButton(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LSB.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 10";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 8";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 8";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。右スティックボタン。
		*/
		public void CreateRightStickButton(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RSB.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 11";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 9";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 9";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.KeyOrMouseButton;
					this.axis = Axis.None;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。左トリガー１ボタン。
		*/
		public void CreateLeftTrigger1Button(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LT1.GetItem(a_pad_index,a_pad_type);

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 4";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Type.KeyOrMouseButton;
			this.axis = Axis.None;
			this.joyNum = a_pad_index + 1;
		}

		/** ジョイスティック。右トリガー１ボタン。
		*/
		public void CreateRightTrigger1Button(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RT1.GetItem(a_pad_index,a_pad_type);

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick " + (a_pad_index + 1).ToString() + " button 5";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Type.KeyOrMouseButton;
			this.axis = Axis.None;
			this.joyNum = a_pad_index + 1;
		}

		/** ジョイスティック。左トリガー２ボタン。
		*/
		public void CreateLeftTrigger2Button(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_LT2.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis4;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis9;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis14;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}

		/** ジョイスティック。右トリガー２ボタン。
		*/
		public void CreateRightTrigger2Button(int a_pad_index,Fee.Input.Pad_InputManagerItemName.PadType a_pad_type)
		{
			this.m_Name = Fee.Input.Config.INPUTMANAGER_RT2.GetItem(a_pad_index,a_pad_type);

			switch(a_pad_type){
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_P:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis5;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_X:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis10;
					this.joyNum = a_pad_index + 1;
				}break;
			case Fee.Input.Pad_InputManagerItemName.PadType.Type_A:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Type.JoyStickAxis;
					this.axis = Axis.Axis15;
					this.joyNum = a_pad_index + 1;
				}break;
			}
		}
	}
}
#endif

