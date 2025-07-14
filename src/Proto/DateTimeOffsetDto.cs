using System;

namespace Diadoc.Api.Proto
{
	public partial class DateTimeOffsetDto
	{
		public DateTimeOffset ToDateTimeOffset()
		{
			return new DateTimeOffset(UtcTicks, TimeSpan.Zero).ToOffset(TimeSpan.FromTicks(OffsetTicks));
		}

		public static explicit operator DateTimeOffset(DateTimeOffsetDto value) => value.ToDateTimeOffset();

		public static explicit operator DateTimeOffsetDto(DateTimeOffset value)
			=> new DateTimeOffsetDto
			{
				UtcTicks = value.UtcTicks,
				OffsetTicks = value.Offset.Ticks
			};
	}
}
