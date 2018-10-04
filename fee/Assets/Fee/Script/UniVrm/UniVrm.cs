using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。
*/


/** NVrm
*/
namespace NUniVrm
{
	/** UniVrm
	*/
	public class UniVrm
	{
		/** [シングルトン]s_instance
		*/
		private static UniVrm s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new UniVrm();
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
		public static UniVrm GetInstance()
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
		private UniVrm()
		{
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
		}

		/** 作成。
		*/
		public void Create(byte[] a_binary)
		{
			#if(USE_UNIVRM)
			{
				//context
				VRM.VRMImporterContext t_context = new VRM.VRMImporterContext(/*UniGLTF.UnityPath.FromFullpath(t_full_path)*/);

				//parse
				t_context.ParseGlb(a_binary);

				VRM.VRMImporter.LoadVrmAsync(t_context,(GameObject a_gameobject) => {
					//ロード完了。
					a_gameobject.transform.rotation = Quaternion.Euler(0,180,0);
				});
			}
			#endif
		}
	}
}

