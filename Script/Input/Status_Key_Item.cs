

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。ステータス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Status_Key_Item
	*/
	public class Status_Key_Item
	{
		/** digital
		*/
		public Status_Digital_Button digital;

		/** inputsystem_key
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public UnityEngine.InputSystem.Key inputsystem_key;
		#endif

		/** inputmanager_key
		*/
		public UnityEngine.KeyCode inputmanager_key;

		/** constructor
		*/
		public Status_Key_Item(Status_Key_Type a_key_type)
		{
			//digital
			this.digital.Reset();

			//key
			switch(a_key_type){
			case Status_Key_Type.A:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.A;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.A;
				}break;
			case Status_Key_Type.B:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.B;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.B;
				}break;
			case Status_Key_Type.C:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.C;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.C;
				}break;
			case Status_Key_Type.D:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.D;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.D;
				}break;
			case Status_Key_Type.E:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.E;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.E;
				}break;
			case Status_Key_Type.F:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F;
				}break;
			case Status_Key_Type.G:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.G;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.G;
				}break;
			case Status_Key_Type.H:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.H;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.H;
				}break;
			case Status_Key_Type.I:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.I;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.I;
				}break;
			case Status_Key_Type.J:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.J;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.J;
				}break;
			case Status_Key_Type.K:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.K;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.K;
				}break;
			case Status_Key_Type.L:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.L;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.L;
				}break;
			case Status_Key_Type.M:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.M;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.M;
				}break;
			case Status_Key_Type.N:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.N;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.N;
				}break;
			case Status_Key_Type.O:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.O;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.O;
				}break;
			case Status_Key_Type.P:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.P;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.P;
				}break;
			case Status_Key_Type.Q:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Q;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Q;
				}break;
			case Status_Key_Type.R:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.R;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.R;
				}break;
			case Status_Key_Type.S:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.S;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.S;
				}break;
			case Status_Key_Type.T:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.T;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.T;
				}break;
			case Status_Key_Type.U:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.U;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.U;
				}break;
			case Status_Key_Type.V:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.V;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.V;
				}break;
			case Status_Key_Type.W:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.W;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.W;
				}break;
			case Status_Key_Type.X:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.X;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.X;
				}break;
			case Status_Key_Type.Y:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Y;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Y;
				}break;
			case Status_Key_Type.Z:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Z;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Z;
				}break;




			case Status_Key_Type.N0:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit0;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha0;
				}break;
			case Status_Key_Type.N1:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit1;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha1;
				}break;
			case Status_Key_Type.N2:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit2;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha2;
				}break;
			case Status_Key_Type.N3:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit3;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha3;
				}break;
			case Status_Key_Type.N4:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit4;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha4;
				}break;
			case Status_Key_Type.N5:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit5;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha5;
				}break;
			case Status_Key_Type.N6:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit6;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha6;
				}break;
			case Status_Key_Type.N7:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit7;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha7;
				}break;
			case Status_Key_Type.N8:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit8;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha8;
				}break;
			case Status_Key_Type.N9:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Digit9;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Alpha9;
				}break;




			case Status_Key_Type.F1:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F1;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F1;
				}break;
			case Status_Key_Type.F2:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F2;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F2;
				}break;
			case Status_Key_Type.F3:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F3;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F3;
				}break;
			case Status_Key_Type.F4:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F4;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F4;
				}break;
			case Status_Key_Type.F5:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F5;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F5;
				}break;
			case Status_Key_Type.F6:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F6;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F6;
				}break;
			case Status_Key_Type.F7:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F7;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F7;
				}break;
			case Status_Key_Type.F8:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F8;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F8;
				}break;
			case Status_Key_Type.F9:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F9;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F9;
				}break;
			case Status_Key_Type.F10:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F10;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F10;
				}break;
			case Status_Key_Type.F11:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F11;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F11;
				}break;
			case Status_Key_Type.F12:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.F12;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F12;
				}break;




			case Status_Key_Type.P0:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad0;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad0;
				}break;
			case Status_Key_Type.P1:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad1;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad1;
				}break;
			case Status_Key_Type.P2:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad2;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad2;
				}break;
			case Status_Key_Type.P3:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad3;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad3;
				}break;
			case Status_Key_Type.P4:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad4;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad4;
				}break;
			case Status_Key_Type.P5:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad5;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad5;
				}break;
			case Status_Key_Type.P6:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad6;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad6;
				}break;
			case Status_Key_Type.P7:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad7;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad7;
				}break;
			case Status_Key_Type.P8:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad8;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad8;
				}break;
			case Status_Key_Type.P9:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Numpad9;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Keypad9;
				}break;

			case Status_Key_Type.PadEnter:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadEnter;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadEnter;
				}break;
			case Status_Key_Type.PadPeriod:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadPeriod;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadPeriod;
				}break;
			case Status_Key_Type.PadPlus:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadPlus;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadPlus;
				}break;
			case Status_Key_Type.PadMinus:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadMinus;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadMinus;
				}break;
			case Status_Key_Type.PadMultiply:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadMultiply;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadMultiply;
				}break;
			case Status_Key_Type.PadDivide:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.NumpadDivide;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.KeypadDivide;
				}break;




			case Status_Key_Type.Up:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.UpArrow;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.UpArrow;
				}break;
			case Status_Key_Type.Down:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.DownArrow;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.DownArrow;
				}break;
			case Status_Key_Type.Left:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.LeftArrow;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftArrow;
				}break;
			case Status_Key_Type.Right:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.RightArrow;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightArrow;
				}break;




			case Status_Key_Type.Enter:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Enter;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Return;
				}break;
			case Status_Key_Type.Space:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Space;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Space;
				}break;
			case Status_Key_Type.LeftShift:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.LeftShift;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftShift;
				}break;
			case Status_Key_Type.RightShift:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.RightShift;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightShift;
				}break;
			case Status_Key_Type.LeftCtrl:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.LeftCtrl;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftControl;
				}break;
			case Status_Key_Type.RightCtrl:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.RightCtrl;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightControl;
				}break;
			case Status_Key_Type.LeftAlt:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.LeftAlt;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftAlt;
				}break;
			case Status_Key_Type.RightAlt:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.RightAlt;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightAlt;
				}break;
			case Status_Key_Type.Tab:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Tab;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Tab;
				}break;
			case Status_Key_Type.Esc:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Escape;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Escape;
				}break;
			case Status_Key_Type.Backspace:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Backspace;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Backspace;
				}break;
			case Status_Key_Type.Insert:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Insert;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Insert;
				}break;
			case Status_Key_Type.Delete:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Delete;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Delete;
				}break;
			case Status_Key_Type.Home:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Home;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Home;
				}break;
			case Status_Key_Type.End:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.End;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.End;
				}break;
			case Status_Key_Type.PageUp:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.PageUp;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.PageUp;
				}break;
			case Status_Key_Type.PageDown:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.PageDown;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.PageDown;
				}break;
			case Status_Key_Type.Pause:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine.InputSystem.Key.Pause;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Pause;
				}break;





			default:
				{
					Tool.Assert(false);
				}break;
			}
		}
	}
}

