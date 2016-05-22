using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;

namespace Diadoc.Console
{
	public class ConsoleContext
	{
		public ICrypt Crypt { get; set; }
		public DiadocApi DiadocApi { get; set; }
		public X509Certificate2 CurrentCert { get; set; }
		public string CurrentToken { get; set; }
		public IList<Box> Boxes { get; set; }
		public BoxEventList Events { get; set; }
		public IList<Organization> Orgs { get; set; }
		public string CurrentBoxId { get; set; }
		public string CurrentOrgId { get; set; }
		public bool SignByAttorney { get; set; }

		public bool IsAuthenticated()
		{
			return CurrentToken != null;
		}

		public void ClearAuthenticationContext()
		{
			Events = null;
			Boxes = null;
			Orgs = null;
		}
	}
}
