using System.Linq;
using System.Runtime.InteropServices;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("B1B86134-D9FD-48F9-AA05-F46817010A03")]
	public interface IRoamingOperatorList
	{
		ReadonlyList RoamingOperatorsList { get; }
	}

	[ComVisible(true)]
	[Guid("A5FABD58-BF70-4253-9E74-CBCFE3C7C9C4")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRoamingOperatorList))]
	public partial class RoamingOperatorList : SafeComObject, IRoamingOperatorList
	{
		public ReadonlyList RoamingOperatorsList => new ReadonlyList(RoamingOperators);

		public override string ToString()
		{
			return string.Join("\r\n", RoamingOperators.Select(o => o.ToString()).ToArray());
		}
	}

	[ComVisible(true)]
	[Guid("0B5BC0B1-D4E6-4632-960B-7ABB47FD919F")]
	public interface IRoamingOperator
	{
		string FnsId { get; }
		string Name { get; }
		bool IsActive { get; }
		ReadonlyList FeaturesList { get; }
	}

	[ComVisible(true)]
	[Guid("CD1042B8-014E-4916-8F01-8CA2396B928F")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IRoamingOperator))]
	public partial class RoamingOperator : SafeComObject, IRoamingOperator
	{
		public ReadonlyList FeaturesList => new ReadonlyList(Features);

		public override string ToString()
		{
			var features = string.Join("\r\n", Features.Select(o => o.ToString()).ToArray());
			return $"FnsId: {FnsId}, Name: {Name}, IsActive: {IsActive}, Features: {features}";
		}
	}

	[ComVisible(true)]
	[Guid("D8E3AF96-D485-4B11-A42E-3553065CD223")]
	public interface IOperatorFeature
	{
		string Name { get; }
		string Description { get; }
	}

	[ComVisible(true)]
	[Guid("686DBA55-F111-42A7-8703-2FEE14EAF082")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IOperatorFeature))]
	public partial class OperatorFeature : SafeComObject, IOperatorFeature
	{
		public override string ToString()
		{
			return $"Name: {Name}, Description: {Description}";
		}
	}
}
