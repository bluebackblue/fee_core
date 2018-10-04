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

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ダウンロードアイテム。
	*/
	private NDownLoad.Item download_item;

	/** バイナリ。
	*/
	private byte[] binary;

	/** mycamera
	*/
	private GameObject mycamera;

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

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//layerindex
		int t_layerindex = 0;

		//drawpriority
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//button
		this.button = new NUi.Button(this.deleter,null,t_drawpriority,this.CallBack_Click,0);
		this.button.SetRect(130,10,50,50);
		this.button.SetTexture(Resources.Load<Texture2D>("button"));
		this.button.SetText("Load");

		//inputfield
		this.inputfield = new NRender2D.InputField2D(this.deleter,null,t_drawpriority);
		this.inputfield.SetRect(130 + 50 + 10,10,700,50);
		this.inputfield.SetText("http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx");

		//ステータス。
		this.status = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.status.SetRect(100,100,0,0);

		this.download_item = null;
		this.binary = null;

		//カメラ。
		this.mycamera = GameObject.Find("Main Camera");
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		if((this.download_item == null)&&(this.binary == null)){
			GameObject t_model = GameObject.Find("Model");
			if(t_model != null){
				GameObject.Destroy(t_model);
			}

			this.download_item = NDownLoad.DownLoad.GetInstance().Request("http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx",NDownLoad.DataType.Binary);
		}
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		if(this.download_item != null){
			if(this.download_item.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("Download : " + this.download_item.GetResultProgress().ToString());
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
		
		if(this.binary != null){
			this.status.SetText("Create : size = " + this.binary.Length.ToString());
			NUniVrm.UniVrm.GetInstance().Create(this.binary);
			this.binary = null;
		}

		//カメラを回す。
		if(this.mycamera != null){

			float t_time = Time.realtimeSinceStartup;

			Vector3 t_position = new Vector3(Mathf.Sin(t_time) * 2.0f,1.0f,Mathf.Cos(t_time) * 2.0f);

			Transform t_camera_transform = this.mycamera.GetComponent<Transform>();

			t_camera_transform.position = t_position;
			t_camera_transform.LookAt(new Vector3(0.0f,1.0f,0.0f));
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

