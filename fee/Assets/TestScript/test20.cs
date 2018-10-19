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

		CreateTerrain,

		LoadVrm_Start,
		LoadVrm_Do,

		CreateVrm_Start,
		CreateVrm_Do,

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

	/** vrm
	*/
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

		//vrm
		this.vrm = null;
		this.vrm_loaditem = null;
		this.vrm_status = VrmStatus.None;
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

				this.step = Step.CreateTerrain;
			}break;
		case Step.CreateTerrain:
			{
				//テレイン作成。
				this.status_text.SetText(this.step.ToString());

				GameObject t_prefab = Resources.Load<GameObject>("Terrain/TerrainPrefab");
				GameObject t_terrain = GameObject.Instantiate<GameObject>(t_prefab);
				this.step = Step.LoadVrm_Start;
			}break;
		case Step.LoadVrm_Start:
			{
				//ＶＲＭダウンロード開始。
				this.status_text.SetText(this.step.ToString());

				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx";
				this.vrm_loaditem = NFile.File.GetInstance().RequestDownLoadBinaryFile(t_url);

				this.step = Step.LoadVrm_Do;
			}break;
		case Step.LoadVrm_Do:
			{
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

						this.step = Step.ToMain;
					}
				}else{
					this.step = Step.ToMain;
				}
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

