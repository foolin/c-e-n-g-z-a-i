using System;
namespace CengZai.Model
{
	/// <summary>
	/// Area:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Area
	{
		public Area()
		{}
		#region Model
		private int _areaid;
		private int? _parentid=0;
		private string _enname;
		private string _areaname;
		private string _regionno;
		/// <summary>
		/// 
		/// </summary>
		public int AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EnName
		{
			set{ _enname=value;}
			get{return _enname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionNO
		{
			set{ _regionno=value;}
			get{return _regionno;}
		}
		#endregion Model

	}
}

