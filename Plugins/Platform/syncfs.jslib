mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_SyncFs: function()
	{
		var t_result = true;
		FS.syncfs(false,function(a_error){
			t_result = false;
		});
		return t_result;
	}
});