

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。
*/


/** Fee.Input
*/
#if(USE_DEF_FEE_INPUTSYSTEM)
namespace Fee.Input
{
	/** Pad_InputSystem
	*/
	public class Pad_InputSystem
	{
		/** paddevice_list
		*/
		private System.Collections.Generic.List<UnityEngine.InputSystem.Gamepad> paddevice_list;

		/** constructor
		*/
		public Pad_InputSystem()
		{
			this.paddevice_list = new System.Collections.Generic.List<UnityEngine.InputSystem.Gamepad>();
		}

		/** Delete
		*/
		public void Delete()
		{
			foreach(UnityEngine.InputSystem.InputDevice t_device in UnityEngine.InputSystem.InputSystem.devices){
				UnityEngine.InputSystem.Gamepad t_gamepad = t_device as UnityEngine.InputSystem.Gamepad;
				if(t_gamepad != null){
					t_gamepad.SetMotorSpeeds(0.0f,0.0f);
				}
			}
		}

		/** デバイスリスト。更新。
		*/
		public void UpdateDeviceList()
		{
			this.paddevice_list.Clear();

			foreach(UnityEngine.InputSystem.InputDevice t_device in UnityEngine.InputSystem.InputSystem.devices){
				UnityEngine.InputSystem.Gamepad t_gamepad = t_device as UnityEngine.InputSystem.Gamepad;
				if(t_gamepad != null){
					this.paddevice_list.Add(t_gamepad);
				}
			}

			this.paddevice_list.Sort((UnityEngine.InputSystem.Gamepad a_test,UnityEngine.InputSystem.Gamepad a_target) => {
				int t_ret = a_test.deviceId - a_target.deviceId;
				if(t_ret == 0){
					t_ret = a_test.GetHashCode() - a_target.GetHashCode();
				}
				return t_ret;
			});
		}

		/** GetDevice
		*/
		public UnityEngine.InputSystem.Gamepad GetDevice(int a_index)
		{
			if(a_index < this.paddevice_list.Count){
				return this.paddevice_list[a_index];
			}
			return null;
		}
	}
}
#endif

