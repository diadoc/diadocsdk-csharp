using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto.Users
{
	[ComVisible(true)]
	[Guid("90688E07-B2B0-4423-87D2-52F27D231D25")]
	public interface IUserToUpdate
	{
		UserLoginPatch Login { get; set; }
		UserFullNamePatch FullName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UserToUpdate")]
	[Guid("7C44B38B-E8FF-4E56-91F5-DBDC2ECC6A37")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUserToUpdate))]
	public partial class UserToUpdate : SafeComObject, IUserToUpdate
	{
	}

	[ComVisible(true)]
	[Guid("62B9735D-E1FA-4A0E-8BF8-86031B52F3CA")]
	public interface IUserLoginPatch
	{
		string Login { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UserLoginPatch")]
	[Guid("1D89A3C1-C44D-46C8-A255-1B9CB7604538")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUserLoginPatch))]
	public partial class UserLoginPatch : SafeComObject, IUserLoginPatch
	{
	}

	[ComVisible(true)]
	[Guid("47E724F1-E794-4073-B40D-E7663FBD93EF")]
	public interface IUserFullNamePatch
	{
		FullName FullName { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.UserFullNamePatch")]
	[Guid("619A4FF0-49F3-4448-A966-343C0F29FDB5")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IUserFullNamePatch))]
	public partial class UserFullNamePatch : SafeComObject, IUserFullNamePatch
	{
	}
}