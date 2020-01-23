

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。リフレクションツール。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ConvertTool
	*/
	public class ConvertTool
	{
		/** メンバーリスト。取得。

			継承元を含む。

		*/
		public static void GetMemberListAll(System.Type a_type,System.Collections.Generic.List<System.Reflection.FieldInfo> a_out_list)
		{
			try{
				System.Type t_type = a_type;
				while(true){

					//終端チェック。
					if(t_type == null){
						break;
					}else if(t_type == typeof(System.Object)){
						break;
					}

					//メンバーリスト。
					System.Reflection.MemberInfo[] t_memberinfo_list = Fee.ReflectionTool.Utility.GetMemberList(t_type);
					foreach(System.Reflection.MemberInfo t_memberinfo in t_memberinfo_list){
						if(t_memberinfo.MemberType == System.Reflection.MemberTypes.Field){
							System.Reflection.FieldInfo t_fieldinfo = t_memberinfo as System.Reflection.FieldInfo;
							if(t_fieldinfo != null){

								//オブジェクト化しない。
								if(t_fieldinfo.IsDefined(typeof(Fee.JsonItem.Ignore),false) == true){
									continue;
								}

								//オブジェクト化しない。
								System.Type t_field_type = t_fieldinfo.FieldType;
								if((t_field_type == typeof(System.IntPtr))||(t_field_type == typeof(System.UIntPtr))){
									continue;
								}

								a_out_list.Add(t_fieldinfo);
							}
						}
					}

					//次の継承元へ。
					t_type = t_type.BaseType;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** GetMethod_Stack_Push

			System.Collections.Generic.Stack<TYPE>.Push(TYPE);

		*/
		public static System.Reflection.MethodInfo GetMethod_Stack_Push(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Push",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_LinkedList_AddLast

			System.Collections.Generic.LinkedList<TYPE>.AddLast(TYPE);

		*/
		public static System.Reflection.MethodInfo GetMethod_LinkedList_AddLast(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"AddLast",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_HashSet_Add

			System.Collections.Generic.HashSet<TYPE>.Add(TYPE);

		*/
		public static System.Reflection.MethodInfo GetMethod_HashSet_Add(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Add",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_Queue_Enqueue

			System.Collections.Generic.Queue<TYPE>.Enqueue(TYPE);

		*/
		public static System.Reflection.MethodInfo GetMethod_Queue_Enqueue(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Enqueue",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_SortedSet_Add

			System.Collections.Generic.SortedSet<TYPE>.Add(TYP));

		*/
		public static System.Reflection.MethodInfo GetMethod_SortedSet_Add(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Add",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}
	}
}

