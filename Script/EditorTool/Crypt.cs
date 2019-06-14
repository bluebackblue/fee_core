

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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
		/** 公開鍵秘密鍵作成。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/EditorTool/Crypt/MakePublicKeyPrivateKey")]
		private static void MenuItem_MakePublicKeyPrivateKey()
		{
			string t_public_key;
			string t_private_key;
			if(CreateCryptKey(out t_public_key,out t_private_key) == true){

				//public
				{
					Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());
					Fee.JsonItem.JsonItem t_jsonitem_public = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_StringData(t_public_key));
					t_jsonitem.SetItem("public",t_jsonitem_public,false);

					Utility.WriteJsonFile(t_jsonitem,UnityEngine.Application.dataPath + "/Resources/public_key.json");
				}

				//private
				{
					Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());
					Fee.JsonItem.JsonItem t_jsonitem_private = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_StringData(t_private_key));
					t_jsonitem.SetItem("private",t_jsonitem_private,false);

					Utility.WriteJsonFile(t_jsonitem,UnityEngine.Application.dataPath + "/Resources/private_key.json");
				}
			
				UnityEditor.AssetDatabase.Refresh();
			}
		}
		#endif

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

