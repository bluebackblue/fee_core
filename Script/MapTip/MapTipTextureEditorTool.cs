

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief マップチップ。
*/


/** Fee.MapTip
*/
namespace Fee.MapTip
{
	/** MapTipTextureEditorTool
	*/
	#if(UNITY_EDITOR)
	public class MapTipTextureEditorTool
	{
		/** マップチップテクスチャーを分割。

			a_use_count == true : 重複する場合に名前を最後にカウントを付ける。

		*/
		public static void Parse(UnityEngine.Texture2D a_texture,int a_tip_size,Fee.File.Path a_assets_path,bool a_use_count)
		{
			if(a_texture != null){
				if(a_tip_size > 0){

					Tool.Assert((a_texture.width % a_tip_size) == 0);
					Tool.Assert((a_texture.height % a_tip_size) == 0);

					int t_xx_max = a_texture.width / a_tip_size;
					int t_yy_max = a_texture.height / a_tip_size;

					for(int yy=0;yy<t_yy_max;yy++){
						for(int xx=0;xx<t_xx_max;xx++){

							//テクスチャー作成。
							UnityEngine.Texture2D t_new_texture = Fee.EditorTool.Utility.CreateTextureFromTexture(a_texture,xx * a_tip_size,yy * a_tip_size,a_tip_size,a_tip_size);
										
							//ＰＮＧコンバート。
							byte[] t_binary = UnityEngine.ImageConversion.EncodeToPNG(t_new_texture);

							//ＭＤ５.
							string t_md5 = Fee.MD5.MD5.CalcMD5(t_binary);

							if(a_use_count == true){
								//パス。
								Fee.File.Path t_path = null;
								{
									int t_count = 0;
									while(true){
										if(t_count <= 0){
											t_path = new File.Path(a_assets_path,t_md5 + ".png",Fee.File.Path.SEPARATOR);
										}else{
											t_path = new File.Path(a_assets_path,t_md5 + "_" + t_count.ToString() + ".png",Fee.File.Path.SEPARATOR);
										}

										//ファイル出力。
										if(Fee.EditorTool.Utility.IsExistFile(t_path) == false){
											Fee.EditorTool.Utility.WriteBinaryFile(t_path,t_binary,false);
											break;
										}

										t_count++;
									}
								}
							}else{
								//パス。
								Fee.File.Path t_path = new Fee.File.Path(a_assets_path,t_md5 + ".png",Fee.File.Path.SEPARATOR);

								//ファイル出力。
								if(Fee.EditorTool.Utility.IsExistFile(t_path) == false){
									Fee.EditorTool.Utility.WriteBinaryFile(t_path,t_binary,false);
								}
							}
						}
					}
				}else{
					Tool.Assert(false);
				}
			}else{
				Tool.Assert(false);
			}
		}
	}
	#endif
}

