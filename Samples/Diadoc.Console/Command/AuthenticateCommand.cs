using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Diadoc.Console.Command
{
	public class AuthenticateCommand : ConsoleCommandBase
	{
		public AuthenticateCommand(ConsoleContext consoleContext, string command = "auth") : base(consoleContext, command, CommandType.AuthenticationCommand)
		{
			Usage = "[ <certificate_thumbprint> ]";
			Description = "Authenticate with certificate";
		}

		protected override void PerformRunCommand(string[] args)
		{
			var thumbprint = args.Length > 0 ? args[0] : null;

			if (thumbprint == null)
			{
				ShowCertificates();
			}
			else
			{
				ConsoleContext.CurrentCert = FindCertificate(thumbprint);
				if (ConsoleContext.CurrentCert != null)
				{
					ConsoleContext.CurrentToken = ConsoleContext.DiadocApi.Authenticate(ConsoleContext.CurrentCert.RawData);
					ConsoleContext.ClearAuthenticationContext();
					System.Console.WriteLine("Аутентификация пройдена. Сертификат: " + CertificateToString(ConsoleContext.CurrentCert));
					OutputHelpers.ShowBoxes(ConsoleContext);
				}
				else
				{
					System.Console.WriteLine("Аутентификация не пройдена: не найден сертификат");
				}
			}
		}

		private static void ShowCertificates()
		{
			System.Console.WriteLine();
			System.Console.WriteLine("В личном хранилище найдены следующие сертификаты: ");
			foreach (X509Certificate2 cert in GetCertificatesFromPersonalStore())
				System.Console.WriteLine(cert.Thumbprint + "\t" + CertificateToString(cert));
			System.Console.WriteLine();
		}

		private static X509Certificate2 FindCertificate(string thumbprint)
		{
			List<X509Certificate2> certs = FindCertificates(thumbprint);
			if (certs.Count == 0)
			{
				System.Console.WriteLine("В хранилище личных сертификатов не найдено ни одного сертификата с отпечатком " + thumbprint);
				return null;
			}
			if (certs.Count > 1)
			{
				System.Console.WriteLine("Найдено несколько подходящих сертификатов:");
				foreach (X509Certificate2 cert in certs)
					System.Console.WriteLine(cert.Thumbprint + "\t" + cert.FriendlyName);
				return null;
			}
			return certs[0];
		}

		private static List<X509Certificate2> FindCertificates(string thumbprint)
		{
			return GetCertificatesFromPersonalStore()
				.Where(c => c.Thumbprint != null && c.Thumbprint.StartsWith(thumbprint, StringComparison.InvariantCultureIgnoreCase)).ToList();
		}

		private static string CertificateToString(X509Certificate2 certificate)
		{
			string subject = certificate.Subject;
			int cnStartIndex = subject.IndexOf("CN=");
			if (cnStartIndex == -1) return subject;
			string commonName = subject.Substring(cnStartIndex + 3);
			int commaIndex = commonName.IndexOfAny(new[] { ',', ';' });
			if (commaIndex == -1) return commonName;
			return commonName.Substring(0, commaIndex);
		}


		private static IEnumerable<X509Certificate2> GetCertificatesFromPersonalStore()
		{
			var s = new X509Store();
			s.Open(OpenFlags.ReadOnly);
			try
			{
				return s.Certificates.Cast<X509Certificate2>().Where(c => c.HasPrivateKey).ToList();
			}
			finally
			{
				s.Close();
			}
		}
	}

}
