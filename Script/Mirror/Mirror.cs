

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブルーム。
*/


/** Fee.Mirror
*/
namespace Fee.Mirror
{
	/** Mirror
	*/
	public class Mirror
	{
		/** [シングルトン]s_instance
		*/
		private static Mirror s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Mirror();
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
		public static Mirror GetInstance()
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

		/** [シングルトン]constructor
		*/
		private Mirror()
		{
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** ミラー。作成。
		*/
		public MirrorObject_MonoBehaviour CreateMirror(Fee.Mirror.RenderTextureSizeType a_size_type,UnityEngine.GameObject a_mirror_object,UnityEngine.Camera a_look_camera,string a_name)
		{
			if(a_mirror_object != null){
				MirrorCamera_MonoBehaviour t_mirror_camera = MirrorCamera_MonoBehaviour.Create(a_size_type,a_name);

				//ミラーマテリアル。設定。
				UnityEngine.Renderer t_renderer = a_mirror_object.GetComponent<UnityEngine.Renderer>();
				if(t_renderer != null){
					t_renderer.material = new UnityEngine.Material(UnityEngine.Shader.Find(Config.SHADER_NAME_MIRROR));
					t_renderer.material.SetTexture("texture_mirror",t_mirror_camera.GetRenderTexture());
				}else{
					Tool.Assert(false);
				}

				//ミラーオブジェクト。設定。
				MirrorObject_MonoBehaviour t_mirror_object = a_mirror_object.AddComponent<MirrorObject_MonoBehaviour>();
				{
					t_mirror_object.mirror_camera = t_mirror_camera;
					t_mirror_object.look_camera = a_look_camera;
					t_mirror_object.look_transform = a_look_camera.GetComponent<UnityEngine.Transform>();
					t_mirror_object.enabled = false;
				}

				return t_mirror_object;
			}else{
				Tool.Assert(false);
			}

			return null;
		}
	}
}

