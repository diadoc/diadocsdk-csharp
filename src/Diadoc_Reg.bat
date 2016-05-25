REM Регистрирует сборку, чтобы Diadoc API стало доступно через COM.

c:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe "%~dp0DiadocApi.dll" /codebase /tlb:DiadocApi.tlb
pause