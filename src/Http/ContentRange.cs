using System;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public sealed class ContentRange
	{
		public ContentRange([NotNull] Range range, long length)
			: this(range, (long?)length)
		{
			if (range == null)
				throw new ArgumentNullException("range");
		}

		public ContentRange([NotNull] Range range)
			: this(range, null)
		{
			if (range == null)
				throw new ArgumentNullException("range");
		}

		public ContentRange(long length)
			: this(null, (long?)length)
		{}

		private ContentRange([CanBeNull] Range range, long? length)
		{
			Range = range;
			Length = length;
		}

		[CanBeNull]
		public Range Range { get; private set; }

		public long? Length { get; private set; }

		public override string ToString()
		{
			return string.Format("Range: {0}, Length: {1}", Range, Length);
		}
	}
}