using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test16

	通信

*/
public class test16 : main_base , NNetwork.OnRemoteCallBack_Base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ステータス。
	*/
	private NRender2D.Text2D status_text;

	/** player_text
	*/
	private NRender2D.Text2D[] player_text;

	/** player_list
	*/
	private GameObject[] player_list;

	/** 開始ボタン。
	*/
	private NUi.Button start_button;

	/** 修了ボタン。
	*/
	private NUi.Button end_button;

	/** Mode
	*/
	private enum Mode
	{
		Init,
		Wait,
		Start,
		Do,
		DisConnect,
		DisConnectNow,
		ChangeScene,
	};

	/** mode
	*/
	private Mode mode;

	/** InputMode
	*/
	private enum InputMode
	{
		Position,
		Rotate,
		Scale,
	};

	/** inputmode
	*/
	private InputMode inputmode;

	/** [NNetwork.OnRemoteCallBack_Base]リモートコール。
	*/
	public void OnRemoteCallInt(int a_playerlist_index,int a_key,int a_value)
	{
		Debug.Log("OnRemoteCallInt : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value.ToString());

		NNetwork.Player t_player = NNetwork.Network.GetInstance().GetPlayer(a_playerlist_index);
		if(t_player != null){
			if(a_value == 0){
				//白。
				this.player_list[a_playerlist_index].GetComponent<MeshRenderer>().material.color = Color.white;
			}else{
				//赤。
				this.player_list[a_playerlist_index].GetComponent<MeshRenderer>().material.color = Color.red;
			}
		}
	}

	/** [NNetwork.OnRemoteCallBack_Base]リモートコール。
	*/
	public void OnRemoteCallString(int a_playerlist_index,int a_key,string a_value)
	{
		Debug.Log("OnRemoteCallString : " + a_playerlist_index.ToString() + " : " + a_key.ToString() + " : " + a_value);
	}

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//キ。インスタンス作成。
		NInput.Key.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ネットワーク。インスタンス作成。
		NNetwork.Config.LOG_ENABLE = true;
		NNetwork.Network.CreateInstance();
		NNetwork.Network.GetInstance().SetRecvCallBack(this);

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//layerindex
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//ステータス。
		{
			int t_x = 100;
			int t_y = 100;

			this.status_text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.status_text.SetRect(t_x,t_y,0,0);
			this.status_text.SetText("");
		}

		//player_text
		{
			int t_x = 100;
			int t_y = 130;

			this.player_text = new NRender2D.Text2D[8];
			for(int ii=0;ii<player_text.Length;ii++){
				this.player_text[ii] = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
				this.player_text[ii].SetRect(t_x,t_y + 35*ii,0,0);
			}
		}

		//player_list
		{
			GameObject t_prefab = Resources.Load<GameObject>("box");

			this.player_list = new GameObject[8];
			for(int ii=0;ii<this.player_list.Length;ii++){
				this.player_list[ii] = GameObject.Instantiate(t_prefab);
				this.player_list[ii].SetActive(false);
			}
		}

		//開始ボタン。
		{
			int t_w = 100;
			int t_h = 30;
			int t_x = 100;
			int t_y = 300;

			Texture2D t_texture = Resources.Load<Texture2D>("button");

			this.start_button = new NUi.Button(this.deleter,null,t_drawpriority,this.CallBack_Click_Start,0);
			this.start_button.SetRect(t_x,t_y,t_w,t_h);
			this.start_button.SetTexture(t_texture);
			this.start_button.SetText("接続");
			this.start_button.SetVisible(false);
		}

		//修了ボタン。
		{
			int t_w = 100;
			int t_h = 30;
			int t_x = 100 + 110;
			int t_y = 300;

			Texture2D t_texture = Resources.Load<Texture2D>("button");

			this.end_button = new NUi.Button(this.deleter,null,t_drawpriority,this.CallBack_Click_End,0);
			this.end_button.SetRect(t_x,t_y,t_w,t_h);
			this.end_button.SetTexture(t_texture);
			this.end_button.SetText("切断");
			this.end_button.SetVisible(false);
		}

		//mode
		this.mode = Mode.Init;

		//inputmode
		this.inputmode = InputMode.Position;
	}

	/** [Button_Base]コールバック。クリック。開始。
	*/
	private void CallBack_Click_Start(int a_id)
	{
		if(this.mode == Mode.Wait){
			this.mode = Mode.Start;
		}
	}

	/** [Button_Base]コールバック。クリック。終了。
	*/
	private void CallBack_Click_End(int a_id)
	{
		if(this.mode == Mode.Do){
			this.mode = Mode.DisConnect;
		}
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ネットワーク。
		NNetwork.Network.GetInstance().Main();

		switch(this.mode){
		case Mode.Init:
			{
				this.status_text.SetText("Mode.Init");
				this.mode = Mode.Wait;
			}break;
		case Mode.Wait:
			{
				this.status_text.SetText("Mode.Wait");

				if(this.IsChangeScene() == true){
					this.mode = Mode.ChangeScene;
				}else{
					this.start_button.SetVisible(true);
				}
			}break;
		case Mode.Start:
			{
				this.status_text.SetText("Mode.Start");

				//開始。
				NNetwork.Network.GetInstance().Start_AutoJoinRandomRoom();

				//ボタン。
				this.start_button.SetVisible(false);
				this.end_button.SetVisible(true);

				this.mode = Mode.Do;
			}break;
		case Mode.Do:
			{
				List<NNetwork.Player> t_list = NNetwork.Network.GetInstance().GetPlayerList();
				NNetwork.Player t_myplayer = NNetwork.Network.GetInstance().GetMyPlayer();

				this.status_text.SetText("Mode.Do : " + t_list.Count.ToString());

				{
					if(NInput.Mouse.GetInstance().right.down == true){
						switch(this.inputmode){
						case InputMode.Position:
							{
								this.inputmode = InputMode.Rotate;
							}break;
						case InputMode.Rotate:
							{
								this.inputmode = InputMode.Scale;
							}break;
						case InputMode.Scale:
							{
								this.inputmode = InputMode.Position;
							}break;
						}
					}

					if(NInput.Mouse.GetInstance().left.down == true){
						switch(this.inputmode){
						case InputMode.Position:
							{
								float t_x = ((float)NInput.Mouse.GetInstance().pos.x - NRender2D.Render2D.VIRTUAL_W / 2) / 100;
								float t_y = ((float)NInput.Mouse.GetInstance().pos.y - NRender2D.Render2D.VIRTUAL_H / 2) / 100;

								if(t_myplayer != null){
									t_myplayer.SetPosition(t_x,t_y,0.0f);
								}
							}break;
						case InputMode.Rotate:
							{
								float t_angle = NInput.Mouse.GetInstance().pos.x;

								Quaternion t_q = Quaternion.AngleAxis(t_angle,new Vector3(0.0f,1.0f,0.0f));

								if(t_myplayer != null){
									t_myplayer.SetQuaternion(ref t_q);
								}
							}break;
						case InputMode.Scale:
							{
								float t_x = 1.0f + (float)NInput.Mouse.GetInstance().pos.x / NRender2D.Render2D.VIRTUAL_W;
								float t_y = 1.0f + (float)NInput.Mouse.GetInstance().pos.y / NRender2D.Render2D.VIRTUAL_H;

								if(t_myplayer != null){
									t_myplayer.SetScale(t_x,t_y,1.0f);
								}
							}break;
						}
					}

					//■送信。
					if(NInput.Key.GetInstance().enter.down == true){
						if(t_myplayer != null){
							//自分赤。
							t_myplayer.RemoteCallInt(999,1);
							t_myplayer.RemoteCallString(777,"red");
						}
					}else if(NInput.Key.GetInstance().escape.down == true){
						if(t_myplayer != null){
							//自分白。
							t_myplayer.RemoteCallInt(999,0);
						}
					}

					if(NInput.Key.GetInstance().sub1.down == true){
						//全部赤。

						List<NNetwork.Player> t_player_list = NNetwork.Network.GetInstance().GetPlayerList();
						for(int ii=0;ii<t_player_list.Count;ii++){
							t_player_list[ii].RemoteCallInt(888,1);
						}
					}else if(NInput.Key.GetInstance().sub2.down == true){
						//全部白。
						List<NNetwork.Player> t_player_list = NNetwork.Network.GetInstance().GetPlayerList();
						for(int ii=0;ii<t_player_list.Count;ii++){
							t_player_list[ii].RemoteCallInt(888,0);
						}
					}
				}

				for(int ii=0;ii<this.player_text.Length;ii++){
					if(ii < t_list.Count){

						string t_text = "";
						t_text += "IsMine = " + t_list[ii].IsMine().ToString() + " ";
						t_text += "IsMasterClient = " + t_list[ii].IsMasterClient().ToString() + " ";
						t_text += "NickName = " + t_list[ii].GetNickName() + " ";
						t_text += "UserID = " + t_list[ii].GetUniqueID().ToString() + "\n";

						t_text += "Pos = " + t_list[ii].GetPosition().ToString() + " ";
						t_text += "Rotate = " + t_list[ii].GetQuaternion().ToString() + " ";
						t_text += "Scale = " + t_list[ii].GetScale().ToString() + " ";

						this.player_text[ii].SetText(t_text);

						this.player_list[ii].SetActive(true);
						this.player_list[ii].transform.position = t_list[ii].GetPosition();
						this.player_list[ii].transform.rotation = t_list[ii].GetQuaternion();
						this.player_list[ii].transform.localScale = t_list[ii].GetScale();
					}else{
						this.player_text[ii].SetText("---");
						this.player_list[ii].SetActive(false);
					}
				}

				if(this.IsChangeScene() == true){
					this.mode = Mode.DisConnect;
				}
			}break;
		case Mode.DisConnect:
			{
				this.status_text.SetText("Mode.DisConnect");

				for(int ii=0;ii<this.player_text.Length;ii++){
					this.player_text[ii].SetText("");
					this.player_list[ii].SetActive(false);
				}

				//切断リクエスト。
				NNetwork.Network.GetInstance().DisconnectRequest();

				//ボタン。
				this.end_button.SetVisible(false);

				this.mode = Mode.DisConnectNow;
			}break;
		case Mode.DisConnectNow:
			{
				this.status_text.SetText("Mode.DisConnectNow");

				if(NNetwork.Network.GetInstance().IsBusy() == false){
					this.mode = Mode.Wait;
				}
			}break;
		case Mode.ChangeScene:
			{
				//シーン変更可能。
			}break;
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		if(this.mode == Mode.ChangeScene){
			return true;
		}
		return false;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

