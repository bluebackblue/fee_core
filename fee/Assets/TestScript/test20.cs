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


/** test20
*/
public class test20 : main_base
{
	/** Step
	*/
	private enum Step
	{
		Init,

		Login_Start,
		Login_WaitButton,
		Login_Connect_Start,
		Login_Connect_Do,
		Login_Connect_End,

		DwonLoadVrm_Start,
		DwonLoadVrm_Do,

		CreateVrm_Start,
		CreateVrm_Do,

		CreateTerrain,

		ToMain,

		Main,
	};

	/** VrmStatus
	*/
	private enum VrmStatus
	{
		None,

		Idel,

		Walk,
	};

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** step
	*/
	private Step step;

	/** status_text
	*/
	private NRender2D.Text2D status_text;

	/** login
	*/
	private NUi.Button login_button;
	private NUi.Button login_apikey_button;
	private NRender2D.InputField2D login_apikey_inputfield;
	private NRender2D.InputField2D login_pass_inputfield;
	private NRender2D.Text2D login_apikey_text;
	private NRender2D.Text2D login_pass_text;
	private NFile.Item login_api_vrm_get;
	private NRender2D.Text2D login_last_error;

	/** vrm
	*/
	private string vrm_url;
	private NUniVrm.Item vrm;
	private NFile.Item vrm_loaditem;
	private GameObject vrm_camera;
	private VrmStatus vrm_status;

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

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//パッド。インスタンス作成。
		NInput.Pad.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		NFile.File.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//step
		this.step = Step.Init;

		//status_text
		this.status_text = new NRender2D.Text2D(this.deleter,null,0);
		this.status_text.SetRect(100,10,0,0);
		this.status_text.SetText("");

		//login
		{
			int t_button_w = 100;
			int t_button_h = 50;
			int t_button_x = 200;//(NRender2D.Render2D.VIRTUAL_W - t_button_w) / 2;
			int t_button_y = (NRender2D.Render2D.VIRTUAL_H - t_button_h) / 2;

			int t_pass_x = t_button_x;
			int t_pass_y = t_button_y - 70;
			int t_pass_h = 50;
			int t_apikey_x = t_button_x;
			int t_apikey_y = t_button_y - 140;
			int t_apikey_h = 50;

			this.login_button = new NUi.Button(this.deleter,null,0,this.CallBack_Click_Login,0);
			this.login_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.login_button.SetRect(t_button_x,t_button_y,t_button_w,t_button_h);
			this.login_button.SetText("ログイン");
			this.login_button.SetVisible(false);

			this.login_pass_inputfield = new NRender2D.InputField2D(this.deleter,null,0);
			this.login_pass_inputfield.SetRect(t_pass_x,t_pass_y,300,t_pass_h);
			this.login_pass_inputfield.SetVisible(false);

			this.login_apikey_inputfield = new NRender2D.InputField2D(this.deleter,null,0);
			this.login_apikey_inputfield.SetRect(t_apikey_x,t_apikey_y,400,t_apikey_h);
			this.login_apikey_inputfield.SetVisible(false);

			this.login_pass_text = new NRender2D.Text2D(this.deleter,null,0);
			this.login_pass_text.SetRect(t_pass_x - 100,t_pass_y,0,t_pass_h);
			this.login_pass_text.SetText("PASS");
			this.login_pass_text.SetCenter(false,true);
			this.login_pass_text.SetVisible(false);

			this.login_apikey_text = new NRender2D.Text2D(this.deleter,null,0);
			this.login_apikey_text.SetRect(t_apikey_x - 100,t_apikey_y,0,t_apikey_h);
			this.login_apikey_text.SetText("APIKEY");
			this.login_apikey_text.SetCenter(false,true);
			this.login_apikey_text.SetVisible(false);

			this.login_apikey_button = new NUi.Button(this.deleter,null,0,this.CallBack_Click_ApiKeyPage,0);
			this.login_apikey_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.login_apikey_button.SetRect(t_apikey_x + 450,t_apikey_y,t_button_w + 140,t_button_h);
			this.login_apikey_button.SetText("ApiKey作成ページを開く");
			this.login_apikey_button.SetVisible(false);

			this.login_last_error = new NRender2D.Text2D(this.deleter,null,0);
			this.login_last_error.SetRect(t_button_x + 130,t_button_y,0,t_pass_h);
			this.login_last_error.SetText("---");
			this.login_last_error.SetCenter(false,true);
			this.login_last_error.SetVisible(false);

			this.login_api_vrm_get = null;
		}

		//vrm
		this.vrm_url = null;
		this.vrm = null;
		this.vrm_loaditem = null;
		this.vrm_status = VrmStatus.None;
	}

	/** [Button_Base]コールバック。クリック。
	*/
	public void CallBack_Click_Login(int a_id)
	{
		if(this.step == Step.Login_WaitButton){
			this.step = Step.Login_Connect_Start;
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	public void CallBack_Click_ApiKeyPage(int a_id)
	{
		Application.OpenURL("https://bbbproject.sakura.ne.jp/www/project_discord/");
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//パッド。
		NInput.Pad.GetInstance().Main();

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ファイル。
		NFile.File.GetInstance().Main();

		//ＵＮＩＶＲＭ。
		NUniVrm.UniVrm.GetInstance().Main();

		switch(this.step){
		case Step.Init:
			{
				this.status_text.SetText(this.step.ToString());

				this.step = Step.Login_Start;
			}break;
		case Step.Login_Start:
			{
				//ログイン。開始。
				this.status_text.SetText(this.step.ToString());

				{
					this.login_button.SetVisible(true);
					this.login_pass_inputfield.SetVisible(true);
					this.login_pass_inputfield.SetText("");
					this.login_apikey_inputfield.SetVisible(true);
					this.login_apikey_inputfield.SetText("");
					this.login_pass_text.SetVisible(true);
					this.login_apikey_text.SetVisible(true);
					this.login_apikey_button.SetVisible(true);
					this.login_last_error.SetVisible(true);
				}

				this.step = Step.Login_WaitButton;
			}break;
		case Step.Login_WaitButton:
			{
				//ログイン。ボタン待ち。
				this.status_text.SetText(this.step.ToString());
			}break;
		case Step.Login_Connect_Start:
			{
				//ログイン。ＶＲＭファイルのＵＲＬ取得。開始。
				this.status_text.SetText(this.step.ToString());

				WWWForm t_post_data = new WWWForm();
				t_post_data.AddField("apikey_pass",this.login_pass_inputfield.GetText());
				t_post_data.AddField("apikey_token",this.login_apikey_inputfield.GetText());
				this.login_api_vrm_get = NFile.File.GetInstance().RequestDownLoadTextFile("https://bbbproject.sakura.ne.jp/www/project_discord/api/vrm/get/",t_post_data,NFile.ProgressMode.DownLoad);

				this.step = Step.Login_Connect_Do;
			}break;
		case Step.Login_Connect_Do:
			{
				//ログイン。ＶＲＭファイルのＵＲＬ取得。処理中。

				if(this.login_api_vrm_get != null){
					if(this.login_api_vrm_get.IsBusy() == true){
						//ダウンロード中。
						this.status_text.SetText(this.step.ToString() + " " + this.login_api_vrm_get.GetResultProgress().ToString());

						//キャンセル。
						if(this.IsChangeScene() == true){
							this.login_api_vrm_get.Cancel();
						}
					}else{
						if(this.login_api_vrm_get.GetResultType() == NFile.Item.ResultType.Text){
							//ダウンロード成功。
						}else{
							//ダウンロード失敗。
							this.login_api_vrm_get = null;
						}

						this.step = Step.Login_Connect_End;
					}
				}else{
					this.step = Step.Login_Connect_End;
				}
			}break;
		case Step.Login_Connect_End:
			{
				this.vrm_url = null;

				if(this.login_api_vrm_get != null){

					NJsonItem.JsonItem t_json = new NJsonItem.JsonItem(this.login_api_vrm_get.GetResultText());
					if(t_json != null){
						if(t_json.IsAssociativeArray() == true){
							if(t_json.IsExistItem("vrm_url", NJsonItem.ValueType.StringData) == true){
								this.vrm_url = t_json.GetItem("vrm_url").GetStringData();
							}
						}
					}

					{
						this.login_button.SetVisible(false);
						this.login_pass_inputfield.SetVisible(false);
						this.login_pass_inputfield.SetText("");
						this.login_apikey_inputfield.SetVisible(false);
						this.login_apikey_inputfield.SetText("");
						this.login_pass_text.SetVisible(false);
						this.login_apikey_text.SetVisible(false);
						this.login_apikey_button.SetVisible(false);
						this.login_last_error.SetVisible(false);
					}

					if(this.vrm_url != null){
						this.status_text.SetText(this.vrm_url);

						this.step = Step.DwonLoadVrm_Start;
					}else{

						string t_error = "error";
						if(t_json != null){
							if(t_json.IsAssociativeArray() == true){
								if(t_json.IsExistItem("error", NJsonItem.ValueType.StringData) == true){
									t_error = t_json.GetItem("error").GetStringData();
								}
							}
						}

						this.login_last_error.SetText(t_error);

						this.status_text.SetText(t_error);

						this.step = Step.Login_Start;
					}
				}
			}break;
		case Step.DwonLoadVrm_Start:
			{
				//ダウンロードＶＲＭ。開始。

				string t_vrm_url = null;

				if(this.login_api_vrm_get != null){

					NJsonItem.JsonItem t_json = new NJsonItem.JsonItem(this.login_api_vrm_get.GetResultText());
					if(t_json != null){
						if(t_json.IsAssociativeArray() == true){
							if(t_json.IsExistItem("vrm_url", NJsonItem.ValueType.StringData) == true){
								t_vrm_url = t_json.GetItem("vrm_url").GetStringData();
							}
						}
					}

					if(t_vrm_url != null){
						this.status_text.SetText(t_vrm_url);
					}else{

						string t_error = "error";

						if(t_json != null){
							if(t_json.IsAssociativeArray() == true){
								if(t_json.IsExistItem("error", NJsonItem.ValueType.StringData) == true){
									t_error = t_json.GetItem("error").GetStringData();
								}
							}
						}

						this.status_text.SetText(t_error);
					}
				}

				if(t_vrm_url != null){
					this.vrm_loaditem = NFile.File.GetInstance().RequestDownLoadBinaryFile(t_vrm_url,null,NFile.ProgressMode.DownLoad);
				}else{
					this.vrm_loaditem = null;
				}

				this.step = Step.DwonLoadVrm_Do;
			}break;
		case Step.DwonLoadVrm_Do:
			{
				//ダウンロードＶＲＭ。処理中。

				if(this.vrm_loaditem != null){
					if(this.vrm_loaditem.IsBusy() == true){
						//ダウンロード中。
						this.status_text.SetText(this.step.ToString() + " " + this.vrm_loaditem.GetResultProgress().ToString());

						//キャンセル。
						if(this.IsChangeScene() == true){
							this.vrm_loaditem.Cancel();
						}
					}else{
						if(this.vrm_loaditem.GetResultType() == NFile.Item.ResultType.Binary){
							//ダウンロード成功。
						}else{
							//ダウンロード失敗。
							this.vrm_loaditem = null;
						}

						this.step = Step.CreateVrm_Start;
					}
				}else{
					this.step = Step.CreateVrm_Start;
				}
			}break;
		case Step.CreateVrm_Start:
			{
				//ＶＲＭ作成開始。
				this.status_text.SetText(this.step.ToString());

				byte[] t_binary = null;
				if(this.vrm_loaditem != null){
					t_binary = this.vrm_loaditem.GetResultBinary();
				}

				if(t_binary != null){
					this.vrm = NUniVrm.UniVrm.GetInstance().Request(t_binary);
				}

				this.vrm_loaditem = null;

				this.step = Step.CreateVrm_Do;
			}break;
		case Step.CreateVrm_Do:
			{
				if(this.vrm != null){
					if(this.vrm.IsBusy() == true){
						//ＶＲＭ作成中。
						this.status_text.SetText(this.step.ToString() + " " + this.vrm.GetResultProgress().ToString());

					}else{
						if(this.vrm.GetResultType() == NUniVrm.Item.ResultType.Context){
							//ＶＲＭ作成成功。

							//レイヤーをモデルに設定。
							this.vrm.SetLayer("Model");

							//表示開始。
							this.vrm.SetRendererEnable(true);

							//アニメータコントローラ設定。
							this.vrm.SetAnimatorController(Resources.Load<RuntimeAnimatorController>("Anime/AnimatorController"));

							//モーション停止。
							this.vrm.SetAnimeEnable(false);
							this.vrm_status = VrmStatus.None;

							//追従カメラ。
							this.vrm_camera = GameObject.Find("Main Camera");
						}else{
							//ＶＲＭ作成失敗。
							this.vrm = null;
						}

						this.step = Step.CreateTerrain;
					}
				}else{
					this.step = Step.CreateTerrain;
				}
			}break;
		case Step.CreateTerrain:
			{
				//テレイン作成。
				this.status_text.SetText(this.step.ToString());

				GameObject t_prefab = Resources.Load<GameObject>("Terrain/TerrainPrefab");
				GameObject t_terrain = GameObject.Instantiate<GameObject>(t_prefab);
				this.step = Step.ToMain;
			}break;
		case Step.ToMain:
			{
				this.status_text.SetText("");

				this.step = Step.Main;
			}break;
		case Step.Main:
			{
				if(this.vrm != null){

					VrmStatus t_request = VrmStatus.None;

					if(NInput.Key.GetInstance().up.on == true){
						//前進。
						t_request = VrmStatus.Walk;

						//前方向。
						Vector3 t_vrm_forward = this.vrm.GetForward();

						//移動。
						float t_speed_move = 0.02f;
						Vector3 t_position = this.vrm.GetPosition() + t_vrm_forward * t_speed_move;
						this.vrm.SetPosition(ref t_position);
					}

					if(NInput.Key.GetInstance().left.on == true){
						//左回転。
						t_request = VrmStatus.Walk;

						//前方向。
						Vector3 t_vrm_forward = this.vrm.GetForward();

						float t_speed_rotate = 0.3f;
						Transform t_vrm_transform = this.vrm.GetTransform();
						t_vrm_transform.rotation = Quaternion.AngleAxis(-t_speed_rotate,Vector3.up) * t_vrm_transform.rotation;

						//移動。
						float t_speed_move = 0.005f;
						Vector3 t_position = this.vrm.GetPosition() + t_vrm_forward * t_speed_move;
						this.vrm.SetPosition(ref t_position);
					}else if(NInput.Key.GetInstance().right.on == true){
						//右回転。
						t_request = VrmStatus.Walk;

						//前方向。
						Vector3 t_vrm_forward = this.vrm.GetForward();

						float t_speed_rotate = 0.3f;
						Transform t_vrm_transform = this.vrm.GetTransform();
						t_vrm_transform.rotation = Quaternion.AngleAxis(t_speed_rotate,Vector3.up) * t_vrm_transform.rotation;

						//移動。
						float t_speed_move = 0.005f;
						Vector3 t_position = this.vrm.GetPosition() + t_vrm_forward * t_speed_move;
						this.vrm.SetPosition(ref t_position);
					}

					if(this.vrm_status != t_request){
						if(this.vrm_status == VrmStatus.None){
							this.vrm.SetAnimeEnable(true);
						}else if(t_request == VrmStatus.None){
							this.vrm.SetAnimeEnable(false);
						}

						switch(t_request){
						case VrmStatus.Walk:
							{
								this.vrm_status = VrmStatus.Walk;
								this.vrm.SetAnime(Animator.StringToHash("Base Layer.standing_walk_forward_inPlace"));
							}break;
						default:
							{
								this.vrm_status = t_request;
							}break;
						}
					}

					//カメラ更新。
					this.UpdateCamera();
				}
			}break;
		}
	}

	/** カメラ更新。
	*/
	public void UpdateCamera()
	{
		if(this.vrm != null){
			//前方向。
			Vector3 t_vrm_forward = this.vrm.GetForward();

			//位置。
			Transform t_vrm_transform = this.vrm.GetTransform();

			//カメラ位置。
			Vector3 t_to_camerapos = t_vrm_transform.position - t_vrm_forward * 3 + Vector3.up * 1.5f;

			//注視点。
			Vector3 t_to_lookat = t_vrm_transform.position + Vector3.up * 0.5f + t_vrm_forward * 1.1f;

			//注視点補間。
			{
				float t_speed = 0.05f;
				Vector3 t_dir = (t_to_lookat - this.vrm_camera.transform.position).normalized;
				Quaternion t_quaternion = Quaternion.LookRotation(t_dir,Vector3.up);
				this.vrm_camera.transform.rotation = Quaternion.Lerp(this.vrm_camera.transform.rotation,t_quaternion,t_speed);
			}

			//位置補間。
			this.vrm_camera.transform.position = Vector3.Lerp(t_to_camerapos,this.vrm_camera.transform.position,0.01f);
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

