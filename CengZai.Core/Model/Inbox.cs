using System;
namespace CengZai.Model
{
	/// <summary>
	/// Inbox:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Inbox
	{
		public Inbox()
		{}
		#region Model
		private int _msgid;
		private string _title;
		private string _content;
		private int? _touserid;
		private int? _fromuserid;
		private DateTime? _sendtime;
		private int? _isread;
		private int? _issystem;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public int MsgID
		{
			set{ _msgid=value;}
			get{return _msgid;}
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
		/// 接收者
		/// </summary>
		public int? ToUserID
		{
			set{ _touserid=value;}
			get{return _touserid;}
		}
		/// <summary>
		/// 发送者
		/// </summary>
		public int? FromUserID
		{
			set{ _fromuserid=value;}
			get{return _fromuserid;}
		}
		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime? SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 是否阅读:0=否，1=是
		/// </summary>
		public int? IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		/// <summary>
		/// 是否系统信息：0=否，1=是
		/// </summary>
		public int? IsSystem
		{
			set{ _issystem=value;}
			get{return _issystem;}
		}
		/// <summary>
		/// 是否删除，0=否，1=发送人删除，2=接收人删除
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

