REM Register Diadoc.Api assembly to get access from COM interface

c:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe "%~dp0DiadocApi.dll" /codebase /tlb:DiadocApi.tlb
pause