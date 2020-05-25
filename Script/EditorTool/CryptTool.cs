

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。暗号鍵。
*/


/** Fee.EditorTool
*/
namespace Fee.EditorTool
{
	/** Crypt
	*/
	#if(UNITY_EDITOR)
	public class Crypt
	{
		/** 鍵作成。
		*/
		public static void CreatePublicKeyPrivateKey(Fee.File.Path a_assets_path,Fee.File.Path a_private_key)
		{
			string t_public_key;
			string t_private_key;
			if(CreateCryptKey(out t_public_key,out t_private_key) == true){

				//public
				{
					Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());
					Fee.JsonItem.JsonItem t_jsonitem_public = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_StringData(t_public_key));
					t_jsonitem.SetItem("public",t_jsonitem_public,false);

					Utility.WriteJsonFile(a_assets_path,t_jsonitem);
				}

				//private
				{
					Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());
					Fee.JsonItem.JsonItem t_jsonitem_private = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_StringData(t_private_key));
					t_jsonitem.SetItem("private",t_jsonitem_private,false);

					Utility.WriteJsonFile(a_private_key,t_jsonitem);
				}
			
				UnityEditor.AssetDatabase.Refresh();
			}
		}

		/** 暗号鍵作成。
		*/
		public static bool CreateCryptKey(out string a_public_key,out string a_private_key)
		{
			try{
				using(System.Security.Cryptography.RSACryptoServiceProvider t_rsa = new System.Security.Cryptography.RSACryptoServiceProvider(1024)){
					a_public_key = t_rsa.ToXmlString(false);
					a_private_key = t_rsa.ToXmlString(true);
					return true;
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			a_public_key = null;
			a_private_key = null;
			return false;
		}
	}
	#endif
}

