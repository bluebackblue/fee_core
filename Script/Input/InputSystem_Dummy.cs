

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インプットシステム。ダミー。
*/


/** ダミー
*/
#if(false)
namespace InputSystemDummy
{
	/** PointerPhase
	*/
	public enum PointerPhase
	{
		Began,
		Moved,
		Stationary,
		Ended,
		Cancelled,
		None,
	}

	/** Device
	*/
	public class Device
	{
	}

	/** Status
	*/
	public struct Status
	{
		public bool isPressed;
		public float ReadValue(){return 0.0f;}
	}

	/** Status_Touchid
	*/
	public struct Status_Touchid
	{
		public int ReadValue(){return 0;}
	}

	/** Status_Pos
	*/
	public struct Status_Pos
	{
		public Status x;
		public Status y;
	}

	/** Status_Phase
	*/
	public struct Status_Phase
	{
		public PointerPhase ReadValue(){return PointerPhase.None;}
	}

	public struct Status_Scroll_Value
	{
		public float x;
		public float y;
	}

	/** Status_Scroll
	*/
	public struct Status_Scroll
	{
		public Status_Scroll_Value ReadValue(){return default(Status_Scroll_Value);}
	}

	/** Status_Dpad
	*/
	public struct Status_Dpad
	{
		public Status left;
		public Status right;
		public Status up;
		public Status down;
	}

	/** Controls
	*/
	public struct Controls
	{
		/** TouchControl
		*/
		public struct TouchControl
		{
			public Status_Phase phase;
			public Status_Touchid touchId;
			public Status_Pos position;
		}
	}

	/** Touchscreen
	*/
	public class Touchscreen : Device
	{
		public System.Collections.Generic.List<Controls.TouchControl> activeTouches;	
	}

	/** Gamepad
	*/
	public class Gamepad : Device
	{
		public Status_Dpad dpad;
		public Status buttonSouth;
		public Status buttonNorth;
		public Status buttonEast;
		public Status buttonWest;
		public Status selectButton;
		public Status startButton;
		public Status_Pos leftStick;
		public Status_Pos rightStick;
		public Status leftStickButton;
		public Status rightStickButton;
		public Status leftShoulder;
		public Status rightShoulder;
		public Status leftTrigger;
		public Status rightTrigger;

		public void SetMotorSpeeds(float a_a,float a_b)
		{
		}
	}

	/** Pointer
	*/
	public class Pointer : Device
	{
		public Status_Pos position;
		public Status_Phase phase;
	}

	/** Mouse
	*/
	public class Mouse : Device
	{
		public Status_Pos position;
		public Status leftButton;
		public Status rightButton;
		public Status middleButton;
		public Status_Scroll scroll;
	}

	/** Keyboard
	*/
	public class Keyboard : Device
	{
		public Status this[UnityEngine.Experimental.Input.Key key]
		{
			get
			{
				return default(Status);
			}
			set
			{
			}
		}
	}

	/** InputSystem
	*/
	public class InputSystem
	{
		public static Type GetDevice<Type>()
			where Type : Device
		{
			return default(Type);
		}
	}

	/** Key
	*/
	public struct Key
	{
		public static Key A = default(Key);
		public static Key D = default(Key);
		public static Key W = default(Key);
		public static Key S = default(Key);
		public static Key Enter = default(Key);
		public static Key Escape = default(Key);
		public static Key LeftShift = default(Key);
		public static Key LeftCtrl = default(Key);
		public static Key Space = default(Key);
		public static Key Backspace = default(Key);
	}
}
#endif


