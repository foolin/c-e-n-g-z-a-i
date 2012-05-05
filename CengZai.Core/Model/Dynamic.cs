using System;
namespace CengZai.Model
{
	/// <summary>
	/// Dynamic:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Dynamic
	{
		public Dynamic()
		{}
		#region Model
		private int _dynid;
		private string _content;
		private int? _userid;
		private DateTime? _posttime;
		/// <summary>
		/// 
		/// </summary>
		public int DynID
		{
			set{ _dynid=value;}
			get{return _dynid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PostTime
		{
			set{ _posttime=value;}
			get{return _posttime;}
		}
		#endregion Model

	}
}

