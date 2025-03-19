﻿using System;

namespace Diadoc.Samples
{
	internal static class Program
	{
		private static void Main()
		{
			try
			{
				Authenticate.RunSample();
				PostNonformalizedDocument.RunSample();
				PostLargeDocumentWithShelf.RunSample();
				PostUniversalTransferDocument820.RunSample();
				PostUniversalTransferDocument970.RunSample();
				PostUniversalCorrectionDocument736.RunSample();
				PatchDocumentWithReceipt.RunSample();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Произошла ошибка при использовании API Диадока:");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}

			Console.WriteLine("Нажмите любую клавишу, чтобы выйти из приложения");
			Console.ReadKey();
		}
	}
}
