mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenUrl: function(a_url)
	{
        	var t_url = Pointer_stringify(a_url);
		window.open(t_url);
	}
});