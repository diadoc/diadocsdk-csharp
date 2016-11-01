using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public struct UserDomain
	{
		public string User { get; private set; }
		public string Domain { get; private set; }

		public static UserDomain Parse([NotNull] string user)
		{
			var domain = string.Empty;
			var idx = user.IndexOf('\\');
			if (idx >= 0)
			{
				domain = user.Substring(0, idx);
				user = user.Substring(idx + 1);
			}
			else if ((idx = user.IndexOf('@')) >= 0)
			{
				domain = user.Substring(idx + 1);
				user = user.Substring(0, idx);
			}
			return new UserDomain { Domain = domain, User = user };
		}
	}
}