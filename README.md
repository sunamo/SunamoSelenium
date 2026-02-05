# SunamoSelenium

Code base for easy work with Selenium

## Overview

SunamoSelenium is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development.

## Main Components

### Key Classes

- **ByHelper**
- **ExceptionsExtensions**
- **StringToUnixLineEndingExtensions**
- **PsOutput**
- **SeleniumHelper**
- **CmpWorkaroundService**
- **SeleniumNavigateService**
- **SeleniumService**

### Key Methods

- `ClassName()`
- `GetAllMessages()`
- `ToUnixLineEnding()`
- `InvokeAsync()`
- `ProcessErrorRecords()`
- `ProcessPSObjects()`
- `InitEdgeDriver()`
- `VerifyHuman()`
- `CmpWorkaroundService()`
- `LoginSeznamCz()`

## Installation

```bash
dotnet add package SunamoSelenium
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions** (v9.0.3)
- **Microsoft.PowerShell.SDK** (v7.5.0)
- **Selenium.Support** (v4.30.0)
- **Selenium.WebDriver** (v4.30.0)
- **DotNetSeleniumExtras.WaitHelpers** (v3.11.0)
- **System.Management.Automation** (v7.5.0)

## Package Information

- **Package Name**: SunamoSelenium
- **Version**: 25.8.22.1
- **Target Framework**: net9.0
- **Category**: Platform-Independent NuGet Package
- **Source Files**: 10

## Target Framework Differences

### Main Library (SunamoSelenium.csproj)
- **Target Frameworks**: net10.0, net9.0, net8.0
- **Reason**: NuGet packages must support multiple .NET versions for broad compatibility

### Test & Runner Projects
- **Target Framework**: net10.0 only
- **Projects affected**:
  - SunamoSelenium.Tests
  - RunnerSelenium
- **Reason**: Test and runner projects don't need multi-targeting. Using only the latest .NET version (net10.0) simplifies dependency management and avoids package compatibility issues.

## Package Version Changes (2026-02-04)

### PowerShell and System.Management.Automation
- **Changed from**: 7.5.4 → 7.4.6
- **Reason**: System.Management.Automation 7.5.4 requires .NET 9+ only. Version 7.4.6 supports net8.0, net9.0, and net10.0.

### Microsoft.Extensions.Logging.Abstractions
- **Changed from**: 10.0.2 → * (latest compatible)
- **Reason**: Using wildcard ensures we get the latest compatible version automatically, avoiding version conflicts with dependencies.

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.
