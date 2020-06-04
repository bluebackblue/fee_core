

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Input
	*/
	public class Input
	{
		/** [シングルトン]s_instance
		*/
		private static Input s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance(bool a_key,bool a_pad,bool a_mouse,bool a_touch)
		{
			if(s_instance == null){
				s_instance = new Input(a_key,a_pad,a_mouse,a_touch);
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Input GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** key
		*/
		public Key key;

		/** pad
		*/
		public Pad pad;

		/** mouse
		*/
		public Mouse mouse;

		/** touch
		*/
		public Touch touch;

		/** is_focus
		*/
		public bool is_focus;

		/** CallBackType
		*/
		public delegate void CallBackType();

		/** callback
		*/
		private CallBackType callback;

		/** debugview
		*/
		#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
		public DebugView_MonoBehaviour debugview;
		public UnityEngine.GameObject debugview_gameobject;
		#endif

		/** [シングルトン]constructor
		*/
		private Input(bool a_key,bool a_pad,bool a_mouse,bool a_touch)
		{
			//key
			if(a_key == true){
				this.key = new Key();
			}else{
				this.key = null;
			}

			//pad
			if(a_pad == true){
				this.pad = new Pad();
			}else{
				this.pad = null;
			}

			//mouse
			if(a_mouse == true){
				this.mouse = new Mouse();
			}else{
				this.mouse = null;
			}

			//touch
			if(a_touch == true){
				this.touch = new Touch();
			}else{
				this.touch = null;
			}

			//is_focus
			this.is_focus = true;

			//callback
			this.callback = null;

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				this.debugview_gameobject = new UnityEngine.GameObject("input_debugview");
				UnityEngine.GameObject.DontDestroyOnLoad(this.debugview_gameobject);

				this.debugview = this.debugview_gameobject.AddComponent<DebugView_MonoBehaviour>();
			}
			#endif

			//AddFirst
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().AddFirst(typeof(UnityEngine.Experimental.PlayerLoop.Update),typeof(PlayerLoopSystemType.Update),this.Update);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			if(this.touch != null){
				this.touch.Delete();
				this.touch = null;
			}

			if(this.mouse != null){
				this.mouse.Delete();
				this.mouse = null;
			}

			if(this.pad != null){
				this.pad.Delete();
				this.pad = null;
			}

			if(this.key != null){
				this.key.Delete();
				this.key = null;
			}

			//callback
			this.callback = null;

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				if(this.debugview_gameobject != null){
					UnityEngine.GameObject.DestroyImmediate(this.debugview_gameobject);
				}
			}
			#endif
		}

		/** SetCallBack
		*/
		public void SetCallBack(CallBackType a_callback)
		{
			this.callback += a_callback;
		}

		/** UnSetCallBack
		*/
		public void UnSetCallBack(CallBackType a_callback)
		{
			this.callback -= a_callback;
		}

		/** SetFocusFlag
		*/
		public void SetFocusFlag(bool a_is_focus)
		{
			this.is_focus = a_is_focus;
		}

		/** Update
		*/
		private void Update()
		{
			//key
			if(this.key != null){
				this.key.Main();
			}

			//pad
			if(this.pad != null){
				this.pad.Main();
			}

			//mouse
			if(this.mouse != null){
				this.mouse.Main();
			}

			//touch
			if(this.touch != null){
				this.touch.Main();
			}

			if(this.callback != null){
				this.callback();
			}
		}
	}
}

