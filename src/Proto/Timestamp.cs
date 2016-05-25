using System;

namespace Diadoc.Api.Proto
{
	public partial class Timestamp : IEquatable<Timestamp>
	{
		public Timestamp(long ticks)
		{
			Ticks = ticks;
		}

		public DateTime ToDateTime()
		{
			return new DateTime(Ticks, DateTimeKind.Utc);
		}

		public override string ToString()
		{
			return string.Format("Ticks: {0}, DateTime: {1}", Ticks, ToDateTime().ToString("u"));
		}

		public bool Equals(Timestamp other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Ticks == other.Ticks;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Timestamp) obj);
		}

		public override int GetHashCode()
		{
			return Ticks.GetHashCode();
		}
	}
}
