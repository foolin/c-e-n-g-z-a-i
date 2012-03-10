using System;
namespace CengZai.Model
{
	/// <summary>
	/// LoverComment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LoverComment
	{
		public LoverComment()
		{}
		#region Model
		private int _commentid;
		private int? _loverid;
		private string _title;
		private string _content;
		private int? _userid;
		private DateTime? _posttime;
		private string _postip;
		private int? _parentid;
		/// <summary>
		/// 
		/// </summary>
		public int CommentID
		{
			set{ _commentid=value;}
			get{return _commentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LoverID
		{
			set{ _loverid=value;}
			get{return _loverid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// <summary>
		/// 
		/// </summary>
		public string PostIP
		{
			set{ _postip=value;}
			get{return _postip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		#endregion Model

	}
}

