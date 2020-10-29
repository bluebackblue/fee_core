

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
	/** Fee_Tool
	*/
	#if(UNITY_EDITOR)
	public class Fee_Tool
	{
		/** FindFeePath
		*/
		public static Fee.File.Path FindFeePath()
		{
			return Fee.EditorTool.AssetTool.FindFile(new File.Path(),new File.Path("fee_buildtarget")).CreateFileNameChangePath("",Fee.File.Path.SEPARATOR);
		}
	}
	#endif
}

