using System;
namespace CengZai.Model
{
	/// <summary>
	/// Like:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Like
	{
		public Like()
		{}
		#region Model
		private int _likeid;
		private int? _likeuserid;
		private int? _likeartid;
		private int? _userid;
		private int? _type;
		/// <summary>
		/// 
		/// </summary>
		public int LikeID
		{
			set{ _likeid=value;}
			get{return _likeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LikeUserID
		{
			set{ _likeuserid=value;}
			get{return _likeuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LikeArtID
		{
			set{ _likeartid=value;}
			get{return _likeartid;}
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
		/// 类型：0=关注，1=喜欢
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}
}

