using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api
{
	[ComImport]
	[Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IObjectSafety
	{
		[PreserveSig]
		int GetInterfaceSafetyOptions(ref Guid riid, out int pdwSupportedOptions, out int pdwEnabledOptions);

		[PreserveSig]
		int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions);
	}

	public abstract class SafeComObject : IObjectSafety
	{
		int IObjectSafety.GetInterfaceSafetyOptions(ref Guid riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
		{
			pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
			pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
			return 0;
		}

		int IObjectSafety.SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
		{
			return 0;
		}

		private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
		private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
	}
}