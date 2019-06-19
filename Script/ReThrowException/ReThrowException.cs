

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief リスロー。
*/


/** Fee.Audio
*/
namespace Fee.ReThrowException
{
	/** ReThrowException
	*/
	[System.Serializable()]
	class ReThrowException : System.Exception
	{
		/** constructor
		*/
		public ReThrowException()
			: base()
		{
		}

		/** constructor
		*/
		public ReThrowException(string a_message)
			: base(a_message)
		{
		}

		/** constructor
		*/
		public ReThrowException(string a_message,System.Exception a_inner_exception)
			: base(a_message,a_inner_exception)
		{
		}

		/** constructor
		*/
		protected ReThrowException(System.Runtime.Serialization.SerializationInfo a_info,System.Runtime.Serialization.StreamingContext a_context)
			: base(a_info,a_context)
		{
		}
	}
}

