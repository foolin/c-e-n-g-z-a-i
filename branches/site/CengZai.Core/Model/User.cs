using System;
namespace CengZai.Model
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private int _userid;
		private string _email;
		private string _password;
		private string _nickname;
		private string _sign;
		private string _intro;
		private DateTime? _birth;
		private int? _sex;
		private int? _areaid;
		private string _mobile;
		private string _loginip;
		private DateTime? _logintime;
		private string _regip;
		private DateTime? _regtime;
		private int? _state;
		private int? _private;
		private int? _logincount;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string Nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 签名
		/// </summary>
		public string Sign
		{
			set{ _sign=value;}
			get{return _sign;}
		}
		/// <summary>
		/// 自我介绍
		/// </summary>
		public string Intro
		{
			set{ _intro=value;}
			get{return _intro;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birth
		{
			set{ _birth=value;}
			get{return _birth;}
		}
		/// <summary>
		/// 0=保密，1=男，2=女
		/// </summary>
		public int? Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginIp
		{
			set{ _loginip=value;}
			get{return _loginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LoginTime
		{
			set{ _logintime=value;}
			get{return _logintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegIp
		{
			set{ _regip=value;}
			get{return _regip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegTime
		{
			set{ _regtime=value;}
			get{return _regtime;}
		}
		/// <summary>
		/// 0=未激活，1=已经激活
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 隐私：0=公开，1=好友(关注的人)，2=自己
		/// </summary>
		public int? Private
		{
			set{ _private=value;}
			get{return _private;}
		}
		/// <summary>
		/// 登录次数
		/// </summary>
		public int? LoginCount
		{
			set{ _logincount=value;}
			get{return _logincount;}
		}
		#endregion Model

	}
}

