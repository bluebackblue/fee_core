

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
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_Stack_Push
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
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}

		/** GetMethod_SortedSet
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
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_methodinfo;
		}




		/** ParameterCheck
		*/
		/*
		public static bool ParameterCheck(System.Reflection.MethodInfo a_methodinfo,string a_name,System.Type[] a_parameter_list)
		{
			bool t_result = false;

			try{
				if((a_methodinfo != null)&&(a_parameter_list != null)){
					if(a_methodinfo.Name == a_name){
						System.Reflection.ParameterInfo[] t_parameterinfo_list = a_methodinfo.GetParameters();
						if(t_parameterinfo_list.Length == a_param_list.Length){

							t_result = true;

							for(int ii=0;ii<t_parameterinfo_list.Length;ii++){
								if(t_parameterinfo_list[ii].ParameterType != a_param_list[ii]){
									t_result = false;
									break;
								}
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_result = false;
			}

			return t_result;
		}
		*/

		/** GetGenericTypeDefinition

			現在のジェネリック型を構築する元になるジェネリック型定義を表す。

		*/
		/*
		public static System.Type GetGenericTypeDefinition(System.Type a_type)
		{
			System.Type t_type = null;

			if(a_type != null){
				t_type = a_type.GetGenericTypeDefinition();
			}

			return t_type;
		}
		*/

		/** GetDictionaryValueType

			Dictionaryの値型。

		*/
		/*
		public static System.Type GetDictionaryValueType(System.Type a_dictionary_type)
		{
			System.Type t_type = null;

			System.Type[] t_typelist_value = a_dictionary_type.GetGenericArguments();
			if(t_typelist_value.Length > 1){
				t_type = t_typelist_value[1];
			}

			if(t_type == null){
				Tool.Assert(false);
				t_type = typeof(System.Object);
			}

			return t_type;
		}
		*/

		/** GetListValueType

			リスト型の値型。

		*/
		/*
		public static System.Type GetListValueType(System.Type a_list_type)
		{
			System.Type t_type = null;

			if(a_list_type.IsArray == true){
				//type == x[]

				t_type = a_list_type.GetElementType();
			}else{
				//List

				System.Type[] t_type_list = a_list_type.GetGenericArguments();
				if(t_type_list != null){
					if(t_type_list.Length > 0){
						t_type = t_type_list[0];
					}else{
						t_type = a_list_type.GetElementType();
					}
				}else{
					t_type = a_list_type.GetElementType();
				}
			}

			if(t_type == null){
				Tool.Assert(false);
				t_type = typeof(System.Object);
			}

			return t_type;
		}
		*/

		/** GetMethodList
		*/
		/*
		public static System.Collections.Generic.List<System.Reflection.MethodInfo> GetMethodList(System.Type a_class_type)
		{
			System.Collections.Generic.List<System.Reflection.MethodInfo> t_result = new System.Collections.Generic.List<System.Reflection.MethodInfo>();

			System.Type t_type = a_class_type;

			while(true){

				//終端チェック。
				if(t_type == null){
					break;
				}else if(t_type == typeof(System.Object)){
					break;
				}

				//取得。
				System.Reflection.MethodInfo[] t_methodinfo_list = t_type.GetMethods(
				
					//指定した型の階層のレベルで宣言されたメンバーのみを対象にすることを指定します。 継承されたメンバーは対象になりません。
					System.Reflection.BindingFlags.DeclaredOnly |
							
					//インスタンス メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Instance |
							
					//パブリック メンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.Public |
							
					//パブリック メンバー以外のメンバーを検索に含めることを指定します。
					System.Reflection.BindingFlags.NonPublic
				);

				foreach(System.Reflection.MethodInfo t_methodinfo in t_methodinfo_list){
					t_result.Add(t_methodinfo);
				}

				//次の継承元へ。
				t_type = t_type.BaseType;
			}

			return t_result;
		}
		*/


	}
}

