using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class TaskAsyncronousPattern_Test
	{
		[TestCaseSource(nameof(GetAllTapMethods))]
		public void AllTapMethodsMustHaveAsyncInName(MethodInfo method)
		{
			Assert.That(method.Name, Does.EndWith("Async"), method.ToString());
		}

		[TestCaseSource(nameof(GetAllTapMethods))]
		public void AllTapMethodsMustReturnTask(MethodInfo method)
		{
			Assert.IsTrue((method.ReturnType == typeof(Task)) || method.ReturnType.IsSubclassOf(typeof(Task)), method.ToString());
		}

		[TestCaseSource(nameof(GetAllSynchronousApiMethods))]
		public void AllSynchronousApiMethodsShouldHaveAsyncCounterpartWithSameSignature(MethodInfo method)
		{
			var methodParameters = method.GetParameters().Select(x => x.ParameterType).ToArray();
			var methodName = method.Name;
			var asyncMethodCounterpart = method.ReflectedType?.GetMethod(methodName + "Async", methodParameters);
			Assert.NotNull(asyncMethodCounterpart, $"Type: {method.ReflectedType}, Method: {method} has no async counterpart");
			if (method.ReturnType == typeof(void))
				Assert.That(asyncMethodCounterpart.ReturnType, Is.EqualTo(typeof(Task)), $"Type: {method.ReflectedType}, Method: {method} async counterpart has wrong return type");
			else if (method.ReturnType.IsGenericParameter)
				Assert.IsTrue(asyncMethodCounterpart.ReturnType.GenericTypeArguments.First().IsGenericParameter, $"Type: {method.ReflectedType}, Method: {method} async counterpart has wrong return type");
			else
				Assert.That(asyncMethodCounterpart.ReturnType, Is.EqualTo(typeof(Task<>).MakeGenericType(method.ReturnType)), $"Type: {method.ReflectedType}, Method: {method} async counterpart has wrong return type");
		}

		[TestCaseSource(nameof(GetAllAsynchronousApiMethods))]
		public void AllAsynchronousApiMethodsShouldHaveSyncCounterpartWithSameSignature(MethodInfo method)
		{
			var methodParameters = method.GetParameters().Select(x => x.ParameterType).ToArray();
			var methodNameWithoutAsync = method.Name.Substring(0, method.Name.Length - "Async".Length);
			var syncMethodCounterpart = method.ReflectedType?.GetMethod(methodNameWithoutAsync, methodParameters);
			Assert.NotNull(syncMethodCounterpart, $"Type: {method.ReflectedType}, Method: {method} has no synchronous counterpart");
			if (method.ReturnType == typeof(Task) || method.ReturnType == typeof(void))
				Assert.That(syncMethodCounterpart.ReturnType, Is.EqualTo(typeof(void)), $"Type: {method.ReflectedType}, Method: {method} sync counterpart has wrong return type");
			else if (method.ReturnType.GenericTypeArguments.First().IsGenericParameter)
				Assert.IsTrue(syncMethodCounterpart.ReturnType.IsGenericParameter, $"Type: {method.ReflectedType}, Method: {method} async counterpart has wrong return type");
			else
				Assert.That(syncMethodCounterpart.ReturnType, Is.EqualTo(method.ReturnType.GenericTypeArguments.First()), $"Type: {method.ReflectedType}, Method: {method} async counterpart has wrong return type");
		}

		[TestCaseSource(nameof(GetAllDiadocHttpApiMethods))]
		public void AllDiadocHttpApiMethodsShouldHaveCounterpartsInDiadocApi(MethodInfo method)
		{
			var counterpart = typeof(DiadocApi).GetMethod(method.Name, method.GetParameters().Select(x => x.ParameterType).ToArray());
			Assert.NotNull(counterpart, method + " have no counterpart in DiadocApi class");
			Assert.That(counterpart.ReturnType, Is.EqualTo(method.ReturnType));
		}

		[TestCaseSource(nameof(GetAllDiadocHttpApiMethods))]
		public void AllDiadocHttpApiMethodsShouldHaveCounterpartsInIDiadocApi(MethodInfo method)
		{
			var counterpart = typeof(IDiadocApi).GetMethod(method.Name, method.GetParameters().Select(x => x.ParameterType).ToArray());
			Assert.NotNull(counterpart, method + " have no counterpart in IDiadocApi interface");
			Assert.That(counterpart.ReturnType, Is.EqualTo(method.ReturnType));
		}

		private static IEnumerable<MethodInfo> GetAllTapMethods()
		{
			var diadocApiTypes = typeof(IDiadocApi).Assembly.GetExportedTypes();
			var result = diadocApiTypes
				.SelectMany(x => x.GetMethods()
					.Where(y => y.ReturnType.IsSubclassOf(typeof(Task))
								|| y.ReturnType == typeof(Task)
								|| y.Name.EndsWith("Async", true, CultureInfo.InvariantCulture)));
			return result;
		}

		private static IEnumerable<MethodInfo> GetAllSynchronousApiMethods()
		{
			return GetAllApiMethods().Where(x => !x.Name.EndsWith("Async")).Where(x => !x.Name.StartsWith("WaitTaskResult"));
		}

		private static IEnumerable<MethodInfo> GetAllAsynchronousApiMethods()
		{
			return GetAllApiMethods().Where(x => x.Name.EndsWith("Async")).Where(x => !x.Name.StartsWith("WaitTaskResult"));
		}

		private static IEnumerable<MethodInfo> GetAllApiMethods()
		{
			var apiTypes = new[]
			{
				typeof(IDiadocApi),
				typeof(DiadocApi),
				typeof(DiadocHttpApi),
				typeof(IDocflowApi),
				typeof(DocflowApi),
				typeof(DiadocHttpApi.DocflowHttpApi)
			};
			return apiTypes.SelectMany(GetAllApiMethodsForType);
		}

		private static IEnumerable<MethodInfo> GetAllDiadocHttpApiMethods()
		{
			return GetAllApiMethodsForType(typeof(DiadocHttpApi)).Where(x => !x.Name.StartsWith("WaitTaskResult"));
		}

		private static readonly string[] ExceptMethods =
		{
			"SetProxyUri",
			"EnableSystemProxyUsage",
			"DisableSystemProxyUsage",
			"SetProxyCredentials",
			"SetSolutionInfo",
			"UseOidc"
		};

		private static readonly string[] ObjectMethods = typeof(object).GetMethods().Select(x => x.Name).ToArray();

		private static IEnumerable<MethodInfo> GetAllApiMethodsForType(Type type)
		{
			return type.GetMethods()
				.Where(x => !x.Name.StartsWith("get_") && !x.Name.StartsWith("set_"))
				.Where(x => !ExceptMethods.Contains(x.Name))
				.Where(x => !ObjectMethods.Contains(x.Name));
		}
	}
}
