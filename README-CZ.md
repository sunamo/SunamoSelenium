# SunamoSelenium

Snadná práce se Selenium.

## Rozdíly v Target Frameworku

### Hlavní Knihovna (SunamoSelenium.csproj)
- **Target Frameworks**: net10.0, net9.0, net8.0
- **Důvod**: NuGet balíčky musí podporovat více verzí .NET pro širokou kompatibilitu

### Testovací a Runner Projekty
- **Target Framework**: pouze net10.0
- **Dotčené projekty**:
  - SunamoSelenium.Tests
  - RunnerSelenium
- **Důvod**: Testovací a runner projekty nepotřebují multi-targeting. Použití pouze nejnovější verze .NET (net10.0) zjednodušuje správu závislostí a vyhýbá se problémům s kompatibilitou balíčků.

## Změny Verzí Balíčků (2026-02-04)

### PowerShell a System.Management.Automation
- **Změněno z**: 7.5.4 → 7.4.6
- **Důvod**: System.Management.Automation 7.5.4 vyžaduje pouze .NET 9+. Verze 7.4.6 podporuje net8.0, net9.0 i net10.0.

### Microsoft.Extensions.Logging.Abstractions
- **Změněno z**: 10.0.2 → * (nejnovější kompatibilní)
- **Důvod**: Použití wildcard zajistí že automaticky dostaneme nejnovější kompatibilní verzi, čímž se vyhneme konfliktům verzí se závislostmi.
