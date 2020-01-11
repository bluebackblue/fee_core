mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenFileDialog_Register: function()
	{
		if(!document.getElementById('ID_FEE_FILEOPENDIALOG')){

			var t_input = document.createElement('input');
			{
				t_input.setAttribute('type','file');
				t_input.setAttribute('id','ID_FEE_FILEOPENDIALOG');
				t_input.setAttribute('accept','.*');
				t_input.style.visibility = 'hidden';

				t_input.onchange = function(a_event){
					var t_result = "";

					if(a_event.target.files == null){
					}else if(a_event.target.files[0] == null){
					}else if(a_event.target.files[0] == ""){
					}else{
						t_result = URL.createObjectURL(a_event.target.files[0]);
					}

					SendMessage('Platform','OpenFileDialog_CallBack',t_result);
				}
			}

			document.body.appendChild(t_input);

		}
	},
	Fee_Platform_WebGLPlugin_OpenFileDialog_Open: function(a_title,a_extension)
	{
		var t_extension = Pointer_stringify(a_extension);

		var t_input = document.getElementById('ID_FEE_FILEOPENDIALOG');
		{
			t_input.setAttribute('accept',t_extension);
		}

		t_input.click();
	}
});