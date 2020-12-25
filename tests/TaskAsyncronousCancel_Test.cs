using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class TaskAsyncronousCancel_Test
	{
		[TestCase(0, 5000)]
		[TestCase(1, 5000)]
		[TestCase(1000, 5000)]
		[TestCase(10000, 5000)]
		[TestCase(15000, 5000)]
		public async Task ShouldThrowOperationCanceledExceptionWhenIsCancellationRequested(int ms, int delay)
		{
			var cts = new CancellationTokenSource();
			cts.CancelAfter(TimeSpan.FromMilliseconds(ms));
			
			var mock = Substitute.For<IDiadocApi>();

			mock.AuthenticateAsync(null, null, ct: cts.Token)
				.ReturnsForAnyArgs(x => string.Empty)
				.AndDoes(x =>
				{
					Thread.Sleep(delay);
					var ct = x.Arg<CancellationToken>();
					if (ct.IsCancellationRequested)
					{
						throw new OperationCanceledException();
					}
				});
			
			if (ms > delay)
			{
				Assert.AreEqual(string.Empty, await mock.AuthenticateAsync(null, null, ct: cts.Token));
			}
			else
			{
				Assert.ThrowsAsync<OperationCanceledException>(async () => await mock.AuthenticateAsync(null, null, ct: cts.Token));
			}
		}
		
		[TestCase(0, 5000)]
		[TestCase(1, 5000)]
		[TestCase(1000, 5000)]
		[TestCase(10000, 5000)]
		[TestCase(15000, 5000)]
		public async Task ShouldThrowTaskCanceledExceptionWhenTokenPassedToNestedMethod(int ms, int delay)
		{
			var cts = new CancellationTokenSource();
			cts.CancelAfter(TimeSpan.FromMilliseconds(ms));
			
			var mock = Substitute.For<IDiadocApi>();

			var delayedResult = Task.Delay(delay, cts.Token).ContinueWith(_ => string.Empty, cts.Token);
			mock.AuthenticateAsync(null, null, ct: cts.Token).Returns(delayedResult);
			
			if (ms > delay)
			{
				Assert.AreEqual(string.Empty, await mock.AuthenticateAsync(null, null, ct: cts.Token));
			}
			else
			{
				Assert.ThrowsAsync<TaskCanceledException>(async () => await mock.AuthenticateAsync(null, null, ct: cts.Token));
			}
		}
	}
}