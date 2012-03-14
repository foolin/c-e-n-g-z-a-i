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
		private int? _certificate;
		private DateTime? _joindate;
		private int? _applyuserid;
		private DateTime? _applytime;
		private int? _flow;
        private int? _state;
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
		public int? Certificate
		{
			set{ _certificate=value;}
			get{return _certificate;}
		}
		/// <summary>
		/// 结婚时间
		/// </summary>
		public DateTime? JoinDate
		{
			set{ _joindate=value;}
			get{return _joindate;}
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
		/// 当前状态：0=证书作废,1=已提出申请,-1=撤销申请,2=对方确认,-2=对方拒绝,3=已审核通过颁发证书。-3=确认不通过
		/// </summary>
		public int? Flow
		{
			set{ _flow=value;}
			get{return _flow;}
		}
        /// <summary>
        /// 状态：-1=已作废,0=处理中,1=已经颁发
        /// </summary>
        public int? State
        {
            set { _state = value; }
            get { return _state; }
        }
		#endregion Model

	}

    /// <summary>
    /// Lover状态
    /// </summary>
    public enum LoverFlow
    {
        /// <summary>
        /// 废除
        /// </summary>
        Unknow = 0,
        /// <summary>
        /// 申请
        /// </summary>
        Apply = 1,
        /// <summary>
        /// 取消申请
        /// </summary>
        UnApply = -1,
        /// <summary>
        /// 对方接受
        /// </summary>
        Accept =2,
        /// <summary>
        /// 对方拒绝
        /// </summary>
        UnAccept = -2,
        /// <summary>
        /// 授予证书
        /// </summary>
        Award = 3,
        /// <summary>
        /// 不授予证书
        /// </summary>
        UnAward = -3,
    }

    public enum LoverCertificate
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkown = 0,
        /// <summary>
        /// 恋爱证
        /// </summary>
        Love = 1,
        /// <summary>
        /// 结婚证
        /// </summary>
        Marry= 2,
    }
}

