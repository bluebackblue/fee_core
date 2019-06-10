

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。エクセルＴＯＪＳＯＮ。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** ExcelToJson
	*/
	public class ExcelToJson
	{
		/** excel
		*/
		private Excel excel;

		/** jsonitem
		*/
		private Fee.JsonItem.JsonItem jsonitem;

		/** pos_root
		*/
		private Pos pos_root;
		private Pos pos_paramtype;
		private Pos pos_paramname;
		private Pos pos_end_y;
		private Pos pos_end_x;

		/** Pos
		*/
		public readonly struct Pos
		{
			/** xy
			*/
			public readonly int x;
			public readonly int y;

			/** constructor
			*/
			public Pos(int a_x,int a_y)
			{
				this.x = a_x;
				this.y = a_y;
			}
		}

		/** ParamItem
		*/
		public struct ParamItem
		{
			/** パラメータタイプ。
			*/
			public string paramtype;

			/** パラメメータ名。
			*/
			public string paramname;

			/** 位置。
			*/
			public int pos_x;

			/** constructor
			*/
			public ParamItem(string a_paramtype,string a_paramname,int a_pos_x)
			{
				this.paramtype = a_paramtype;
				this.paramname = a_paramname;
				this.pos_x = a_pos_x;
			}
		}

		/** constructor
		*/
		public ExcelToJson(File.Path a_full_path)
		{
			this.excel = new Excel(a_full_path);
		}

		/** コンバート。
		*/
		public bool Convert()
		{
			this.jsonitem = new JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());

			{
				if(this.excel.ReadOpen() == true){
					int t_sheet_max = this.excel.GetSheetMax();
					for(int ii=0;ii<t_sheet_max;ii++){
						if(this.excel.OpenSheet(ii) == true){
							this.Convert_Sheet(ii);
							this.excel.CloseSheet();
						}
					}
					this.excel.Close();
				}else{
					//失敗。
					return false;
				}
			}

			return true;
		}

		/** GetJsonString
		*/
		public string GetJsonString()
		{
			return this.jsonitem.ConvertJsonString();
		}

		/** FindCell
		*/
		private bool FindCell(int a_x,int a_y,string a_text,ref Pos a_result_pos)
		{
			string t_cell_string = this.excel.GetCell(a_x,a_y);
			if(t_cell_string == a_text){
				a_result_pos = new Pos(a_x,a_y);
				return true;
			}

			return false;
		}

		/** FindCellBox
		*/
		private bool FindCellBox(int a_x,int a_y,int a_size,string a_text,ref Pos a_result_pos)
		{
			for(int xx=0;xx<a_size;xx++){
				for(int yy=0;yy<a_size;yy++){
					int t_x = a_x + xx;
					int t_y = a_y + yy;
					string t_cell_string = this.excel.GetCell(t_x,t_y);
					if(t_cell_string == a_text){
						a_result_pos = new Pos(t_x,t_y);
						return true;
					}
				}
			}

			return false;
		}

		/** FindCellXLine
		*/
		private bool FindCellXLine(int a_x,int a_y,int a_size,string a_text,ref Pos a_result_pos)
		{
			for(int xx=0;xx<a_size;xx++){
				int t_x = a_x + xx;
				int t_y = a_y;
				string t_cell_string = this.excel.GetCell(t_x,t_y);
				if(t_cell_string == a_text){
					a_result_pos = new Pos(t_x,t_y);
					return true;
				}
			}

			return false;
		}

		/** FindCellYLine
		*/
		private bool FindCellYLine(int a_x,int a_y,int a_size,string a_text,ref Pos a_result_pos)
		{
			for(int yy=0;yy<a_size;yy++){
				int t_x = a_x;
				int t_y = a_y + yy;
				string t_cell_string = this.excel.GetCell(t_x,t_y);
				if(t_cell_string == a_text){
					a_result_pos = new Pos(t_x,t_y);
					return true;
				}
			}

			return false;
		}

		/** FindCell_Root
		*/
		private bool FindCell_Root()
		{
			int t_size = 5;
			for(int xx=0;xx<10;xx++){
				for(int yy=0;yy<10;yy++){
					if(this.FindCellBox(xx * t_size,yy * t_size,t_size,"[root]",ref this.pos_root) == true){
						return true;
					}
				}
			}

			return false;
		}

		/** 
		*/
		private System.Collections.Generic.List<ParamItem> CreateParamList()
		{
			System.Collections.Generic.List<ParamItem> t_list = new System.Collections.Generic.List<ParamItem>();

			for(int xx = this.pos_paramtype.x + 1;xx < this.pos_end_x.x;xx++){
				string t_paramtype_string = this.excel.GetCell(xx,this.pos_paramtype.y);
				string t_paramname_string = this.excel.GetCell(xx,this.pos_paramname.y);
				switch(t_paramtype_string){
				case "string":
				case "int":
				case "float":
					{
						t_list.Add(new ParamItem(t_paramtype_string,t_paramname_string,xx));
					}break;
				case "-comment":
					{
						//スキップ。
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
			}

			return t_list;
		}


		/** コンバート。シート。
		*/
		private void Convert_Sheet(int a_index)
		{
			Pos t_pos_root = new Pos(-1,-1);
			
			if(this.FindCell_Root() == false){
				return;
			}

			if(this.FindCell(this.pos_root.x,this.pos_root.y + 1,"[paramtype]",ref this.pos_paramtype) == false){
				return;
			}

			if(this.FindCell(this.pos_root.x,this.pos_root.y + 2,"[paramname]",ref this.pos_paramname) == false){
				return;
			}

			if(this.FindCellXLine(this.pos_paramtype.x + 1,this.pos_paramtype.y,Config.COMMAND_SEARCH_WIDTH,"[end]",ref this.pos_end_x) == false){
				return;
			}

			if(this.FindCellYLine(this.pos_paramtype.x,this.pos_paramtype.y + 1,Config.COMMAND_SEARCH_WIDTH,"[end]",ref this.pos_end_y) == false){
				return;
			}

			System.Collections.Generic.List<ParamItem> t_param_list = this.CreateParamList();

			Fee.JsonItem.JsonItem t_jsonitem_list = new JsonItem.JsonItem(new Fee.JsonItem.Value_IndexArray());

			for(int yy=this.pos_paramname.y+1;yy<this.pos_end_y.y;yy++){
				string t_flag = this.excel.GetCell(this.pos_paramname.x,yy);
				if(t_flag == "*"){
					Fee.JsonItem.JsonItem t_jsonitem_item = new JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());

					for(int ii=0;ii<t_param_list.Count;ii++){
						
						switch(t_param_list[ii].paramtype){
						case "string":
							{
								string t_cell = this.excel.GetCell(t_param_list[ii].pos_x,yy);
								t_jsonitem_item.AddItem(t_param_list[ii].paramname,new JsonItem.JsonItem(new  Fee.JsonItem.Value_StringData(t_cell)),false);
							}break;
						case "int":
							{
								string t_cell = this.excel.GetCell(t_param_list[ii].pos_x,yy);
								int t_cell_int = int.Parse(t_cell);
								t_jsonitem_item.AddItem(t_param_list[ii].paramname,new JsonItem.JsonItem(new  Fee.JsonItem.Value_Int(t_cell_int)),false);
							}break;
						case "float":
							{
								string t_cell = this.excel.GetCell(t_param_list[ii].pos_x,yy);
								float t_cell_float = float.Parse(t_cell);
								t_jsonitem_item.AddItem(t_param_list[ii].paramname,new JsonItem.JsonItem(new  Fee.JsonItem.Value_Float(t_cell_float)),false);
							}break;
						default:
							{
								Tool.Assert(false);
							}break;
						}
					}

					t_jsonitem_list.AddItem(t_jsonitem_item,false);
				}
			}

			string t_root_string = this.excel.GetCell(this.pos_root.x + 1,this.pos_root.y);
			this.jsonitem.AddItem(t_root_string,t_jsonitem_list,false);
		}
	}

}

