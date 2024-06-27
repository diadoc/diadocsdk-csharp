using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Diadoc.Api.Tests
{
	public static class ApiMethodsHelper
	{
		private static readonly HashSet<string> ObjectMethods = new HashSet<string>(typeof(object).GetMethods().Select(x => x.Name));

		public static IEnumerable<MethodInfo> GetAllApiMethodsForType(Type type, ICollection<string> exceptMethods)
		{
			return type.GetMethods()
				.Where(x => !x.Name.StartsWith("get_") && !x.Name.StartsWith("set_"))
				.Where(x => !exceptMethods.Contains(x.Name))
				.Where(x => !ObjectMethods.Contains(x.Name));
		}
	}
}
