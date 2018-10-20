using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号。
*/


/** NCrypt
*/
namespace NCrypt
{
	/** Crypt
	*/
	public class Crypt : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Crypt s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Crypt();
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
		public static Crypt GetInstance()
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
		private GameObject root_gameobject;
		private Transform root_transform;

		/** security
		*/
		private GameObject security_gameobject;
		private MonoBehaviour_Security security_script;

		/** work_list
		*/
		private List<Work> work_list;

		/** add_list
		*/
		private List<Work> add_list;

		/** [シングルトン]constructor
		*/
		private Crypt()
		{
			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Crypt";
			GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<Transform>();

			//security
			{
				this.security_gameobject = new GameObject();
				this.security_gameobject.name = "Crypt_Security";
				this.security_script = this.security_gameobject.AddComponent<MonoBehaviour_Security>();
				this.security_gameobject.GetComponent<Transform>().SetParent(this.root_transform);
			}

			//work_list
			this.work_list = new List<Work>();

			//add_list
			this.add_list = new List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//削除リクエスト。
			this.security_gameobject.GetComponent<Transform>().SetParent(null);
			GameObject.DontDestroyOnLoad(this.security_gameobject);
			this.security_script.DeleteRequest();

			//ルート削除。
			GameObject.Destroy(this.root_gameobject);
		}

		/** MonoSecurity。取得。
		*/
		public MonoBehaviour_Security GetMonoIo()
		{
			return this.security_script;
		}

		/** リクエスト。ロードローカル。バイナリファイル。
		*/
		public Item RequestEncryptPublicKey(byte[] a_binary,string a_key)
		{
			Work t_work = new Work();
			t_work.RequestEncryptPublicKey(a_binary,a_key);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** リクエスト。ロードローカル。テキストファイル。
		*/
		public Item RequestDecryptPrivateKey(byte[] a_binary,string a_key)
		{
			Work t_work = new Work();
			t_work.RequestDecryptPrivateKey(a_binary,a_key);
			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** TODO:暗号鍵作成。
		*/
		#if(UNITY_EDITOR)
		public static bool CreateNewKey(out string a_public_key,out string a_private_key)
		{
			try{
				using(System.Security.Cryptography.RSACryptoServiceProvider t_rsa = new System.Security.Cryptography.RSACryptoServiceProvider(1024)){
					a_public_key = t_rsa.ToXmlString(false);
					a_private_key = t_rsa.ToXmlString(true);
					return true;
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			a_public_key = null;
			a_private_key = null;
			return false;
		}
		#endif
	}
}

