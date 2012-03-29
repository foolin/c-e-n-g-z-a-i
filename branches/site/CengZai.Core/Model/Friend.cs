using System;
namespace CengZai.Model
{
	/// <summary>
	/// Friend:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Friend
	{
		public Friend()
		{}
		#region Model
		private int _id;
		private int? _userid;
		private int? _frienduserid;
		private int? _type;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		public int? FriendUserID
		{
			set{ _frienduserid=value;}
			get{return _frienduserid;}
		}
		/// <summary>
		/// 朋友类型：0=关注，1=密友（暂无效），-1=黑名单
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}

    /// <summary>
    /// 朋友类型
    /// </summary>
    public enum FriendType
    {
        /// <summary>
        /// 我关注的人
        /// </summary>
        Follow = 0,
        /// 黑名单
        /// </summary>
        Black = -1
    }

    /// <summary>
    /// 朋友关系
    /// </summary>
    public enum FriendRelation
    {
        /// <summary>
        /// 我关注的人
        /// </summary>
        Follow = 0,
        /// <summary>
        /// 关注我的人
        /// </summary>
        Fans = 1,
        /// <summary>
        /// 朋友，互相关注
        /// </summary>
        Friend = 2,
        /// <summary>
        /// 朋友，互相关注
        /// </summary>
        FollowOrFans = 3,
        /// <summary>
        /// 黑名单
        /// </summary>
        Black = -1
    }
}

