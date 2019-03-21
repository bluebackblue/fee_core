mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenPrompt: function(a_title,a_text)
	{
        	var t_title = Pointer_stringify(a_title);
        	var t_text = Pointer_stringify(a_text);

		var t_result = window.prompt(t_title,t_text);

		if(!t_result){
			return a_text;
		}

		var t_buffer = _malloc(lengthBytesUTF8(t_result)+1);
		writeStringToMemory(t_result,t_buffer);
		return t_buffer;
	}
});