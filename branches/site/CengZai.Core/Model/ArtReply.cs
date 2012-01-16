using System;
namespace CengZai.Model
{
	/// <summary>
	/// ArtReply:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ArtReply
	{
		public ArtReply()
		{}
		#region Model
		private int _replyid;
		private int? _artid;
		private string _content;
		private int? _userid;
		private DateTime? _posttime;
		private string _postip;
		private int? _reportcount;
		private int? _state;
		/// <summary>
		/// 
		/// </summary>
		public int ReplyID
		{
			set{ _replyid=value;}
			get{return _replyid;}
		}
		/// <summary>
		/// 文章ID
		/// </summary>
		public int? ArtID
		{
			set{ _artid=value;}
			get{return _artid;}
		}
		/// <summary>
		/// 回复内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 提交者
		/// </summary>
		public int? UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 提交时间
		/// </summary>
		public DateTime? PostTime
		{
			set{ _posttime=value;}
			get{return _posttime;}
		}
		/// <summary>
		/// 提交IP
		/// </summary>
		public string PostIP
		{
			set{ _postip=value;}
			get{return _postip;}
		}
		/// <summary>
		/// 举报次数
		/// </summary>
		public int? ReportCount
		{
			set{ _reportcount=value;}
			get{return _reportcount;}
		}
		/// <summary>
		/// 状态：0=正常，-1=删除
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model

	}
}

