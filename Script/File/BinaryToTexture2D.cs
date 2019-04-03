

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。テクスチャー作成。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** BinaryToTexture2D
	*/
	public class BinaryToTexture2D
	{
		/** BinaryType
		*/
		private enum BinaryType
		{
			/** None
			*/
			None,

			/** Png
			*/
			Png,

			/** Jpg
			*/
			Jpg,
		}

		/** GetType
		*/
		private static BinaryType GetType(byte[] a_binary)
		{
			if(a_binary != null){
				if(a_binary.Length > 8){
					if(
						(a_binary[0] == 0x89) &&
						(a_binary[1] == 0x50) &&
						(a_binary[2] == 0x4E) &&
						(a_binary[3] == 0x47) &&
						(a_binary[4] == 0x0D) &&
						(a_binary[5] == 0x0A) &&
						(a_binary[6] == 0x1A) &&
						(a_binary[7] == 0x0A)
					){
						return BinaryType.Png;
					}
				}
				if(a_binary.Length > 2){
					if(
						(a_binary[0] == 0xFF) &&
						(a_binary[1] == 0xD8)
					){
						return BinaryType.Jpg;
					}
				}
			}

			return BinaryType.None;
		}

		/** Png_GetSize
		*/
		private static bool Png_GetSize(byte[] a_binary,out int a_width,out int a_height)
		{
			int t_width = 0;
			int t_height = 0;

			if(a_binary != null){
				if(a_binary.Length > 23){
					t_width += a_binary[16] * 256 * 256 * 256;
					t_width += a_binary[17] * 256 * 256;
					t_width += a_binary[18] * 256;
					t_width += a_binary[19];

					t_height += a_binary[20] * 256 * 256 * 256;
					t_height += a_binary[21] * 256 * 256;
					t_height += a_binary[22] * 256;
					t_height += a_binary[23];

					a_width = t_width;
					a_height = t_height;
					return true;
				}
			}

			a_width = -1;
			a_height = -1;
			return false;
		}

		/** Jpg_GetSize
		*/
		private static bool Jpg_GetSize(byte[] a_binary,out int a_width,out int a_height)
		{
			int t_index = 2;

			while(t_index >= 0){
				if((t_index + 3) >= a_binary.Length){
					break;
				}

				if(a_binary[t_index] == 0xFF){
					int t_marker_size = a_binary[t_index + 2] * 256 + a_binary[t_index + 3];
					if((t_index + t_marker_size + 2) >= a_binary.Length){
						break;
					}

					switch(a_binary[t_index + 1]){
					case 0xE0:
						{
							//APP アプリケーション用。
						}break;
					case 0xFE:
						{
							//COM コメント。
						}break;
					case 0xC4://DHT  ハフマンテーブル定義。
						{
						}break;
					case 0xC0://SOF0 ハフマン符号化基本DCT方式。
					case 0xC1://SOF1 ハフマン符号化拡張シーケンシャルDCT方式。
					case 0xC2://SOF2 ハフマン符号化プログレッシブDCT方式。
					case 0xC3://SOF3 ハフマン符号化ロスレス方式。
					case 0xC5://SOF5 ハフマン符号化差分シーケンシャルDCT。
					case 0xC6://SOF6 ハフマン符号化差分プログレッシブDCT。
					case 0xC7://SOF7 ハフマン符号化差分ロスレス方式。
					case 0xC8://JPG  JPEG拡張用リザーブ。
					case 0xC9://SOF9 算術符号化拡張シーケンシャルDCT方式。
					case 0xCA://SOFA 算術符号化プログレッシブDCT方式。
					case 0xCB://SOFB 算術符号化ロスレス方式。
					case 0xCC://DAC  算術符号テーブル定義。
					case 0xCD://SOFD 算術符号化差分シーケンシャルDCT。
					case 0xCE://SOFE 算術符号化差分プログレッシブDCT。
					case 0xCF://SOFF 算術符号化差分ロスレス方式。
						{
							if(t_index + 8 <= a_binary.Length){
								a_width = a_binary[t_index + 5] * 256 + a_binary[t_index + 6];
								a_height = a_binary[t_index + 7] * 256 + a_binary[t_index + 8];
								return true;
							}
						}break;
					case 0xDA:
						{
							//SOS。

							//この後ろは画像データ。
							t_marker_size = -1;
						}break;
					}

					//次。
					if(t_marker_size >= 0){
						t_index += t_marker_size + 2;
					}else{
						t_index = -1;
					}
				}
			}

			a_width = 0;
			a_height = 0;
			return false;
		}

		/** Convert
		*/
		public static UnityEngine.Texture2D Convert(byte[] a_binary)
		{
			BinaryType t_type = GetType(a_binary);

			switch(t_type){
			case BinaryType.Png:
				{
					int t_width;
					int t_height;
					if(Png_GetSize(a_binary,out t_width,out t_height) == true){
						UnityEngine.Texture2D t_texture = new UnityEngine.Texture2D(t_width,t_height);
						if(t_texture != null){
							if(UnityEngine.ImageConversion.LoadImage(t_texture,a_binary) == true){
								return t_texture;
							}
						}
					}
				}break;
			case BinaryType.Jpg:
				{
					int t_width;
					int t_height;
					if(Jpg_GetSize(a_binary,out t_width,out t_height) == true){
						UnityEngine.Texture2D t_texture = new UnityEngine.Texture2D(t_width,t_height);
						if(t_texture != null){
							if(UnityEngine.ImageConversion.LoadImage(t_texture,a_binary) == true){
								return t_texture;
							}
						}
					}
				}break;
			}

			return null;
		}
	}
}

