using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** NFile
*/
namespace NFile
{
	/** ロードローカル。テクスチャーファイル。
	*/
	public class Coroutine_LoadLocalTextureFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			public Texture2D texture;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.texture = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** ＰＮＧのサイズをバイトバイナリから取得する。
		*/
		private static void GetSizeFromPngBinary(byte[] a_png,out int a_width,out int a_height)
		{
			int t_width = 0;
			int t_height = 0;

			if(a_png != null){
				if(a_png.Length > 23){
					t_width += a_png[16] * 256 * 256 * 256;
					t_width += a_png[17] * 256 * 256;
					t_width += a_png[18] * 256;
					t_width += a_png[19];

					t_height += a_png[20] * 256 * 256 * 256;
					t_height += a_png[21] * 256 * 256;
					t_height += a_png[22] * 256;
					t_height += a_png[23];
				}
			}

			a_width = t_width;
			a_height = t_height;
		}

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_full_path)
		{
			//result
			this.result = new ResultType();

			//キャンセルトークン。
			NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

			//タスク起動。
			NTaskW.Task<Task_LoadLocalTextureFile.ResultType> t_task = Task_LoadLocalTextureFile.Run(a_full_path,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_instance.OnCoroutine() == false){
					t_cancel_token.Cancel();
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//プログレス。
			/*
			a_instance.SetResultProgress(0.999f);
			*/

			//結果。
			Task_LoadLocalTextureFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.binary != null){
					int t_width;
					int t_height;
					GetSizeFromPngBinary(t_result.binary,out t_width,out t_height);
					if((t_width > 0)&&(t_height > 0)&&(t_width <= 8192)&&(t_height <= 8192)){
						Texture2D t_result_texture = new Texture2D(t_width,t_height);
						if(t_result_texture != null){
							if(t_result_texture.LoadImage(t_result.binary) == true){
								this.result.texture = t_result_texture;
								yield break;
							}else{
								this.result.errorstring = "LoadImage == false";
								yield break;
							}
						}else{
							this.result.errorstring = "new Texture2D == null";
							yield break;
						}
					}else{
						this.result.errorstring = "width == " + t_width.ToString() + " : height == " + t_height.ToString();
						yield break;
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "null";
				yield break;
			}
		}
	}
}

