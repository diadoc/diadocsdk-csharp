using System.Linq;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("6430CCC7-5640-491C-8D59-86E697281D76")]
	public interface IOrganizationList
	{
		ReadonlyList OrganizationsList { get; }
	}

	[ComVisible(true)]
	[Guid("556FE194-58D7-4798-A9E9-D2127834723A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationList))]
	public partial class OrganizationList : SafeComObject, IOrganizationList
	{
		public ReadonlyList OrganizationsList
		{
			get { return new ReadonlyList(Organizations); }
		}

		public override string ToString()
		{
			return string.Join("\r\n", Organizations.Select(o => o.ToString()).ToArray());
		}
	}

	[ComVisible(true)]
	[Guid("88AAEC73-E8F8-4656-892C-39603A1ACEBF")]
	public interface IOrganization
	{
		string OrgId { get; }
		string Inn { get; }
		string Kpp { get; }
		string FullName { get; }
		string ShortName { get; }
		string Ogrn { get; }
		string FnsParticipantId { get; }
		string FnsRegistrationDate { get; }
		Address Address { get; }
		ReadonlyList BoxesList { get; }
		ReadonlyList DepartmentsList { get; }
		bool IsPilot { get; }
		bool IsActive { get; }
		bool IsTest { get; }
		bool IsRoaming { get; }
		string LiquidationDate { get; }
    }

	[ComVisible(true)]
	[Guid("CD9F454E-4A6A-4DE3-8B99-F9E89AE36F0F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganization))]
	public partial class Organization : SafeComObject, IOrganization
	{
		public ReadonlyList BoxesList
		{
			get { return new ReadonlyList(Boxes); }
		}

		public ReadonlyList DepartmentsList
		{
			get { return new ReadonlyList(Departments); }
		}


		public override string ToString()
		{
			var boxes = string.Join("\r\n", Boxes.Select(b => b.ToString()).ToArray());
			return string.Format("OrgId: {0}, Inn: {1}, Kpp: {2}, FullName: {3}, ShortName: {4}, Boxes:\r\n {5}, " +
								"Ogrn: {6}, FnsParticipantId: {7}, Address: {8}, FnsRegistrationDate: {9}, " +
								"Departments: {10}, IfnsCode: {11}, IsPilot: {12}, IsActive: {13}, IsTest: {14}, " +
								"IsBranch: {15}, IsRoaming: {16}, IsEmployee: {17}, InvitationCount: {18}, " +
								"SearchCount: {19}, Sociability: {20}, LiquidationDate: {21}, CertificateOfRegistryInfo: {22}",
								OrgId, Inn, Kpp, FullName, ShortName, boxes, Ogrn, FnsParticipantId, Address, FnsRegistrationDate,
								Departments, IfnsCode, IsPilot, IsActive, IsTest, IsBranch, IsRoaming, IsEmployee, InvitationCount,
								SearchCount, Sociability, LiquidationDate, CertificateOfRegistryInfo);
		}
	}

	[ComVisible(true)]
	[Guid("83F8EEB9-7974-46C1-AEE0-0257AADE1D5B")]
	public interface IBox
	{
		string BoxId { get; }
		string Title { get; }
		Organization Organization { get; }
		Com.OrganizationInvoiceFormatVersion InvoiceFormatVersionValue { get; }
		bool EncryptedDocumentsAllowed { get; }
	}

	[ComVisible(true)]
	[Guid("FCA452E2-AE05-424E-839C-A44A50F24836")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IBox))]
	public partial class Box : SafeComObject, IBox
	{
		public Com.OrganizationInvoiceFormatVersion InvoiceFormatVersionValue
		{
			get { return (Com.OrganizationInvoiceFormatVersion) InvoiceFormatVersion; }
			set { InvoiceFormatVersion = (OrganizationInvoiceFormatVersion) value; }
		}
		
		public override string ToString()
		{
			return string.Format("BoxId: {0}, Title: {1}, Organization: {2}, " +
								 "InvoiceFormatVersion: {3}, EncryptedDocumentsAllowed: {4}", 
								 BoxId, Title, Organization, InvoiceFormatVersion, EncryptedDocumentsAllowed);
		}
	}

	[ComVisible(true)]
	[Guid("80060ABA-5B61-40DC-B009-30BA240CA37B")]
	public interface IDepartment
	{
		string DepartmentId { get; }
		string ParentDepartmentId { get; }
		string Name { get; }
		string Abbreviation { get; }
		string Kpp { get; }
		Address Address { get; }
	}

	[ComVisible(true)]
	[Guid("93130771-8E24-4203-AD34-49C624F5DC98")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDepartment))]
	public partial class Department : SafeComObject, IDepartment
	{
		public override string ToString()
		{
			return string.Format("DepartmentId: {0}, Name: {1}, ParentDepartmentId: {2}, " +
			                     "Abbreviation: {3}, Kpp: {4}, Address: {5}, IsDisabled: {6}", 
								 DepartmentId, Name, ParentDepartmentId, Abbreviation, Kpp, Address, IsDisabled);
		}
	}
}