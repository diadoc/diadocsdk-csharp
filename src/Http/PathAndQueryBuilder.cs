using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public class PathAndQueryBuilder
	{
		private readonly string action;
		private readonly IDictionary<string, string> parameters = new Dictionary<string, string>();

		public PathAndQueryBuilder([NotNull] string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}

			this.action = action.StartsWith("/") ? action.Substring(1) : action;
		}

		public void AddParameter(string key, string value = null)
		{
			if (!string.IsNullOrEmpty(key))
			{
				parameters.Add(key, value);
			}
		}

		public void AddCommaSeparatedParameter(string key, params string[] values)
		{
			if (!string.IsNullOrEmpty(key) && values != null && values.Length != 0)
			{
				parameters.Add(key, string.Join(",", values));
			}
		}

		public string BuildPathAndQuery()
		{
			var sb = new StringBuilder("/" + action);
			if (!parameters.Any())
			{
				return sb.ToString();
			}

			sb.Append("?");
			foreach (var parameter in parameters)
			{
				sb.Append(Uri.EscapeDataString(parameter.Key));
				if (!string.IsNullOrEmpty(parameter.Value))
				{
					sb.AppendFormat("={0}", Uri.EscapeDataString(parameter.Value));
				}

				sb.Append("&");
			}

			return sb.ToString(0, sb.Length - 1);
		}

		public override string ToString()
		{
			return BuildPathAndQuery();
		}
	}
}
