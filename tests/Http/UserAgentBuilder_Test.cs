using Diadoc.Api.Http;
using NUnit.Framework;

namespace Diadoc.Api.Tests.Http
{
	public class UserAgentBuilder_Test
	{
		[Test]
		public void Build_throws_if_user_agent_is_null()
		{
			Assert.That(() => UserAgentBuilder.Build(null), Throws.ArgumentNullException);
		}

		[Test]
		public void Build_works()
		{
			Assert.That(() => UserAgentBuilder.Build("example"), Does.StartWith("Diadoc example SDK"));
		}
	}
}
