using System;
namespace CengZai.Model
{
	/// <summary>
	/// Attachment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Attachment
	{
		public Attachment()
		{}
		#region Model
		private int _attachid;
		private int? _source;
		private int? _sourceid;
		private string _filepath;
		private string _fileext;
		private int? _fiesize;
		private string _filedata;
		private int? _userid;
		private DateTime? _uploadtime;
		/// <summary>
		/// 
		/// </summary>
		public int AttachID
		{
			set{ _attachid=value;}
			get{return _attachid;}
		}
		/// <summary>
		/// 来源：Article（文章）=1,Lover（证书）=2
		/// </summary>
		public int? Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 文件扩展名，如jpg/png/gif
		/// </summary>
		public string FileExt
		{
			set{ _fileext=value;}
			get{return _fileext;}
		}
		/// <summary>
		/// 文件大小
		/// </summary>
		public int? FieSize
		{
			set{ _fiesize=value;}
			get{return _fiesize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileData
		{
			set{ _filedata=value;}
			get{return _filedata;}
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
		public DateTime? UploadTime
		{
			set{ _uploadtime=value;}
			get{return _uploadtime;}
		}
		#endregion Model

	}


    /// <summary>
    /// 附件类型
    /// </summary>
    public enum AttachmentSource
    {
        /// <summary>
        /// 相片
        /// </summary>
        Article = 1,
        /// <summary>
        /// 用户头像
        /// </summary>
        User = 2,
        /// <summary>
        /// 情侣合照头像
        /// </summary>
        Lover = 3
    }
}

