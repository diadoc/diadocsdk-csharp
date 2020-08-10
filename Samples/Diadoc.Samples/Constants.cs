namespace Diadoc.Samples
{
	internal static class Constants
	{
		// URL веб-сервиса Диадок
		internal const string DefaultApiUrl = "https://diadoc-api.kontur.ru";

		// Идентификатор клиента, он же ключ разработчика
		internal const string DefaultClientId = "<Вставьте ключ>";

		// Логин для авторизации на сервере Диадок
		internal const string DefaultLogin = "<Вставьте логин>";

		// Пароль для авторизации на сервере Диадок
		internal const string DefaultPassword = "<Вставьте пароль>";

		// Подставьте сюда идентификатор вашего ящика (отправителя), из которого будете отправлять документ.
		// Допустимы форматы как в GUID (12345675-1234-1234-1234-123456789012),
		// так и в формате вида 12345675123412341234123456789012@diadoc.ru
		internal const string DefaultFromBoxId = "<Идентификатор отправителя>";

		// Подставьте сюда идентификатор ящика-получателя, в который будете отправлять документ.
		internal const string DefaultToBoxId = "<Идентификатор получателя>";

		// Подставьте сюда путь до публичной части сертификата, которым будет подписан документ (пример: C:\public.cer)
		// Важно, чтобы приватная часть ключа была установлена в машину, на которой будет выполняться этот код
		internal const string CertificatePath = @"<Путь до сертификата>";
	}
}