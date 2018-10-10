using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。ダミー。
*/


#if(USE_UNIVRM)
#else

/** NUniVrm
*/
namespace NUniVrm
{
	/** VRM
	*/
	namespace VRM
	{
		/** VRMImporterContext
		*/
		public class VRMImporterContext
		{
			public void Destroy(bool a_flag){}
		}
	}
}

#endif

