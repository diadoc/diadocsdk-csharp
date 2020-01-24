using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class ComClasses_Test
	{
		private readonly Assembly diadocApiAssembly = typeof(ComDiadocApi).Assembly;

		[Test]
		public void EachClassInheritedFromComVisibleInterface_MustBeComVisible()
		{
			var comInvisibleClassesWithComVisibleInterface = diadocApiAssembly.GetTypes()
				.Where(x => x.IsClass && !IsComVisible(x) && FindComVisibleInterfaces(x).Any())
				.ToArray();

			foreach (var type in comInvisibleClassesWithComVisibleInterface)
				Console.WriteLine(type.Name);

			Assert.IsFalse(comInvisibleClassesWithComVisibleInterface.Any());
		}

		[Test]
		public void ComDefaultInterfaceAttribute_MustPointToSingleInterface()
		{
			var comClasses = GetComClasses();
			foreach (var comClass in comClasses)
			{
				var implementedInterface = FindComVisibleInterfaces(comClass).FirstOrDefault();
				Assert.IsNotNull(implementedInterface, "Failed to get single com visible interface for class: {0}", comClass);
				Console.Out.WriteLine("{0} implements {1}", comClass.Name, implementedInterface.Name);
				var comDefaultInterface = TryGetComDefaultInterface(comClass);
				Assert.That(implementedInterface, Is.EqualTo(comDefaultInterface));
			}
		}

		[Test]
		public void ClassInterfaceAttribute_MustBeSetToNone()
		{
			var comClasses = GetComClasses();
			foreach (var comClass in comClasses)
			{
				var classInterface = FindAttributes<ClassInterfaceAttribute>(comClass).FirstOrDefault();
				Assert.IsNotNull(classInterface, "ClassInterface attribute is not set for class: {0}", comClass);
				Assert.That(classInterface.Value, Is.EqualTo(ClassInterfaceType.None));
			}
		}

		[Test]
		public void GuidAttributes_MustBeAllDistinct()
		{
			var typeGuids = diadocApiAssembly.GetTypes()
				.Select(x => new {Guid = TryGetGuid(x), Type = x})
				.Where(g => g.Guid.HasValue)
				.ToList();
			var guidTypeStrings = string.Join("\n", typeGuids
				.GroupBy(x => x.Guid)
				.Where(x => x.Count() > 1)
				.SelectMany(x => x)
				.Select(x => $"{x.Guid} for {x.Type.Name}")
				.ToArray());
			Console.WriteLine(guidTypeStrings);
			Assert.That(typeGuids.Select(x => x.Guid).Distinct().Count(), Is.EqualTo(typeGuids.Count));
		}

		[Test]
		public void ProgIdAttributes_MustBeAllDistinct()
		{
			var typeProgIds = diadocApiAssembly.GetTypes()
				.Select(x => new {ProgId = FindProgId(x), Type = x})
				.Where(g => g.ProgId != null)
				.ToList();
			var guidTypeStrings = string.Join("\n", typeProgIds
				.GroupBy(x => x.ProgId)
				.Where(x => x.Count() > 1)
				.SelectMany(x => x)
				.Select(x => $"{x.ProgId} for {x.Type.Name}")
				.ToArray());
			Console.WriteLine(guidTypeStrings);
			Assert.That(typeProgIds.Select(x => x.ProgId).Distinct().Count(), Is.EqualTo(typeProgIds.Count));
		}

		[Test]
		public void ComVisibleInterfaces_DoNotReturnArrayList()
		{
			var comInterfaces = diadocApiAssembly.GetTypes()
				.Where(x => x.IsInterface && IsComVisible(x))
				.ToArray();

			var hasErrors = false;
			foreach (var comInterface in comInterfaces)
			{
				var wrongMembers = comInterface.GetMembers()
					.Where(x => IsMethodReturnsType(x, typeof(ArrayList)))
					.ToArray();
				foreach (var wrongMember in wrongMembers)
					Console.WriteLine("{0}.{1}", comInterface.Name, wrongMember.Name);
				if (wrongMembers.Any())
					hasErrors = true;
			}

			Assert.IsFalse(hasErrors);
		}

		private bool IsMethodReturnsType(MemberInfo member, Type returnType)
		{
			if (member is MethodInfo)
				return ((MethodInfo) member).ReturnType == returnType;
			if (member is PropertyInfo)
				return ((PropertyInfo) member).PropertyType == returnType;
			throw new InvalidOperationException();
		}

		private IEnumerable<Type> GetComClasses()
		{
			var comClasses = diadocApiAssembly.GetTypes()
				.Where(x => x.IsClass && TryGetComDefaultInterface(x) != null)
				.ToArray();
			return comClasses;
		}

		private Type[] FindComVisibleInterfaces(Type type)
		{
			return type.GetInterfaces()
				.Where(x => IsComVisible(x) && x.Assembly == diadocApiAssembly)
				.ToArray();
		}

		private static bool IsComVisible(Type type)
		{
			return FindAttributes<ComVisibleAttribute>(type).Any(x => x.Value);
		}

		private static string FindProgId(Type type)
		{
			var progIdAttribute = FindAttributes<ProgIdAttribute>(type).FirstOrDefault();
			return progIdAttribute?.Value;
		}

		private static Type TryGetComDefaultInterface(Type type)
		{
			var attribute = FindAttributes<ComDefaultInterfaceAttribute>(type).FirstOrDefault();
			return attribute?.Value;
		}

		private static Guid? TryGetGuid(Type type)
		{
			var attribute = FindAttributes<GuidAttribute>(type).FirstOrDefault();
			return attribute == null ? (Guid?) null : new Guid(attribute.Value);
		}

		private static T[] FindAttributes<T>(Type type)
		{
			return type.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();
		}
	}
}