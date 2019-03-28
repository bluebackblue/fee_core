

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
	/** EditInputManager_Item
	*/
	#if(UNITY_EDITOR)
	public class EditInputManager_Item
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
		public EditInputManager_Item()
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
		public void CreateDigitalButtonLeft(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LEFT[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis7;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis6;
					this.joyNum = 0;

				}break;
			}
		}

		/** デジタルボタン。右。
		*/
		public void CreateDigitalButtonRight(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RIGHT[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis7;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis6;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。上。
		*/
		public void CreateDigitalButtonUp(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_UP[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis8;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis7;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。下。
		*/
		public void CreateDigitalButtonDown(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_DOWN[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis8;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis7;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。Enter。
		*/
		public void CreateDigitalButtonEnter(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_ENTER[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 2";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 1";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。Escape。
		*/
		public void CreateDigitalButtonEscape(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_ESCAPE[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 1";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 0";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。Sub1。
		*/
		public void CreateDigitalButtonSub1(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_SUB1[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 0";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 2";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。Sub2。
		*/
		public void CreateDigitalButtonSub2(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_SUB2[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 3";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 3";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。左メニュー。
		*/
		public void CreateDigitalButtonLeftMenu(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LMENU[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 8";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 6";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** デジタルボタン。右メニュー。
		*/
		public void CreateDigitalButtonRightMenu(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RMENU[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 9";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 7";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。左スティックＸ。
		*/
		public void CreateLeftStickAxisX(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LSX[(int)a_padtype];

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
			this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
			this.axis = Fee.Input.EditInputManager_Item.Axis.XAxis;
			this.joyNum = 0;
		}

		/** ジョイスティック。左スティックＹ。
		*/
		public void CreateLeftStickAxisY(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LSY[(int)a_padtype];

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
			this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
			this.axis = Fee.Input.EditInputManager_Item.Axis.YAxis;
			this.joyNum = 0;
		}

		/** ジョイスティック。右スティックＸ。
		*/
		public void CreateRightStickAxisX(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RSX[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis3;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis4;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。右スティックＹ。
		*/
		public void CreateRightStickAxisY(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RSY[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis6;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis5;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。左スティックボタン。
		*/
		public void CreateLeftStickButton(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LSB[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 10";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 8";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。右スティックボタン。
		*/
		public void CreateRightStickButton(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RSB[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 11";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
				{
					this.descriptiveName = "";
					this.descriptiveNegativeName = "";
					this.negativeButton = "";
					this.positiveButton = "joystick button 9";
					this.altNegativeButton = "";
					this.altPositiveButton = "";
					this.gravity = 0.0f;
					this.dead = 0.001f;
					this.sensitivity = 1.0f;
					this.snap = false;
					this.invert = false;
					this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
					this.axis = Fee.Input.EditInputManager_Item.Axis.None;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。左トリガー１ボタン。
		*/
		public void CreateLeftTrigger1Button(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LT1[(int)a_padtype];

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 4";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
			this.axis = Fee.Input.EditInputManager_Item.Axis.None;
			this.joyNum = 0;
		}

		/** ジョイスティック。右トリガー１ボタン。
		*/
		public void CreateRightTrigger1Button(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RT1[(int)a_padtype];

			this.descriptiveName = "";
			this.descriptiveNegativeName = "";
			this.negativeButton = "";
			this.positiveButton = "joystick button 5";
			this.altNegativeButton = "";
			this.altPositiveButton = "";
			this.gravity = 0.0f;
			this.dead = 0.001f;
			this.sensitivity = 1.0f;
			this.snap = false;
			this.invert = false;
			this.type = Fee.Input.EditInputManager_Item.Type.KeyOrMouseButton;
			this.axis = Fee.Input.EditInputManager_Item.Axis.None;
			this.joyNum = 0;
		}

		/** ジョイスティック。左トリガー２ボタン。
		*/
		public void CreateLeftTrigger2Button(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_LT2[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis4;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis9;
					this.joyNum = 0;
				}break;
			}
		}

		/** ジョイスティック。右トリガー２ボタン。
		*/
		public void CreateRightTrigger2Button(Pad.PadType a_padtype)
		{
			this.m_Name = Config.INPUTMANAGER_RT2[(int)a_padtype];

			switch(a_padtype){
			case Pad.PadType.Type0:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis5;
					this.joyNum = 0;
				}break;
			case Pad.PadType.Type1:
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
					this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
					this.axis = Fee.Input.EditInputManager_Item.Axis.Axis10;
					this.joyNum = 0;
				}break;
			}
		}
	}
	#endif
}

