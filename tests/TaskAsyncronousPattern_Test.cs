using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;
using ProtoBuf;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class TaskAsyncronousPattern_Test
	{
		[TestCaseSource("GetAllTapMethods")]
		public void AllTapMethodsMustHaveAsyncInName(MethodInfo method)
		{
			Assert.That(method.Name, Is.StringEnding("Async"), method.ToString());
		}
		
		[TestCaseSource("GetAllTapMethods")]
		public void AllTapMethodsMustReturnTask(MethodInfo method)
		{
			Assert.IsTrue((method.ReturnType == typeof(Task)) || method.ReturnType.IsSubclassOf(typeof(Task)), method.ToString());
		}

		private static IEnumerable<MethodInfo> GetAllTapMethods()
		{
			var diadocApiTypes = typeof(IDiadocApi).Assembly.GetExportedTypes();
			var result = diadocApiTypes.SelectMany(x => x.GetMethods()
				.Where(y => y.ReturnType.IsSubclassOf(typeof(Task))
					|| y.ReturnType == typeof(Task)
					|| y.Name.EndsWith("Async", true, CultureInfo.InvariantCulture)));
			return result;
		}
	}
}