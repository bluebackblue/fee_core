using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief メイン。
*/


/** main
*/
public class main : MonoBehaviour
{
	/** scene_list
	*/
	private string[] scene_list;

	/** シーン数。
	*/
	private readonly int SCENE_COUNT = 20;

	/** Start
	*/
	void Start()
	{
		//シーン列挙。
		this.scene_list = new string[SCENE_COUNT];
		for(int ii=0;ii<scene_list.Length;ii++){
			this.scene_list[ii] = "test" + string.Format("{0:D2}",ii+1);
		}

		//ライブラリ停止。
		this.DeleteLibInstance();
	}

	/** //ライブラリ停止。
	*/
	public void DeleteLibInstance()
	{
		//オーディオ。
		NAudio.Audio.DeleteInstance();

		//ブラー。
		NBlur.Blur.DeleteInstance();

		//ダウンロード。
		NDownLoad.DownLoad.DeleteInstance();

		//イベントプレート。
		NEventPlate.EventPlate.DeleteInstance();

		//フェード。
		NFade.Fade.DeleteInstance();

		//マスウ。
		NInput.Mouse.DeleteInstance();

		//キー。
		NInput.Key.DeleteInstance();

		//ジョイスティック。
		NInput.Joy.DeleteInstance();

		//２Ｄ描画。インスタンス削除。
		NRender2D.Render2D.DeleteInstance();

		//セーブロード。
		NSaveLoad.SaveLoad.DeleteInstance();

		//シーン。
		NScene.Scene.DeleteInstance();

		//ネットワーク。
		NNetwork.Network.DeleteInstance();
	}

	/** デバッグ表示。
	*/
    void OnGUI()
    {
		//ii_max
		int ii_max = 50;

		for(int ii=0;ii<ii_max;ii++){
			int t_xx_max = Screen.width / 100;
			int t_xx = ii % t_xx_max;
			int t_yy = ii / t_xx_max;

			string t_name = null;
			
			if(ii < this.scene_list.Length){
				t_name = this.scene_list[ii];
			}

			int t_x = 30 + t_xx * 100;
			int t_y = 30 + t_yy * 60;
			int t_w = 80;
			int t_h = 40;

			string t_button_string = t_name;
			if(t_button_string == null){
				t_button_string = "-";
			}
			
			if(GUI.Button(new Rect(t_x,t_y,t_w,t_h),t_button_string) == true){
				if(t_name != null){
					UnityEngine.SceneManagement.SceneManager.LoadScene(t_name);
				}
			}
		}
	}

	/** シーン名。
	*/
	public void CallFromHTML(string a_scene_name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(a_scene_name);
	}
}

/** main_base
*/
public class main_base : MonoBehaviour
{
	/** is_changescene
	*/
	public bool is_changescene = false;

	/** デバッグ描画。
	*/
	public void OnGUI()
    {
		if(this.is_changescene == false){
			int t_x = 30;
			int t_y = 30;
			int t_w = 80;
			int t_h = 40;

			if(GUI.Button(new Rect(t_x,t_y,t_w,t_h),"return") == true){
				this.is_changescene = true;
				StartCoroutine(ChangeScene());
			}
		}
	}

	/** 削除前。
	*/
	public virtual bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** シーン切り替え。チェック。
	*/
	public bool IsChangeScene()
	{
		return this.is_changescene;
	}

	/** シーン切り替え。
	*/
	public IEnumerator ChangeScene()
	{
		bool t_first = true;

		while(this.PreDestroy(t_first) == false){
			t_first = false;
			yield return null;
		}

		bool t_ok = false;
		while(t_ok == false){
			t_ok = true;

			if(NDownLoad.DownLoad.GetInstance() != null){
				if(NDownLoad.DownLoad.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}
		
			if(NNetwork.Network.GetInstance() != null){
				if(NNetwork.Network.GetInstance().IsBusy() == true){
					t_ok = false;
				}
			}

			if(t_ok == false){
				yield return null;
			}
		}

		GameObject.Destroy(this.gameObject);

		UnityEngine.SceneManagement.SceneManager.LoadScene("main");

		yield break;
	}
}

