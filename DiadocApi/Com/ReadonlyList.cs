using System.Collections;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("3ABC4C7E-598A-4833-846B-69D7FC1E302C")]
	public interface IReadonlyList
	{
		int Count { get; }

		[return: MarshalAs(UnmanagedType.IDispatch)]
		object Item(int zeroBasedIndex);
	}

	[ComVisible(true)]
	[Guid("56D2D74C-13DA-4CE3-B017-78ED75E0E519")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IReadonlyList))]
	public class ReadonlyList : SafeComObject, IReadonlyList
	{
		private readonly ArrayList arrayList;

		public ReadonlyList(ICollection source)
		{
			arrayList = new ArrayList(source);
		}

		public ReadonlyList(ArrayList arrayList)
		{
			this.arrayList = arrayList;
		}

		public int Count { get { return arrayList.Count; } }

		public object Item(int zeroBasedIndex)
		{
			return arrayList[zeroBasedIndex];
		}
	}
}