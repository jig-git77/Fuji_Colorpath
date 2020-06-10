using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TUserSession")]
	public class NDLG_UserSession
	{
		[Index("UserID", 2)]
		[Required]
		[StringLength(46)]
		public string m_ClientHostIP
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ClientHostName
		{
			get;
			set;
		}

		public DateTime m_CreateTime
		{
			get;
			set;
		}

		[Key]
		public int m_ID
		{
			get;
			set;
		}

		[DefaultValue("CURRENT_TIMESTAMP")]
		public DateTime m_ModifyTime
		{
			get;
			set;
		}

		public int m_RequestCount
		{
			get;
			set;
		}

		[Index("UserID", 3)]
		[Required]
		[StringLength(46)]
		public string m_ServerHostIP
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_ServerHostName
		{
			get;
			set;
		}

		[Index("UserID", 4)]
		[StringLength(128)]
		public string m_SessID
		{
			get;
			set;
		}

		public byte[] m_SessionData
		{
			get;
			set;
		}

		[ForeignKey("TUser3", Fk_constraint.k_cascade, Fk_constraint.k_cascade, table="TUser", column="ID")]
		[Index("UserID", 1, is_unique=true)]
		[NoDefault]
		public int m_UserID
		{
			get;
			set;
		}

		public NDLG_UserSession()
		{
		}
	}
}