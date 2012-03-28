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
		/// 朋友类型：0=关注，1=朋友，-1=黑名单
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion Model

	}
}

