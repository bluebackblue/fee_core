
/** 登録。
*/
mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenFileDialog_Register: function()
	{
		if(!document.getElementById('ID_FEE_FILEOPENDIALOG')){

			var t_input = document.createElement('input');
			{
				t_input.setAttribute('type','file');
				t_input.setAttribute('id','ID_FEE_FILEOPENDIALOG');
				t_input.setAttribute('accept','.*')
				t_input.style.visibility = 'hidden';

				t_input.onclick = function(a_event){
					this.value = null;
				};

				t_input.onchange = function(a_event){
					SendMessage('Platform','OpenFileDialog_CallBack',URL.createObjectURL(a_event.target.files[0]));
				}
			}

			document.body.appendChild(t_input);
		}
	}
});

/** オープン。
*/
mergeInto(LibraryManager.library,{
	Fee_Platform_WebGLPlugin_OpenFileDialog_Open: function()
	{
		document.getElementById('ID_FEE_FILEOPENDIALOG').click();
	}
});