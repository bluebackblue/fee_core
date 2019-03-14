

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

		/** ButtonName
		*/
		public class ButtonName
		{
			public const string LEFT = "left";
			public const string RIGHT = "right";
			public const string UP = "up";
			public const string DOWN = "down";

			public const string ENTER = "enter";
			public const string ESCAPE = "escape";
			public const string SUB1 = "sub1";
			public const string SUB2 = "sub2";

			public const string LEFT_MENU = "left_menu";
			public const string RIGHT_MENU = "right_menu";

			public const string LEFT_STICK_AXIS_X = "left_stick_axis_x";
			public const string LEFT_STICK_AXIS_Y = "left_stick_axis_y";
			public const string RIGHT_STICK_AXIS_X = "right_stick_axis_x";
			public const string RIGHT_STICK_AXIS_Y = "right_stick_axis_y";

			public const string LEFT_STICK_BUTTON = "left_stick_button";
			public const string RIGHT_STICK_BUTTON = "right_stick_button";

			public const string LEFT_TRIGGER1_BUTTON = "left_trigger1_button";
			public const string RIGHT_TRIGGER1_BUTTON = "right_trigger1_button";
			public const string LEFT_TRIGGER2_AXIS = "left_trigger2_axis";
			public const string RIGHT_TRIGGER2_AXIS = "right_trigger2_axis";
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
		public void CreateDigitalButtonLeft()
		{
			this.m_Name = ButtonName.LEFT;
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
		}

		/** デジタルボタン。右。
		*/
		public void CreateDigitalButtonRight()
		{
			this.m_Name = ButtonName.RIGHT;
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
		}

		/** デジタルボタン。上。
		*/
		public void CreateDigitalButtonUp()
		{
			this.m_Name = ButtonName.UP;
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
		}

		/** デジタルボタン。下。
		*/
		public void CreateDigitalButtonDown()
		{
			this.m_Name = ButtonName.DOWN;
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
		}

		/** デジタルボタン。Enter。
		*/
		public void CreateDigitalButtonEnter()
		{
			this.m_Name = ButtonName.ENTER;
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
		}

		/** デジタルボタン。Escape。
		*/
		public void CreateDigitalButtonEscape()
		{
			this.m_Name = ButtonName.ESCAPE;
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
		}

		/** デジタルボタン。Sub1。
		*/
		public void CreateDigitalButtonSub1()
		{
			this.m_Name = ButtonName.SUB1;
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
		}

		/** デジタルボタン。Sub2。
		*/
		public void CreateDigitalButtonSub2()
		{
			this.m_Name = ButtonName.SUB2;
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
		}

		/** デジタルボタン。左メニュー。
		*/
		public void CreateDigitalButtonLeftMenu()
		{
			this.m_Name = ButtonName.LEFT_MENU;
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
		}

		/** デジタルボタン。右メニュー。
		*/
		public void CreateDigitalButtonRightMenu()
		{
			this.m_Name = ButtonName.RIGHT_MENU;
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
		}

		/** ジョイスティック。左スティックＸ。
		*/
		public void CreateLeftStickAxisX()
		{
			this.m_Name = ButtonName.LEFT_STICK_AXIS_X;
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
			this.axis = Fee.Input.EditInputManager_Item.Axis.XAxis;
			this.joyNum = 0;
		}

		/** ジョイスティック。左スティックＹ。
		*/
		public void CreateLeftStickAxisY()
		{
			this.m_Name = ButtonName.LEFT_STICK_AXIS_Y;
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
			this.axis = Fee.Input.EditInputManager_Item.Axis.YAxis;
			this.joyNum = 0;
		}

		/** ジョイスティック。右スティックＸ。
		*/
		public void CreateRightStickAxisX()
		{
			this.m_Name = ButtonName.RIGHT_STICK_AXIS_X;
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
		}

		/** ジョイスティック。右スティックＹ。
		*/
		public void CreateRightStickAxisY()
		{
			this.m_Name = ButtonName.RIGHT_STICK_AXIS_Y;
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
		}

		/** ジョイスティック。左スティックボタン。
		*/
		public void CreateLeftStickButton()
		{
			this.m_Name = ButtonName.LEFT_STICK_BUTTON;
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
		}

		/** ジョイスティック。右スティックボタン。
		*/
		public void CreateRightStickButton()
		{
			this.m_Name = ButtonName.RIGHT_STICK_BUTTON;
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
		}

		/** ジョイスティック。左トリガー１ボタン。
		*/
		public void CreateLeftTrigger1Button()
		{
			this.m_Name = ButtonName.LEFT_TRIGGER1_BUTTON;
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
		public void CreateRightTrigger1Button()
		{
			this.m_Name = ButtonName.RIGHT_TRIGGER1_BUTTON;
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
		public void CreateLeftTrigger2Button()
		{
			this.m_Name = ButtonName.LEFT_TRIGGER2_AXIS;
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
			this.invert = true;
			this.type = Fee.Input.EditInputManager_Item.Type.JoyStickAxis;
			this.axis = Fee.Input.EditInputManager_Item.Axis.Axis3;
			this.joyNum = 0;
		}

		/** ジョイスティック。右トリガー２ボタン。
		*/
		public void CreateRightTrigger2Button()
		{
			this.m_Name = ButtonName.RIGHT_TRIGGER2_AXIS;
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
			this.axis = Fee.Input.EditInputManager_Item.Axis.Axis3;
			this.joyNum = 0;
		}
	}
	#endif
}

