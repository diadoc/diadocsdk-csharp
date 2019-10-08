using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class NullifyEmptyStringPropertiesSerialization_Test
	{
		[Test]
		public void Test()
		{
			var actual = new NullifyPropertiesTestData
			{
				PublicProperty = "",
				ObjectProperty = new object(),
				EnumProperty = TestEnum.One,
				ComplexProperty = new NullifyPropertiesTestData
				{
					PublicProperty = "",
				},
				ComplexPropertyArray = new[]
				{
					new NullifyPropertiesTestData
					{
						PublicProperty = "",
						BoolProperty = true
					},
					new NullifyPropertiesTestData
					{
						PublicProperty = "",
					},
					new NullifyPropertiesTestData
					{
						PublicProperty = "123",
					}
				}
			};

			var expected = new NullifyPropertiesTestData
			{
				PublicProperty = null,
				ObjectProperty = new object(),
				EnumProperty = TestEnum.One,
				ComplexProperty = new NullifyPropertiesTestData
				{
					PublicProperty = null,
				},
				ComplexPropertyArray = new[]
				{
					new NullifyPropertiesTestData
					{
						PublicProperty = null,
						BoolProperty = true,
					},
					new NullifyPropertiesTestData
					{
						PublicProperty = null,
					},
					new NullifyPropertiesTestData
					{
						PublicProperty = "123",
					}
				}
			};

			var actualXml = Encoding.UTF8.GetString(actual.NullifyEmptyStringPropertiesAndSerializeToXml());
			var expectedXml = Encoding.UTF8.GetString(expected.NullifyEmptyStringPropertiesAndSerializeToXml());
			Assert.That(actualXml, Is.EqualTo(expectedXml));
		}
	}


	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
	public class NullifyPropertiesTestData
	{
		public string PublicProperty { get; set; }
		public bool BoolProperty { get; set; }
		public object ObjectProperty { get; set; }
		public TestEnum EnumProperty { get; set; }
		public NullifyPropertiesTestData ComplexProperty { get; set; }
		public NullifyPropertiesTestData[] ComplexPropertyArray { get; set; }
	}

	public enum TestEnum
	{
		One,
	}
}