

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** FocusGroup
	*/
	public interface Compare_Base<ID>
	{
		/** [Fee.Focus.Compare_Base]比較。
		*/
		bool Compare(ID a_id_a,ID a_id_b);
	}
}

