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


/** test03

	ＵＮＩＶＲＭ。

*/
public class test03 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** button
	*/
	private NUi.Button button;

	/** inputfield
	*/
	private NRender2D.InputField2D inputfield;

	/** bg
	*/
	private NRender2D.Sprite2D bg;

	/** window
	*/
	private NRender2D.Sprite2D window;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ダウンロードアイテム。
	*/
	private NDownLoad.Item download_item;
	private NSaveLoad.Item saveload_item;

	/** バイナリ。
	*/
	private byte[] binary;

	/** mycamera
	*/
	private GameObject mycamera_gameobject;
	private Camera mycamera_camera;

	enum LayerIndex
	{
		LayerIndex_Bg = 0,

		LayerIndex_Model = 0,

		LayerIndex_Ui = 1,
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

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.DownLoad.CreateInstance();

		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		{
			//layerindex
			int t_layerindex_ui = (int)LayerIndex.LayerIndex_Ui;

			//drawpriority
			long t_drawpriority_ui = t_layerindex_ui * NRender2D.Render2D.DRAWPRIORITY_STEP;

			//button
			this.button = new NUi.Button(this.deleter,null,t_drawpriority_ui,this.CallBack_Click,0);
			this.button.SetRect(130,10,50,50);
			this.button.SetTexture(Resources.Load<Texture2D>("button"));
			this.button.SetText("Load");

			//inputfield
			this.inputfield = new NRender2D.InputField2D(this.deleter,null,t_drawpriority_ui);
			this.inputfield.SetRect(130 + 50 + 10,10,700,50);
			this.inputfield.SetText("http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx");
			this.inputfield.SetMultiLine(false);

			//ステータス。
			this.status = new NRender2D.Text2D(this.deleter,null,t_drawpriority_ui);
			this.status.SetRect(100,100,0,0);

			//window
			this.window = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority_ui);
			this.window.SetTexture(Texture2D.whiteTexture);
			this.window.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.window.SetRect(300,300,200,200);
			this.window.SetColor(1.0f,0.1f,0.1f,0.5f);
			this.window.SetMaterialType(NRender2D.Config.MaterialType.Alpha);
		}

		//bg
		{
			int t_layerindex_bg = (int)LayerIndex.LayerIndex_Bg;

			long t_drawpriority_bg = t_layerindex_bg * NRender2D.Render2D.DRAWPRIORITY_STEP;

			this.bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority_bg);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
			this.bg.SetColor(0.1f,0.1f,0.1f,1.0f);
		}

		//download
		this.download_item = null;
		this.saveload_item = null;

		//binary
		this.binary = null;

		//カメラ。
		this.mycamera_gameobject = GameObject.Find("Main Camera");
		this.mycamera_camera = this.mycamera_gameobject.GetComponent<Camera>();
		if(this.mycamera_camera != null){
			//クリアしない。
			this.mycamera_camera.clearFlags = CameraClearFlags.Nothing;

			//モデルだけを表示。
			this.mycamera_camera.cullingMask = (1 << LayerMask.NameToLayer("Model"));

			//デプスを２Ｄ描画の合わせる。
			this.mycamera_camera.depth = NRender2D.Render2D.GetInstance().GetCameraAfterDepth((int)LayerIndex.LayerIndex_Model);
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		if((this.saveload_item == null)&&(this.download_item == null)&&(this.binary == null)){
			GameObject t_model = GameObject.Find("Model");
			if(t_model != null){
				GameObject.Destroy(t_model);
			}

			#if(true)
			this.download_item = NDownLoad.DownLoad.GetInstance().Request(this.inputfield.GetText(),NDownLoad.DataType.Binary);
			#else
			this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestLoadStreamingAssetsBinaryFile("nana.vrmx");
			#endif
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance().Main();

		//ＵＮＩＶＲＭ。
		NUniVrm.UniVrm.GetInstance().Main();

		if(this.download_item != null){
			if(this.download_item.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("Download : " + this.download_item.GetResultProgress().ToString());

				//キャンセル。
				if(this.IsChangeScene() == true){
					this.download_item.Cancel();
				}
			}else{
				//ダウンロード完了。
				if(this.download_item.GetResultDataType() == NDownLoad.DataType.Binary){
					this.binary = this.download_item.GetResultBinary();
				}else{
					this.status.SetText("Download : Error");
				}
				this.download_item = null;
			}
		}
		
		if(this.saveload_item != null){
			if(this.saveload_item.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("SaveLoad : " + this.saveload_item.GetResultProgress().ToString());

				//キャンセル。
				if(this.IsChangeScene() == true){
					this.saveload_item.Cancel();
				}
			}else{
				//ダウンロード完了。
				if(this.saveload_item.GetResultDataType() == NSaveLoad.DataType.Binary){
					this.binary = this.saveload_item.GetResultBinary();
				}else{
					this.status.SetText("SaveLoad : Error");
				}
				this.saveload_item = null;
			}
		}

		if(this.binary != null){
			this.status.SetText("Create : size = " + this.binary.Length.ToString());
			NUniVrm.UniVrm.GetInstance().Create(this.binary);
			this.binary = null;

			NUniVrm.UniVrm.GetInstance().SetLayer("Model");
		}

		//カメラを回す。
		if(this.mycamera_gameobject != null){
			float t_time = Time.realtimeSinceStartup / 3;
			Vector3 t_position = new Vector3(Mathf.Sin(t_time) * 2.0f,1.0f,Mathf.Cos(t_time) * 2.0f);
			Transform t_camera_transform = this.mycamera_gameobject.GetComponent<Transform>();
			t_camera_transform.position = t_position;
			t_camera_transform.LookAt(new Vector3(0.0f,1.0f,0.0f));
		}

		//右手。
		Vector3 t_position_hand = Vector3.zero;
		GameObject t_gameobject_hand = GameObject.Find("J_Bip_R_Hand");
		if(t_gameobject_hand != null){
			Transform t_transcorm_hand = t_gameobject_hand.GetComponent<Transform>();
			if(t_transcorm_hand != null){
				t_position_hand = t_transcorm_hand.position;
			}
		}

		//スクリーン座標計算。
		if(this.mycamera_camera != null){
			int t_x;
			int t_y;
			NRender2D.Render2D.GetInstance().WorldToVirtualScreen(this.mycamera_camera,ref t_position_hand,out t_x,out t_y);
			this.window.SetX(t_x - this.window.GetW()/2);
			this.window.SetY(t_y - this.window.GetH()/2);
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

