             | Build Status
-------------|:--------------:
master       | [![Build status (master)](https://ci.appveyor.com/api/projects/status/7mgixoupt0oagwse/branch/master?svg=true)](https://ci.appveyor.com/project/diadoc-admin/diadocsdk-csharp/branch/master)
lastest      | [![Build status (lastest)](https://ci.appveyor.com/api/projects/status/7mgixoupt0oagwse?svg=true)](https://ci.appveyor.com/project/diadoc-admin/diadocsdk-csharp)
nuget        | [![diadocsdk](https://img.shields.io/nuget/v/DiadocSDK.svg)](https://www.nuget.org/packages/DiadocSDK/)

# diadocsdk-csharp

diadocsdk-csharp является официальной C#-реализацией клиента, использующего [публичный API Диадока](http://api-docs.diadoc.ru/).

## Сборка проекта

Для окончательной сборки проекта используется утилита [Cake](http://cakebuild.net/).

Запуск powershell-скрипта `build.ps1` скачает утилиту Cake, если ее у вас нет, и запустит сборку проекта.
Из командной строки этот скрипт можно запустить с помощью `generate.bat`.

Выполняется:

- генерация версии на основе тега github
- генерация C#-кода из proto-файлов
- ILMerge (сборка protobuf-net включается в DiadocApi)
- подписание сборки строгим именем
- создание nuget-пакета

## Добавление функциональности

- [Сделайте Fork](https://guides.github.com/activities/forking/)
- Создайте ветку для новой фичи (git checkout -b my-new-feature)
- Сделайте Commit изменений (git commit -am 'Add some feature')
- Сделайте Push новой ветки (git push origin my-new-feature)
- Создайте новый Pull Request
