

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
{
	/** NetworkTool
	*/
	public class NetworkTool
	{
		/** ＷＥＢリクエスト作成。
		*/
		public static UnityEngine.Networking.UnityWebRequest CreateWebRequest(Fee.File.Path a_uri_path,UnityEngine.Networking.CertificateHandler a_certificate)
		{
			UnityEngine.Networking.UnityWebRequest t_webrequest = null;

			try{
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_uri_path.GetPath());

				if(a_certificate != null){
					t_webrequest.certificateHandler = a_certificate;
				}

				UnityEngine.Networking.UnityWebRequestAsyncOperation t_async = t_webrequest.SendWebRequest();

				while(t_async.isDone == false){
					 System.Threading.Thread.Sleep(100);
				}
			}catch(System.Exception t_exception){
				Tool.EditorLogError(t_exception.Message);
				t_webrequest = null;
			}

			return t_webrequest;
		}
	}
}
#endif

