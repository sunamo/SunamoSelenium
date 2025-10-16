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

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.
