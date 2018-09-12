using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。プレイヤー
*/


/** NNetwork
*/
namespace NNetwork
{
	/** Player
	*/
	public class Player : Photon.Pun.MonoBehaviourPun /* , Photon.Pun.IPunObservable*/
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
		private Photon.Pun.PhotonView photon_view;

		/** 開始。
		*/
		void Start()
		{
			Tool.Log("Player", "Start");

			//トランスフォーム。
			this.mytransform = this.GetComponent<Transform>();

			//親。設定。
			Transform t_root = NNetwork.Network.GetInstance().GetRoot();
			this.GetComponent<Transform>().SetParent(t_root);

			//photon_view
			this.photon_view = this.GetComponent<Photon.Pun.PhotonView>();

			//is_mine
			this.is_mine = this.photon_view.IsMine;

			//playerlist_index
			this.playerlist_index = -1;

			//this.name
			this.name = "Player";

			{
				//プレイヤ－インデックス取得。
				this.playerlist_index = NNetwork.Network.GetInstance().AddPlayer(this);

				//名前設定。
				if(this.playerlist_index >= 0){
					this.name = "Player_" + this.playerlist_index.ToString();
				}
			}
		}

		/** 削除。
		*/
		private void OnDestroy()
		{
			Tool.Log("Player","OnDestroy");

			NNetwork.Network.GetInstance().RemovePlayer(this);
		}

		/** OnPhotonSerializeView
		*/
		/*
		public void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
		}
		*/

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
		[Photon.Pun.PunRPC]
		public void RecvInt(int a_key,int a_value)
		{
			OnRecvCallBack_Base t_callback = NNetwork.Network.GetInstance().GetRecvCallBack();
			if(t_callback != null){
				t_callback.OnRecvInt(this.playerlist_index,a_key,a_value);
			}
		}

		/** 受信。
		*/
		[Photon.Pun.PunRPC]
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
				this.photon_view.RPC("RecvInt",Photon.Pun.RpcTarget.All,a_key,a_value);
			}
		}

		/** 送信。
		*/
		public void SendString(int a_key,string a_value)
		{
			if(this.photon_view != null){
				this.photon_view.RPC("RecvString",Photon.Pun.RpcTarget.All,a_key,a_value);
			}
		}
	}
}

