

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief データ。データアイテム。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** DataItem
	*/
	public class DataItem
	{
		/** DataType
		*/
		public enum DataType
		{
			/** AssetBundle
			*/
			AssetBundle,

			/** Resources
			*/
			Resources,

			/** StreamingAssets
			*/
			StreamingAssets
		};

		/** datatype
		*/
		public DataType datatype;

		/** path

			AssetBundle		: Assets/***.prefab
			Resources		: Texture/button
			StreamingAssets	: button.png

		*/
		public string path;

		/** packname
		*/
		public string packname;
	}
}

