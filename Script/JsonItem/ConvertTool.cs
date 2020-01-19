

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
		*/
		public static System.Reflection.MethodInfo GetMethod_Stack_Push(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			/*
			System.Collections.Generic.Stack<int> t_test = new System.Collections.Generic.Stack<int>();
			t_test.Push((int)0);
			*/

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Push",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_LinkedList_AddLast
		*/
		public static System.Reflection.MethodInfo GetMethod_LinkedList_AddLast(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			/*
			System.Collections.Generic.LinkedList<int> t_test = new System.Collections.Generic.LinkedList<int>();
			t_test.AddLast((int)0);
			*/

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"AddLast",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_HashSet_Add
		*/
		public static System.Reflection.MethodInfo GetMethod_HashSet_Add(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			/*
			System.Collections.Generic.HashSet<int> t_test = new System.Collections.Generic.HashSet<int>();
			t_test.Add((int)0);
			*/

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Add",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_Queue_Enqueue
		*/
		public static System.Reflection.MethodInfo GetMethod_Queue_Enqueue(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			/*
			System.Collections.Generic.Queue<int> t_test = new System.Collections.Generic.Queue<int>();
			t_test.Enqueue((int)0);
			*/

			try{
				t_methodinfo = Fee.ReflectionTool.Utility.FindMethodAllParam1(a_type,"Enqueue",a_value_type);
				Tool.Assert(t_methodinfo != null);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_SortedSet_Add
		*/
		public static System.Reflection.MethodInfo GetMethod_SortedSet_Add(System.Type a_type,System.Type a_value_type)
		{
			System.Reflection.MethodInfo t_methodinfo = null;

			/*
			System.Collections.Generic.SortedSet<int> t_test = new System.Collections.Generic.SortedSet<int>();
			t_test.Add((int)0);
			*/

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

