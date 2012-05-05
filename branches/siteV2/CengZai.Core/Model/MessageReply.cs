using System;
namespace CengZai.Model
{
	/// <summary>
	/// MessageReply:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MessageReply
	{
		public MessageReply()
		{}
		#region Model
		private int _replyid;
		private int? _msgid;
		private string _content;
		private int? _replyuserid;
		private DateTime? _replytime;
		/// <summary>
		/// 
		/// </summary>
		public int ReplyID
		{
			set{ _replyid=value;}
			get{return _replyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MsgID
		{
			set{ _msgid=value;}
			get{return _msgid;}
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
		public int? ReplyUserID
		{
			set{ _replyuserid=value;}
			get{return _replyuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReplyTime
		{
			set{ _replytime=value;}
			get{return _replytime;}
		}
		#endregion Model

	}
}

