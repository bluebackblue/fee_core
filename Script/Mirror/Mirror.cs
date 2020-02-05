

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

		/** material
		*/
		private UnityEngine.Material material;

		/** [シングルトン]constructor
		*/
		private Mirror()
		{
			this.material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_SIMPLE);
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** GetMaterial
		*/
		public UnityEngine.Material GetMaterial()
		{
			return this.material;
		}

		/** ミラー。作成。
		*/
		public MirrorCamera_MonoBehaviour CreateMirror(Fee.Mirror.RenderTextureSizeType a_size_type,UnityEngine.GameObject a_mirror_object,UnityEngine.Camera a_camera)
		{
			MirrorCamera_MonoBehaviour t_mirror_camera = MirrorCamera_MonoBehaviour.Create(a_size_type);
			{
				if(a_mirror_object != null){
					UnityEngine.Renderer t_renderer = a_mirror_object.GetComponent<UnityEngine.Renderer>();
					if(t_renderer != null){
						t_renderer.material = new UnityEngine.Material(this.material);
						t_renderer.material.SetTexture("texture_mirror",t_mirror_camera.GetRenderTexture());
					}
					t_mirror_camera.SetMirrorPlane(a_mirror_object.transform.up,a_mirror_object.transform.position);
				}
				t_mirror_camera.SetTarget(a_camera);
			}
			return t_mirror_camera;
		}
	}
}

