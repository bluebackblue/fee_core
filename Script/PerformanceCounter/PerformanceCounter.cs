

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** Fee.PerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace Fee.PerformanceCounter
{
	/** PerformanceCounter
	*/
	public class PerformanceCounter : Config
	{
		/** [シングルトン]s_instance
		*/
		private static PerformanceCounter s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new PerformanceCounter();
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
		public static PerformanceCounter GetInstance()
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

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;

		/** camera_gameobject
		*/
		private UnityEngine.GameObject camera_gameobject;
		private Camera_MonoBehaviour camera_monobehaviour;

		/** フレームデータ。
		*/
		private FrameData framedata;

		/** フレーム終了待ち。
		*/
		private UnityEngine.WaitForEndOfFrame wait_for_endframe;

		/** delete_request
		*/
		private bool delete_request;

		/** material
		*/
		private UnityEngine.Material material;

		/** [シングルトン]constructor
		*/
		private PerformanceCounter()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "PerformanceCounter";
			UnityEngine.Transform t_root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);

			//マテリアル。
			this.material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_PATH);

			//フレームデータ。
			this.framedata = new FrameData();

			//フレーム終了待ち。
			this.wait_for_endframe = new UnityEngine.WaitForEndOfFrame();

			//delete_request
			this.delete_request = false;

			//カメラ。
			this.camera_gameobject = Fee.Instantiate.Instantiate.CreateOrthographicCameraObject("Camera",t_root_transform,999.0f);
			this.camera_monobehaviour = this.camera_gameobject.AddComponent<Camera_MonoBehaviour>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.delete_request = true;
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** フレームデータ。取得。
		*/
		public FrameData GetFrameData()
		{
			return this.framedata;
		}

		/** Unity_Start
		*/
		public System.Collections.IEnumerator Unity_Start()
		{
			while(this.delete_request == false){
				//フレーム終了待ち。
				yield return this.wait_for_endframe;
				
				//次のフレーム。
				this.framedata.NetFrame();
			}
		}

		/** Unity_FixedUpdate
		*/
		public void Unity_FixedUpdate()
		{
			this.framedata.fixedupdate = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_Update
		*/
		public void Unity_Update()
		{
			this.framedata.update = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_LateUpdate
		*/
		public void Unity_LateUpdate()
		{
			this.framedata.lateupdate = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_OnPreRender
		*/
		public void Unity_OnPreRender()
		{
			this.framedata.onprerender = UnityEngine.Time.realtimeSinceStartup;
		}

		/** Unity_OnPostRender
		*/
		public void Unity_OnPostRender()
		{
			this.framedata.onpostrender = UnityEngine.Time.realtimeSinceStartup;
			this.Draw();
		}

		/** 描画。
		*/
		private void Draw()
		{
			if(Fee.Graphic.Gl.PushMatrix() == true){
				if(Fee.Graphic.Gl.LoadOrtho() == true){

					this.material.SetPass(0);

					if(Fee.Graphic.Gl.Begin(UnityEngine.GL.TRIANGLES) == true){
						{
							int t_index = 0;

							this.DrawBar(this.framedata.startframe - this.framedata.startframe_old,t_index,Config.COLOR_FRAME);
							this.DrawBar(1.0f / 60.0f,t_index,Config.COLOR_FRAME_BASE);

							t_index++;

							this.DrawBar(this.framedata.onpostrender - this.framedata.startframe,t_index,Config.COLOR_ONPOSTRENDER);
							this.DrawBar(this.framedata.onprerender - this.framedata.startframe,t_index,Config.COLOR_ONPRERENDER);
							this.DrawBar(this.framedata.lateupdate - this.framedata.startframe,t_index,Config.COLOR_LATEUPDATE);
							this.DrawBar(this.framedata.fixedupdate - this.framedata.startframe,t_index,Config.COLOR_FIXEDUPDATE);
							this.DrawBar(this.framedata.update - this.framedata.startframe,t_index,Config.COLOR_UPDATE);
						}

						Fee.Graphic.Gl.End();
					}
				}
				Fee.Graphic.Gl.PopMatrix();
			}
		}

		/** DrawBar
		*/
		private void DrawBar(float a_length,int a_index,UnityEngine.Color a_color)
		{
			try{
				float t_length = a_length;
				const float t_length_max = 1.0f / 60.0f;
				float t_per = t_length / t_length_max;

				float t_w = 0.3f * t_per;
				float t_h = 5.0f * 1.0f / (float)UnityEngine.Screen.height;

				float t_offset_y = (t_h + 0.0005f) * a_index;

				float t_x_1 = 0.0f;
				float t_y_1 = 1.0f - 0.0f - t_offset_y;

				float t_x_2 = t_w;
				float t_y_2 = 1.0f - 0.0f - t_offset_y;

				float t_x_3 = 0.0f;
				float t_y_3 = 1.0f - t_h - t_offset_y;

				float t_x_4 = t_w;
				float t_y_4 = 1.0f - t_h - t_offset_y;

				UnityEngine.GL.Color(a_color);
			
				{
					//1:左上。
					UnityEngine.GL.Vertex3(t_x_1,t_y_1,0.0f);

					//2:右上。
					UnityEngine.GL.Vertex3(t_x_2,t_y_2,0.0f);

					//3:左下。
					UnityEngine.GL.Vertex3(t_x_3,t_y_3,0.0f);
				}

				{
					//3:左下。
					UnityEngine.GL.Vertex3(t_x_3,t_y_3,0.0f);

					//2:右上。
					UnityEngine.GL.Vertex3(t_x_2,t_y_2,0.0f);

					//4:右下。
					UnityEngine.GL.Vertex3(t_x_4,t_y_4,0.0f);	
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

