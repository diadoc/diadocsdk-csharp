using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api
{
	[ComVisible(true)]
	[Guid("40EE5B76-5D17-424F-9AAD-7FC3334921B8")]
	public interface IDocumentsFilter
	{
		string BoxId { get; set; }
		string FilterCategory { get; set; }
		string CounteragentBoxId { get; set; }
		string FromDocumentDate { get; set; }
		string ToDocumentDate { get; set; }
		string DepartmentId { get; set; }
		string DocumentNumber { get; set; }
		string ToDepartmentId { get; set; }
		bool ExcludeSubdepartments { get; set; }
		string SortDirection { get; set; }
		string AfterIndexKey { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentsFilter")]
	[Guid("FEFDFA6F-E393-4D6E-B29B-6B35C0DEB1DA")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentsFilter))]
	public class DocumentsFilter : SafeComObject, IDocumentsFilter
	{
		public string BoxId { get; set; }
		public string FilterCategory { get; set; }
		public string CounteragentBoxId { get; set; }
		public DateTime? TimestampFrom { get; set; }
		public DateTime? TimestampTo { get; set; }
		public string FromDocumentDate { get; set; }
		public string ToDocumentDate { get; set; }
		public string DepartmentId { get; set; }
		public string DocumentNumber { get; set; }
		public string ToDepartmentId { get; set; }
		public bool ExcludeSubdepartments { get; set; }
		public string SortDirection { get; set; }
		public string AfterIndexKey { get; set; }
	}
}