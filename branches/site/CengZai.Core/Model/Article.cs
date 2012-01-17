using System;
namespace CengZai.Model
{
    public enum ArtType
    {
        /// <summary>
        /// 微博/心情
        /// </summary>
        Weibo = 0,
        /// <summary>
        /// 文本提交
        /// </summary>
        Text = 1,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 2,
        /// <summary>
        /// 音频
        /// </summary>
        Audio = 3,
        /// <summary>
        /// 视频
        /// </summary>
        Video = 4,
        /// <summary>
        /// 连接
        /// </summary>
        Link = 5,
    }


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
		private string _source;
		private int? _type;
		private int? _istop;
		private int? _userid;
		private DateTime? _posttime;
		private string _postip;
		private int? _viewcount;
		private int? _topcount;
		private int? _downcount;
		private int? _reportcount;
		private int? _private;
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
		/// Url资源（图片、音频、视频、连接），多用竖线“|”分割
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
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
		/// 顶次数
		/// </summary>
		public int? TopCount
		{
			set{ _topcount=value;}
			get{return _topcount;}
		}
		/// <summary>
		/// 踩次数
		/// </summary>
		public int? DownCount
		{
			set{ _downcount=value;}
			get{return _downcount;}
		}
		/// <summary>
		/// 举报
		/// </summary>
		public int? ReportCount
		{
			set{ _reportcount=value;}
			get{return _reportcount;}
		}
		/// <summary>
		/// 隐私设置：0=否，1=是
		/// </summary>
		public int? Private
		{
			set{ _private=value;}
			get{return _private;}
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

