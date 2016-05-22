using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("EA3394D7-D56B-433D-86D1-9C974F6FDAF0")]
	public interface IOrganizationUser
	{
		string Id { get; }
		string Name { get; }
		IOrganizationUserPermissions UserPermissions { get; }
	}

	[ComVisible(true)]
	[Guid("0DF041CC-FD23-4A60-B8A4-541C3853FA0D")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOrganizationUser))]
	public partial class OrganizationUser : SafeComObject, IOrganizationUser
	{
		public IOrganizationUserPermissions UserPermissions
		{
			get { return Permissions; }
		}

		public override string ToString()
		{
			return string.Format("Id: {0}, Name: {1}, Permissions: {2}", Id, Name, Permissions);
		}
	}
}