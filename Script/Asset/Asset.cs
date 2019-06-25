

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセット。
*/


/** Fee.Asset
*/
namespace Fee.Asset
{
	/** Asset
	*/
	public class Asset
	{
		/** assettype
		*/
		public AssetType asset_type;

		/** asset_object
		*/
		public object asset_object;

		/** constructor
		*/
		public Asset()
		{
			//asset_type
			this.asset_type = AssetType.None;
			
			//asset_object
			this.asset_object = null;
		}

		/** constructor
		*/
		public Asset(AssetType a_asset_type,object a_asset_object)
		{
			//asset_type
			this.asset_type = a_asset_type;
			
			//asset_object
			this.asset_object = a_asset_object;
		}

		/** GetAssetType
		*/
		public AssetType GetAssetType()
		{
			return this.asset_type;
		}

		/** GetText
		*/
		public string GetText()
		{
			return this.asset_object as string;
		}

		/** GetTexture
		*/
		public UnityEngine.Texture2D GetTexture()
		{
			return this.asset_object as UnityEngine.Texture2D;
		}

		/** GetBinary
		*/
		public byte[] GetBinary()
		{
			return this.asset_object as byte[];
		}

		/** GetPrefab
		*/
		public UnityEngine.GameObject GetPrefab()
		{
			return this.asset_object as UnityEngine.GameObject;
		}

		/** SetText
		*/
		public void SetText(string a_text)
		{
			this.asset_type = AssetType.Text;
			this.asset_object = a_text;
		}

		/** SetTexture
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.asset_type = AssetType.Texture;
			this.asset_object = a_texture;
		}

		/** SetBinary
		*/
		public void SetBinary(byte[] a_binary)
		{
			this.asset_type = AssetType.Text;
			this.asset_object = a_binary;
		}

		/** SetPrefab
		*/
		public void SetPrefab(UnityEngine.GameObject a_prefab)
		{
			this.asset_type = AssetType.Prefab;
			this.asset_object = a_prefab;
		}
	}
}

