using Oris.DB_interface;
using System;
using System.Runtime.CompilerServices;

namespace Lynx
{
	[Table("TJob")]
	public class NJM_Job
	{
		[Required]
		[StringLength(255)]
		public string m_ClassModifyReason
		{
			get;
			set;
		}

		public DateTime? m_ClassModifyTime
		{
			get;
			set;
		}

		public int? m_ClassModifyUserID
		{
			get;
			set;
		}

		[Timestamp]
		public DateTime m_CreateTime
		{
			get;
			set;
		}

		public int? m_CreateUserID
		{
			get;
			set;
		}

		[ForeignKey(table="TCustomer", column="ID")]
		[Index("Name", 2, is_unique=true)]
		public int? m_CustomerID
		{
			get;
			set;
		}

		[Index(is_unique=true)]
		[StringLength(255)]
		public string m_CustomJobID
		{
			get;
			set;
		}

		public int? m_Delete_Error_AfterSeconds
		{
			get;
			set;
		}

		public int? m_Delete_Success_AfterSeconds
		{
			get;
			set;
		}

		[Required]
		[Text]
		public string m_Description
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_DisplayName
		{
			get;
			set;
		}

		public DateTime? m_DueDateTime
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

		[Index(is_unique=true)]
		[StringLength(255)]
		public string m_JobRootFolder
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_MasterFilename
		{
			get;
			set;
		}

		[DefaultValue("5")]
		public uint m_MaxServerHog
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

		public int? m_ModifyUserID
		{
			get;
			set;
		}

		[Required]
		[StringLength(255)]
		public string m_Name
		{
			get;
			set;
		}

		public Db_job_options m_Options
		{
			get;
			set;
		}

		public int? m_OwnerUserID
		{
			get;
			set;
		}

		public int? m_PrioBoostAfterSeconds
		{
			get;
			set;
		}

		private Db_job_prio_class m_PrioClass
		{
			get;
			set;
		}

		public DateTime? m_Status_Error_Time
		{
			get;
			set;
		}

		public DateTime? m_Status_Success_Time
		{
			get;
			set;
		}

		[StringLength(255)]
		public string m_TaskRequireHost
		{
			get;
			set;
		}

		[Index("Name", 1, is_unique=true)]
		[Required]
		[StringLength(255)]
		public string m_TypeName
		{
			get;
			set;
		}

		[ForeignKey(table="TWorkflowType", column="ID")]
		[Index("Name", 3, is_unique=true)]
		public int? m_WorkflowTypeID
		{
			get;
			set;
		}

		public NJM_Job()
		{
		}
	}
}