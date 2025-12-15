|              | Build Status
|--------------|:--------------:
| master       | ![Build status (master)](https://github.com/diadoc/diadocsdk-csharp/actions/workflows/actions.yml/badge.svg?branch=master)
| latest       | ![Build status (lastest)](https://github.com/diadoc/diadocsdk-csharp/actions/workflows/actions.yml/badge.svg)
| nuget        | [![diadocsdk](https://buildstats.info/nuget/diadocsdk)](https://www.nuget.org/packages/diadocsdk/)

# diadocsdk-csharp

diadocsdk-csharp — официальная C#-реализацией клиента, использующего [публичный API Диадока](http://api-docs.diadoc.ru/).

Для подключения diadocsdk-csharp к вашему проекту используйте [nuget-пакет diadocsdk](https://www.nuget.org/packages/DiadocSDK/).
Также вы можете скачать готовую сборку diadocsdk [со страницы релизов](https://github.com/diadoc/diadocsdk-csharp/releases).

## Документация

Документация последней версии SDK доступна по ссылке: http://api-docs.diadoc.ru/.

Обратную связь по документации вы можете оставить [здесь](https://forms.kontur.ru/form/ddapidoc-feedback).

## Примеры использования

[Diadoc.Samples](https://github.com/diadoc/diadocsdk-csharp/tree/master/Samples/Diadoc.Samples) — примеры кода работы API с пояснениями.

[Diadoc.Console](https://github.com/diadoc/diadocsdk-csharp/tree/master/Samples/Diadoc.Console) — пример консольного приложения, с помощью которого можно получать и отправлять документы, читать события, устанавливать связь с контрагентами.

## Сборка проекта

Для окончательной сборки проекта используется утилита [Cake](http://cakebuild.net/).

Запуск powershell-скрипта `build.ps1` скачает утилиту Cake, если ее у вас нет, и запустит сборку проекта.
Из командной строки этот скрипт можно запустить с помощью `generate.bat`.

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

## Наши контакты

- Клиентская поддержка [Контур.Диадок](https://support.kontur.ru/?support_widget=diadoc) - для решения ошибок в работе интеграционного решения
- [Заявка на интеграцию](https://www.diadoc.ru/integrations/api) - для подключения интеграции и связанных вопросов. Например, если нужен сертификат, узнать тарифы и стоимость подключения
- [Форма ОС](https://forms.kontur.ru/form/ddapidoc-feedback) в Документации - для исправления ошибок в Документации
