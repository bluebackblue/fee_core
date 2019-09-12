

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルデータ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Material_Data
	*/
	public class Material_Data
	{
		/** resource_path
		*/
		public string resource_path;

		/** property
		*/
		public string[] property;

		/** constructor
		*/
		public Material_Data(string a_resource_path,string[] a_property)
		{
			//resource_path
			this.resource_path = a_resource_path;

			//property
			this.property = a_property;
		}
	}
}

