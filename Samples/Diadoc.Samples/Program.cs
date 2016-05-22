using System;

namespace Diadoc.Samples
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				SendNonformalizedSample.PostNonformalized();
				SendLargeNonformalizedSample.PostLargeNonformalized();
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine("An error has occurred during Diadoc sample application execution");
				Console.Out.WriteLine(ex.Message);
				Console.Out.WriteLine(ex.StackTrace);
			}
			Console.Out.WriteLine("Press Enter to exit the application...");
			Console.In.ReadLine();
		}
	}
}
