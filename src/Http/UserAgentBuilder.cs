using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	internal static class UserAgentBuilder
	{
		private static readonly string SdkVersion;
		private static readonly string NetFxVersion;

		static UserAgentBuilder()
		{
			SdkVersion = typeof(HttpClient).Assembly.GetName().Version.ToString();
			NetFxVersion = Environment.Version.ToString();
			try
			{
				SdkVersion = FileVersionInfo.GetVersionInfo(typeof(HttpClient).Assembly.Location).FileVersion;
			}
			catch
			{
			}

			try
			{
				NetFxVersion = FileVersionInfo.GetVersionInfo(typeof(int).Assembly.Location).FileVersion;
			}
			catch
			{
			}
		}
		
		public static string Build([NotNull] string sdkName)
		{
			if (sdkName == null)
				throw new ArgumentNullException(nameof(sdkName));
			return string.Format("Diadoc {0} SDK={1};OS={2};NETFX={3}",
				sdkName,
				SdkVersion,
				Environment.OSVersion.VersionString,
				NetFxVersion);
		}
	}
}
