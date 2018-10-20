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


/** test07
*/
public class test07 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** button
	*/
	private NUi.Button button;

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
		//NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//暗号。インスタンス作成。
		NCrypt.Config.LOG_ENABLE = true;
		NCrypt.Crypt.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//button
		this.button = new NUi.Button(this.deleter,null,0,this.CallBack_Click,-1);
		this.button.SetTexture(Resources.Load<Texture2D>("button"));
		this.button.SetRect(100,100,50,50);
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		//鍵作成。
		string t_public_key;
		string t_private_key;
		NCrypt.Crypt.GetInstance().CreateNewKey(out t_public_key,out t_private_key);

		//プレーン。
		byte[] t_plane_binary = new byte[100];
		for(int ii=0;ii<t_plane_binary.Length;ii++){
			t_plane_binary[ii] = (byte)(ii % 256);
		}

		byte[] t_encrypt_bianry = NCrypt.Crypt.GetInstance().EncryptPublicKey(t_public_key,t_plane_binary);
		if(t_encrypt_bianry != null){
			string t_log = "";
			for(int ii=0;ii<10;ii++){
				t_log += t_encrypt_bianry[ii].ToString() + " ";
			}
			Debug.Log(t_encrypt_bianry.Length.ToString());
			Debug.Log(t_log);
		}

		if(t_encrypt_bianry != null){
			byte[] t_decrypt_binary = NCrypt.Crypt.GetInstance().DecryptPrivateKey(t_private_key,t_encrypt_bianry);
			{
				string t_log = "";
				for(int ii=0;ii<10;ii++){
					t_log += t_decrypt_binary[ii].ToString() + " ";
				}
				Debug.Log(t_decrypt_binary.Length.ToString());
				Debug.Log(t_log);
			}
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

