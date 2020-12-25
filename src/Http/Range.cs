using System;

namespace Diadoc.Api.Http
{
	public sealed class Range
	{
		public Range(int from, int to)
		{
			if (from < 0)
			{
				throw new ArgumentOutOfRangeException("from", @from, "from cannot be less than 0");
			}

			if (to < 0)
			{
				throw new ArgumentOutOfRangeException("to", @from, "to cannot be less than 0");
			}

			if (from > to)
			{
				throw new ArgumentOutOfRangeException("to", to, "to cannot be less than from");
			}

			From = from;
			To = to;
		}

		public int From { get; private set; }

		public int To { get; private set; }

		public override string ToString()
		{
			return string.Format("From: {0}, To: {1}", From, To);
		}
	}
}
