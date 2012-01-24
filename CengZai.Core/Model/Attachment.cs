using System;
namespace CengZai.Model
{
	/// <summary>
	/// Attachment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Attachment
	{
		public Attachment()
		{}
		#region Model
		private int _attachid;
		private int? _artid;
		private string _source;
		/// <summary>
		/// 
		/// </summary>
		public int AttachID
		{
			set{ _attachid=value;}
			get{return _attachid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ArtID
		{
			set{ _artid=value;}
			get{return _artid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		#endregion Model

	}
}

