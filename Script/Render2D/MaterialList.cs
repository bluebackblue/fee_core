

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MaterialList
	*/
	public class MaterialList
	{
		/** list
		*/
		private UnityEngine.Material[] list;

		/** material_ui_text
		*/
		private UnityEngine.Material material_ui_text;

		/** material_ui_image
		*/
		private UnityEngine.Material material_ui_image;

		/** constructor
		*/
		public MaterialList()
		{
			//list
			this.list = new UnityEngine.Material[Config.MATERIAL_NAME.Length];
			for(int ii=0;ii<this.list.Length;ii++){
				UnityEngine.Material t_material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME[ii]);
				if(t_material != null){
					this.list[ii] = new UnityEngine.Material(t_material);
				}
			}

			//material_ui_text
			this.material_ui_text = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_UITEXT);

			//material_ui_image
			this.material_ui_image = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_UIIMAGE);
		}

		/** マテリアル。取得。
		*/
		public UnityEngine.Material GetMaterial(Render2D.MaterialType a_material_type)
		{
			return this.list[(int)a_material_type];
		}

		/** ＵＩテキストマテリアル。取得。
		*/
		public UnityEngine.Material GetUiTextMaterial()
		{
			return this.material_ui_text;
		}

		/** ＵＩイメージマテリアル。取得。
		*/
		public UnityEngine.Material GetUiImageMaterial()
		{
			return this.material_ui_image;
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.list != null){
				for(int ii=0;ii<this.list.Length;ii++){
					if(this.list[ii] != null){
						UnityEngine.GameObject.DestroyImmediate(this.list[ii]);
						this.list[ii] = null;
					}
				}
				this.list = null;
			}
		}
	}
}

