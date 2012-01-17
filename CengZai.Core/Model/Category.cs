using System;
namespace CengZai.Model
{
	/// <summary>
	/// Category:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Category
	{
		public Category()
		{}
		#region Model
		private int _categoryid;
		private string _categoryname;
		private string _categorydesc;
        private int? _userid;
		/// <summary>
		/// 分类ID
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 分类名称
		/// </summary>
		public string CategoryName
		{
			set{ _categoryname=value;}
			get{return _categoryname;}
		}
		/// <summary>
		/// 分类描述
		/// </summary>
		public string CategoryDesc
		{
			set{ _categorydesc=value;}
			get{return _categorydesc;}
		}

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
		#endregion Model

	}
}

