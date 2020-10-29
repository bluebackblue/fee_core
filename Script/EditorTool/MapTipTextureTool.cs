

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
namespace Fee.EditorTool
{
	/** MapTipTextureTool
	*/
	#if(UNITY_EDITOR)
	public class MapTipTextureTool
	{
		/** １枚のマップチップテクスチャーを分割する。

			a_use_count == true : 重複する場合に名前を最後にカウントを付ける。

		*/
		public static void Parse(UnityEngine.Texture2D a_texture,int a_tip_pixsize,Fee.File.Path a_assets_path,bool a_use_count)
		{
			if(a_texture != null){
				if(a_tip_pixsize > 0){

					if(((a_texture.width % a_tip_pixsize) != 0)||((a_texture.height % a_tip_pixsize) != 0)){
						Tool.EditorLogError("Size Error");
					}
					
					int t_xx_max = a_texture.width / a_tip_pixsize;
					int t_yy_max = a_texture.height / a_tip_pixsize;

					for(int yy=0;yy<t_yy_max;yy++){
						for(int xx=0;xx<t_xx_max;xx++){

							//テクスチャー作成。
							UnityEngine.Texture2D t_new_texture = Fee.EditorTool.AssetTool.CreateTextureFromTexture(a_texture,xx * a_tip_pixsize,yy * a_tip_pixsize,a_tip_pixsize,a_tip_pixsize);
										
							//ＰＮＧコンバート。
							byte[] t_binary = UnityEngine.ImageConversion.EncodeToPNG(t_new_texture);

							//ＭＤ５.
							string t_md5 = Fee.MD5.MD5.CalcMD5(t_binary,0,t_binary.Length);

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
										if(Fee.EditorTool.AssetTool.IsExistFile(t_path) == false){
											Fee.EditorTool.AssetTool.WriteBinaryFile(t_path,t_binary);
											break;
										}

										t_count++;
									}
								}
							}else{
								//パス。
								Fee.File.Path t_path = new Fee.File.Path(a_assets_path,t_md5 + ".png",Fee.File.Path.SEPARATOR);

								//ファイル出力。
								if(Fee.EditorTool.AssetTool.IsExistFile(t_path) == false){
									Fee.EditorTool.AssetTool.WriteBinaryFile(t_path,t_binary);
								}
							}
						}
					}
				}else{
					Tool.EditorLogError("Error");
				}
			}else{
				Tool.EditorLogError("Error");
			}
		}
	}
	#endif
}

