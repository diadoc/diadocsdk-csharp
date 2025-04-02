|              | Build Status
|--------------|:--------------:
| master       | ![Build status (master)](https://github.com/diadoc/diadocsdk-csharp/actions/workflows/actions.yml/badge.svg?branch=master)
| latest       | ![Build status (lastest)](https://github.com/diadoc/diadocsdk-csharp/actions/workflows/actions.yml/badge.svg)
| nuget        | [![diadocsdk](https://buildstats.info/nuget/diadocsdk)](https://www.nuget.org/packages/diadocsdk/)

# diadocsdk-csharp

diadocsdk-csharp является официальной C#-реализацией клиента, использующего [публичный API Диадока](http://api-docs.diadoc.ru/).

Для подключения diadocsdk-csharp к вашему проекту рекомендуется использовать [nuget-пакет diadocsdk](https://www.nuget.org/packages/DiadocSDK/). Также можно скачать готовую сборку diadocsdk [со страницы релизов](https://github.com/diadoc/diadocsdk-csharp/releases).

## Документация

Документация последней версии SDK доступна по ссылке: http://api-docs.diadoc.ru/.

Мы планируем освежить документацию. Если у вас после её прочтения остаются вопросы, пожалуйста, выскажитесь в соответствующей [issue](https://github.com/diadoc/diadocsdk-csharp/issues/454).

## Примеры использования

[Diadoc.Samples](https://github.com/diadoc/diadocsdk-csharp/tree/master/Samples/Diadoc.Samples) — примеры кода работы API с пояснениями.

[Diadoc.Console](https://github.com/diadoc/diadocsdk-csharp/tree/master/Samples/Diadoc.Console) — пример консольного приложения, с помощью которого можно получать и отправлять документы, читать события, устанавливать связи с контрагентами.


## Сборка проекта

Для сборки проекта требуется установленный dotnet версии 6 или новее 

Запуск powershell-скрипта `build.ps1` скачает необходимые для сборки утилиты dotnet и начнет сборку проекта
`build.ps1` - является кроссплатформенным скриптом, может запускаться как на windows-системах, так и на linux-системах

Выполняется:

- генерация версии на основе тега github
- генерация C#-кода из proto-файлов
- ILMerge (сборка protobuf-net включается в DiadocApi)
- подписание сборки строгим именем (при наличии ключа diadoc.snk в папке src)
- создание nuget-пакета

## Добавление функциональности

- [Сделайте Fork](https://guides.github.com/activities/forking/)
- Создайте ветку для новой фичи (git checkout -b my-new-feature)
- Сделайте Commit изменений (git commit -am 'Add some feature')
- Сделайте Push новой ветки (git push origin my-new-feature)
- Создайте новый Pull Request
