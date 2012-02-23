using System;
namespace CengZai.Model
{
	/// <summary>
	/// Article:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Article
	{
		public Article()
		{}
		#region Model
		private int _artid;
		private int? _categoryid;
		private string _title;
		private string _content;
		private int? _type;
		private int? _istop;
		private int? _userid;
		private DateTime? _posttime;
		private string _postip;
		private int? _viewcount;
		private int? _replycount;
		private int? _reportcount;
		private int? _privacy;
		private int? _state;
		/// <summary>
		/// 
		/// </summary>
		public int ArtID
		{
			set{ _artid=value;}
			get{return _artid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 0=文章,1=图片
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 是否置顶:0=否，1=是
		/// </summary>
		public int? IsTop
		{
			set{ _istop=value;}
			get{return _istop;}
		}
		/// <summary>
		/// 用户ID
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
		/// 浏览次数
		/// </summary>
		public int? ViewCount
		{
			set{ _viewcount=value;}
			get{return _viewcount;}
		}
		/// <summary>
		/// 回复次数
		/// </summary>
		public int? ReplyCount
		{
			set{ _replycount=value;}
			get{return _replycount;}
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
		/// 隐私设置：0=所有人可见，1=仅好友可见，2=仅自己可见
		/// </summary>
		public int? Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
		/// <summary>
		/// 状态：-1=删除，0=草稿，1=发布
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		#endregion Model

	}

    /// <summary>
    /// 文章类型
    /// </summary>
    public enum ArticleType
    {
        /// <summary>
        /// 普通文章
        /// </summary>
        Text = 0,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 1,
    }
}

