using System;
using CengZai.Helper;
namespace CengZai.Model
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{
            _config = new UserConfig();
        }
		#region Model
		private int _userid;
		private string _email;
		private string _password;
        private string _username;
		private string _nickname;
        private string _avatar;
        private int? _love; //添加恋爱状态
		private string _sign;
		private string _intro;
		private DateTime? _birth;
		private int? _sex;
		private int? _areaid;
		private string _mobile;
		private string _loginip;
		private DateTime? _logintime;
		private int? _logincount=0;
		private string _regip;
		private DateTime? _regtime;
		private int? _state;
		private int? _privacy;
        private int? _credit;
        private int? _vip;
        private int? _money;
        private UserConfig _config;
		/// <summary>
		/// 用户ID
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
        /// 用户名，做用户标识或者域名前缀
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
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
        /// 用户头像
        /// </summary>
        public string Avatar
        {
            set { _avatar = value; }
            get { return _avatar; }
        }
        /// <summary>
        /// 爱情状态：0=爱我吧,1=我在恋爱,2=失恋,3=已婚,4=请勿打扰
        /// </summary>
        public int? Love
        {
            set { _love = value; }
            get { return _love; }
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
		/// 生日
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
		/// 地区
		/// </summary>
		public int? AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 登录IP
		/// </summary>
		public string LoginIp
		{
			set{ _loginip=value;}
			get{return _loginip;}
		}
		/// <summary>
		/// 登录时间
		/// </summary>
		public DateTime? LoginTime
		{
			set{ _logintime=value;}
			get{return _logintime;}
		}
		/// <summary>
		/// 登录次数
		/// </summary>
		public int? LoginCount
		{
			set{ _logincount=value;}
			get{return _logincount;}
		}
		/// <summary>
		/// 注册IP
		/// </summary>
		public string RegIp
		{
			set{ _regip=value;}
			get{return _regip;}
		}
		/// <summary>
		/// 注册时间
		/// </summary>
		public DateTime? RegTime
		{
			set{ _regtime=value;}
			get{return _regtime;}
		}
		/// <summary>
		/// 0=未激活，1=已经激活，-1=锁定
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 隐私：0=公开，1=好友(关注的人)，2=自己
		/// </summary>
		public int? Privacy
		{
			set{ _privacy=value;}
			get{return _privacy;}
		}
        /// <summary>
        /// 积分：用来激励用户活跃数
        /// </summary>
        public int? Credit
        {
            set { _credit = value; }
            get { return _credit; }
        }
        /// <summary>
        /// VIP：0=否，1=VIP等级1,2=VIP等级2,....
        /// </summary>
        public int? Vip
        {
            set { _vip = value; }
            get { return _vip; }
        }
        /// <summary>
        /// 用户帐户金额，以角为单位
        /// </summary>
        public int? Money
        {
            set { _money = value; }
            get { return _money; }
        }
        ///// <summary>
        ///// 用户配置
        ///// </summary>
        //public string Config
        //{
        //    set { _config = value; }
        //    get { return _config; }
        //}
        /// <summary>
        /// 用户配置
        /// </summary>
        public UserConfig Config
        {
            set { _config = value; }
            get { return _config; }
        }
		#endregion Model

	}

}


/// <summary>
/// 恋爱状态
/// </summary>
public enum LoveState
{
    Waiting = 0, //爱我吧
    Loving = 1, //我在恋爱
    Lost = 2,   //失恋
    Marry = 3, //已婚
    Hide = 4, //请勿打扰
}

public class UserConfig
{
    public UserConfig()
    {
        this.BlogSkin = "";
    }

    /// <summary>
    /// 博客皮肤
    /// </summary>
    public string BlogSkin
    {
        set;
        get;
    }


    #region __静态转换方法___
    public static UserConfig ToModel(string config)
    {
        UserConfig modelConfig = null;
        try
        {
            modelConfig = JsonConvert.JavascriptDeserialize<UserConfig>(config);
        }
        catch (Exception ex)
        {
            Log.Error("UserConfig转换JavascriptDeserialize异常：" + config, ex);
        }
        return modelConfig;
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns></returns>
    public static string ToString(UserConfig config)
    {
        if (config == null)
        {
            return "";
        }
        string ret = "";
        try
        {
            ret = JsonConvert.JavascriptSerialize(config);
        }
        catch (Exception ex)
        {
            Log.Error("UserConfig转换Json异常", ex);
        }
        return ret;
    }
    #endregion

}

