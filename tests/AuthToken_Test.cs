using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	public class AuthToken_Test
	{
		[Test]
		[TestCase(typeof(IDiadocApi))]
		[TestCase(typeof(DiadocHttpApi))]
		[TestCase(typeof(IDocflowApi))]
		[TestCase(typeof(DiadocHttpApi.DocflowHttpApi))]
		[TestCase(typeof(IComDiadocApi))]
		public void Every_api_method_has_overload_with_auth_token(Type clientType)
		{
			foreach (var methodInfo in ApiMethodsHelper.GetAllApiMethodsForType(clientType, ExceptMethods))
			{
				var parameters = methodInfo.GetParameters();
				if (parameters.Any(x => (x.Name == "token" || x.Name == "authToken") && x.ParameterType == typeof(string)))
				{
					continue;
				}

				var parametersWithToken = new List<Type> {typeof(string)};
				parametersWithToken.AddRange(parameters.Select(x => x.ParameterType));
				var counterpartWithToken = clientType.GetMethod(methodInfo.Name, parametersWithToken.ToArray());
				Assert.That(counterpartWithToken, Is.Not.Null, () => $"Method doesn't have overload with authToken: {clientType}.{methodInfo.Name}");
			}
		}

		[Test]
		[TestCase(typeof(IDiadocApi))]
		[TestCase(typeof(DiadocHttpApi))]
		[TestCase(typeof(IDocflowApi))]
		[TestCase(typeof(DiadocHttpApi.DocflowHttpApi))]
		[TestCase(typeof(IComDiadocApi))]
		public void Every_api_method_without_auth_token_is_marked_obsolete(Type clientType)
		{
			foreach (var methodInfo in ApiMethodsHelper.GetAllApiMethodsForType(clientType, ExceptMethods))
			{
				var parameters = methodInfo.GetParameters();
				if (parameters.Any(x => (x.Name == "token" || x.Name == "authToken") && x.ParameterType == typeof(string)))
				{
					continue;
				}

				var obsoleteAttribute = methodInfo.GetCustomAttributes(typeof(ObsoleteAttribute), true);
				Assert.That(obsoleteAttribute, Is.Not.Null);
			}
		}
		
		private static readonly HashSet<string> ExceptMethods = new HashSet<string>
		{
			"SetProxyUri",
			"EnableSystemProxyUsage",
			"DisableSystemProxyUsage",
			"SetProxyCredentials",
			"SetSolutionInfo",
			"UseOidc",
			"Initialize",
			"SetProxyCredentialsSecure",
			"CreateNewId",
			"NewGuid",
			"NullDateTime",
			"AuthenticateWithPassword",
			"AuthenticateWithCertificate",
			"AuthenticateWithSid",
			"AuthenticateAsync",
			"AuthenticateByKeyAsync",
			"AuthenticateBySidAsync",
			"AuthenticateWithKeyAsync",
			"Authenticate",
			"AuthenticateByKey",
			"AuthenticateBySid",
			"AuthenticateWithKey",
			"Recognize",
			"GetRecognized",
			"RecognizeAsync",
			"GetRecognizedAsync",
			"GetExternalServiceAuthInfo",
			"GetExternalServiceAuthInfoAsync"
		};
	}
}
