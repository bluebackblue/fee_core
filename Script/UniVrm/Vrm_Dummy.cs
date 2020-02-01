

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。ダミー。
*/


/** Fee.UniVrm
*/
#if(!USE_DEF_FEE_UNIVRM)
namespace Fee.UniVrm
{
	/** VRM
	*/
	namespace VRM
	{
		/** RenderType
		*/
		public class RenderType
		{
			public bool enabled;
		}

		/** MeshType
		*/
		public class MeshType
		{
			public System.Collections.Generic.List<RenderType> Renderers;
		}

		/** VRMMeta
		*/
		public class VRMMeta
		{
		}
		
		/** VRMImporterContext
		*/
		public class VRMImporterContext
		{
			/** Root
			*/
			public UnityEngine.GameObject Root;

			/** Meshes
			*/
			public System.Collections.Generic.List<MeshType> Meshes;

			/** Dispose
			*/
			public void Dispose(){}

			/** ParseGlb
			*/
			public void ParseGlb(byte[] a_binary){}

			/** Load
			*/
			public void Load(){
				//USE_DEF_FEE_UNIVRM
				Tool.Assert(false);
			}

			/** LoadCoroutine
			*/
			public System.Collections.IEnumerator LoadCoroutine(System.Action a_onloadend,System.Action<System.Exception> a_onloaderror)
			{
				//USE_DEF_FEE_UNIVRM
				Tool.Assert(false);
				yield break;
			}
		}
	}
}
#endif

