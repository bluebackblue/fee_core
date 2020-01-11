mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenPrompt: function(a_title,a_text)
	{
		var t_title = Pointer_stringify(a_title);
		var t_text = Pointer_stringify(a_text);

		var t_result = window.prompt(t_title,t_text);

		if(t_result == null){
			t_result = t_text;
		}

		var t_buffer_size = lengthBytesUTF8(t_result)+1;
		var t_buffer = _malloc(t_buffer_size);
		stringToUTF8(t_result,t_buffer,t_buffer_size);

		return t_buffer;
	}
});