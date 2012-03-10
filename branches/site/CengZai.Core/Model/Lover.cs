using System;
namespace CengZai.Model
{
	/// <summary>
	/// Lover:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Lover
	{
		public Lover()
		{}
		#region Model
		private int _loverid;
		private string _avatar;
		private int? _boyuserid;
		private int? _girluserid;
		private string _boyoath;
		private string _girloath;
		private int? _lovestate;
		private DateTime? _metdate;
		private DateTime? _lovedate;
		private DateTime? _marrydate;
		private int? _applyuserid;
		private DateTime? _applytime;
		private int? _isconfirm;
		private int? _confirmuserid;
		private DateTime? _confirmtime;
		private int? _currstate;
		private int? _curruserid;
		/// <summary>
		/// 
		/// </summary>
		public int LoverID
		{
			set{ _loverid=value;}
			get{return _loverid;}
		}
		/// <summary>
		/// 合影
		/// </summary>
		public string Avatar
		{
			set{ _avatar=value;}
			get{return _avatar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BoyUserID
		{
			set{ _boyuserid=value;}
			get{return _boyuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GirlUserID
		{
			set{ _girluserid=value;}
			get{return _girluserid;}
		}
		/// <summary>
		/// 男方誓言
		/// </summary>
		public string BoyOath
		{
			set{ _boyoath=value;}
			get{return _boyoath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GirlOath
		{
			set{ _girloath=value;}
			get{return _girloath;}
		}
		/// <summary>
		/// 0=等待确立关系，1=恋爱，2=结婚
		/// </summary>
		public int? LoveState
		{
			set{ _lovestate=value;}
			get{return _lovestate;}
		}
		/// <summary>
		/// 相识日期
		/// </summary>
		public DateTime? MetDate
		{
			set{ _metdate=value;}
			get{return _metdate;}
		}
		/// <summary>
		/// 恋爱日期，不可更改
		/// </summary>
		public DateTime? LoveDate
		{
			set{ _lovedate=value;}
			get{return _lovedate;}
		}
		/// <summary>
		/// 结婚时间
		/// </summary>
		public DateTime? MarryDate
		{
			set{ _marrydate=value;}
			get{return _marrydate;}
		}
		/// <summary>
		/// 申请人/男方/女方的UserID
		/// </summary>
		public int? ApplyUserID
		{
			set{ _applyuserid=value;}
			get{return _applyuserid;}
		}
		/// <summary>
		/// 申请日期
		/// </summary>
		public DateTime? ApplyTime
		{
			set{ _applytime=value;}
			get{return _applytime;}
		}
		/// <summary>
		/// 是否已经公证人
		/// </summary>
		public int? IsConfirm
		{
			set{ _isconfirm=value;}
			get{return _isconfirm;}
		}
		/// <summary>
		/// 公证人：0=系统，大于1表示用户
		/// </summary>
		public int? ConfirmUserID
		{
			set{ _confirmuserid=value;}
			get{return _confirmuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ConfirmTime
		{
			set{ _confirmtime=value;}
			get{return _confirmtime;}
		}
		/// <summary>
		/// 当前状态：1=已提出申请,2=对方确认,3=已审核通过颁发证书。-3=确认不通过
		/// </summary>
		public int? CurrState
		{
			set{ _currstate=value;}
			get{return _currstate;}
		}
		/// <summary>
		/// 当前用户ID，系统为10000
		/// </summary>
		public int? CurrUserID
		{
			set{ _curruserid=value;}
			get{return _curruserid;}
		}
		#endregion Model

	}
}

