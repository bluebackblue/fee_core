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
	/** PlayerPrefab
	*/
	public class PlayerPrefab : Photon.MonoBehaviour
	{
		/** トランスフォーム。
		*/
		private Transform mytransform;

		/** is_mine
		*/
		private bool is_mine;

		/** playerlist_index
		*/
		private int playerlist_index;

		/** photon_view
		*/
		private PhotonView photon_view;

		/** 開始。
		*/
		void Start()
		{
			Tool.Log("PlayerPrefab", "Start");

			//トランスフォーム。
			this.mytransform = this.GetComponent<Transform>();

			//親。設定。
			Transform t_root = NNetwork.Network.GetInstance().GetRoot();
			this.GetComponent<Transform>().SetParent(t_root);

			//プレイヤインデックス取得。
			this.playerlist_index = NNetwork.Network.GetInstance().AddPlayerPrefab(this);

			//名前設定。
			this.name = "PlayerPrefab_" + this.playerlist_index.ToString();

			//photon_view
			this.photon_view = this.GetComponent<PhotonView>();

			//is_mine
			this.is_mine = photon_view.isMine;
		}

		/** 削除。
		*/
		private void OnDestroy()
		{
			Tool.Log("PlayerPrefab","OnDestroy");

			NNetwork.Network.GetInstance().RemovePlayerPrefab(this);
		}

		/** 位置。取得。
		*/
		public Vector3 GetPosition()
		{
			return this.mytransform.position;
		}

		/** 回転。取得。
		*/
		public Quaternion GetQuaternion()
		{
			return this.mytransform.rotation;
		}

		/** スケール。取得。
		*/
		public Vector3 GetScale()
		{
			return this.mytransform.transform.localScale;
		}

		/** 位置。設定。
		*/
		public void SetPosition(float a_x,float a_y,float a_z)
		{
			this.mytransform.transform.position = new Vector3(a_x,a_y,a_z);
		}

		/** 回転。設定。
		*/
		public void SetQuaternion(ref Quaternion a_quaternion)
		{
			this.mytransform.transform.rotation = a_quaternion;
		}

		/** スケール。設定。
		*/
		public void SetScale(float a_x,float a_y,float a_z)
		{
			this.mytransform.transform.localScale = new Vector3(a_x,a_y,a_z);
		}

		/** 自分。チェック。
		*/
		public bool IsMine()
		{
			return this.is_mine;
		}

		/** 受信。
		*/
		[PunRPC]
		public void RecvInt(int a_key,int a_value)
		{
			OnRecvCallBack_Base t_callback = NNetwork.Network.GetInstance().GetRecvCallBack();
			if(t_callback != null){
				t_callback.OnRecvInt(this.playerlist_index,a_key,a_value);
			}
		}

		/** 受信。
		*/
		[PunRPC]
		public void RecvString(int a_key,string a_value)
		{
			OnRecvCallBack_Base t_callback = NNetwork.Network.GetInstance().GetRecvCallBack();
			if(t_callback != null){
				t_callback.OnRecvString(this.playerlist_index,a_key,a_value);
			}
		}

		/** 送信。
		*/
		public void SendInt(int a_key,int a_value)
		{
			if(this.photon_view != null){
				this.photon_view.RPC("RecvInt",PhotonTargets.All,a_key,a_value);
			}
		}

		/** 送信。
		*/
		public void SendString(int a_key,string a_value)
		{
			if(this.photon_view != null){
				this.photon_view.RPC("RecvString",PhotonTargets.All,a_key,a_value);
			}
		}
	}
}

