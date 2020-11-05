

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief マテリアル。ステータス。
*/


/** Fee.Material
*/
namespace Fee.Material
{
	/** Status
	*/
	public class Status
	{
		/** resource_path
		*/
		public string resource_path;

		/** property_list
		*/
		public string[] property_list;

		/** constructor
		*/
		public Status(string a_resource_path,string[] a_property_list)
		{
			//resource_path
			this.resource_path = a_resource_path;

			//property_list
			this.property_list = a_property_list;
		}
	}
}

