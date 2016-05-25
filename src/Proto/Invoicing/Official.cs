using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Invoicing
{
	[ComVisible(true)]
	[Guid("1502462E-980B-45B6-8087-2C3A536420D1")]
	public interface IOfficial
	{
		string Surname { get; set; }
		string FirstName { get; set; }
		string Patronymic { get; set; }
		string JobTitle { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Official")]
	[Guid("8A530471-EEF0-4928-B80D-2E291F5C6443")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOfficial))]
	public partial class Official : SafeComObject, IOfficial
	{
	}

	[ComVisible(true)]
	[Guid("C875849E-3550-49EF-8CC7-993FC0F919FC")]
	public interface IAttorney
	{
		string Number { get; set; }
		string Date { get; set; }
		string IssuerOrganizationName { get; set; }
		Official IssuerPerson { get; set; }
		string IssuerAdditionalInfo { get; set; }
		Official RecipientPerson { get; set; }
		string RecipientAdditionalInfo { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.Attorney")]
	[Guid("53352E29-BB6B-4AF9-A40C-4AD556534C34")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IAttorney))]
	public partial class Attorney : SafeComObject, IAttorney
	{
	}
}
