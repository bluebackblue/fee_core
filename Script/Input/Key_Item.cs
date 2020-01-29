

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。キー。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** UnityEngine_InputSystem
	*/
	#if(USE_DEF_FEE_INPUTSYSTEM)
		#if((UNITY_2018_3)||(UNITY_2018_4))
			using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
		#else
			using UnityEngine_InputSystem = UnityEngine.InputSystem;
		#endif
	#endif

	/** Key_Item
	*/
	public class Key_Item
	{
		/** digital
		*/
		public Digital_Button digital;

		/** inputsystem_key
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public UnityEngine_InputSystem.Key inputsystem_key;
		#endif

		/** inputmanager_key
		*/
		public UnityEngine.KeyCode inputmanager_key;

		/** constructor
		*/
		public Key_Item(Key_Type a_key_type)
		{
			//digital
			this.digital.Reset();

			//key
			switch(a_key_type){
			case Key_Type.A:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.A;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.A;
				}break;
			case Key_Type.B:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.B;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.B;
				}break;
			case Key_Type.C:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.C;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.C;
				}break;
			case Key_Type.D:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.D;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.D;
				}break;
			case Key_Type.E:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.E;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.E;
				}break;
			case Key_Type.F:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.F;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.F;
				}break;
			case Key_Type.G:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.G;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.G;
				}break;
			case Key_Type.H:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.H;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.H;
				}break;
			case Key_Type.I:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.I;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.I;
				}break;
			case Key_Type.J:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.J;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.J;
				}break;
			case Key_Type.K:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.K;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.K;
				}break;
			case Key_Type.L:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.L;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.L;
				}break;
			case Key_Type.M:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.M;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.M;
				}break;
			case Key_Type.N:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.N;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.N;
				}break;
			case Key_Type.O:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.O;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.O;
				}break;
			case Key_Type.P:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.P;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.P;
				}break;
			case Key_Type.Q:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Q;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Q;
				}break;
			case Key_Type.R:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.R;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.R;
				}break;
			case Key_Type.S:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.S;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.S;
				}break;
			case Key_Type.T:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.T;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.T;
				}break;
			case Key_Type.U:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.U;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.U;
				}break;
			case Key_Type.V:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.V;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.V;
				}break;
			case Key_Type.W:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.W;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.W;
				}break;
			case Key_Type.X:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.X;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.X;
				}break;
			case Key_Type.Y:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Y;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Y;
				}break;
			case Key_Type.Z:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Z;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Z;
				}break;


			case Key_Type.Enter:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Enter;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Return;
				}break;
			case Key_Type.Space:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Space;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Space;
				}break;
			case Key_Type.LeftShift:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.LeftShift;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftShift;
				}break;
			case Key_Type.RightShift:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.RightShift;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightShift;
				}break;
			case Key_Type.LeftCtrl:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.LeftCtrl;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftControl;
				}break;
			case Key_Type.RightCtrl:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.RightCtrl;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightControl;
				}break;
			case Key_Type.LeftAlt:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.LeftAlt;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.LeftAlt;
				}break;
			case Key_Type.RightAlt:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.RightAlt;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.RightAlt;
				}break;
			case Key_Type.Tab:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Tab;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Tab;
				}break;
			case Key_Type.Esc:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Escape;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Escape;
				}break;
			case Key_Type.Back:
				{
					#if(USE_DEF_FEE_INPUTSYSTEM)
					this.inputsystem_key = UnityEngine_InputSystem.Key.Backspace;
					#endif
					this.inputmanager_key = UnityEngine.KeyCode.Backspace;
				}break;








			default:
				{
					Tool.Assert(false);
				}break;
			}
		}
	}
}

