using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。
*/


/** NNetwork
*/
namespace NNetwork
{
	/** Network
	*/
	public class Network
	{
		/** [シングルトン]s_instance
		*/
		private static Network s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Network();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Network GetInstance()
		{
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

		/** Mode
		*/
		private enum Mode
		{
			None,

			AutoJoinRandomRoom,
		};

		/** mode
		*/
		private Mode mode;

		/** control_autojoinrandomroom
		*/
		private AutoJoinRandomRoom control_autojoinrandomroom;

		/** ルート。
		*/
		public GameObject root_gameobject;

		/** player_prefab
		*/
		private GameObject player_prefab;

		/** disconnect_request
		*/
		private bool disconnect_request;

		/** [シングルトン]constructor
		*/
		private Network()
		{
			//mode
			this.mode = Mode.None;

			//control_autojoinrandomroom
			this.control_autojoinrandomroom = null;

			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Network";
			GameObject.DontDestroyOnLoad(this.root_gameobject);

			//プレイヤプレハブ。
			this.player_prefab = null;

			//disconnect_request
			this.disconnect_request = false;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			GameObject.Destroy(this.root_gameobject);
		}

		/** プレイヤプレハブ。設定。
		*/
		public void SetPlayerPrefab(GameObject a_player_prefab)
		{
			this.player_prefab = a_player_prefab;
		}

		/** プレイヤプレハブ。取得。
		*/
		public GameObject GetPlayerPrefab()
		{
			return this.player_prefab;
		}

		/** ルート。取得。
		*/
		public Transform GetRoot()
		{
			return this.root_gameobject.GetComponent<Transform>();
		}

		/** 開始。
		*/
		public void Start_AutoJoinRandomRoom()
		{
			if(this.mode == Mode.None){
				this.mode = Mode.AutoJoinRandomRoom;
				this.disconnect_request = false;
				this.control_autojoinrandomroom = new AutoJoinRandomRoom();
			}
		}

		/** 切断リクエスト。設定。
		*/
		public void DisconnectRequest()
		{
			this.disconnect_request = true;
		}

		/** 切断リクエスト。チェック。
		*/
		public bool IsDisconnectRequest()
		{
			return this.disconnect_request;
		}

		/** 処理。チェック。
		*/
		public bool IsBusy()
		{
			if(this.mode == Mode.None){
				return false;
			}
			return true;
		}

		/** 更新。
		*/
		public void Main()
		{
			switch(this.mode){
			case Mode.AutoJoinRandomRoom:
				{
					if(this.control_autojoinrandomroom.Main() == false){
						this.control_autojoinrandomroom = null;
						this.mode = Mode.None;
					}
				}break;
			}
		}
	}
}

