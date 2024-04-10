ItemPrinterDeGacha
=====
Calculation tool for Scarlet/Violet's Item Printer to forecast results from various print jobs.

The game uses time_t to seed the RNG on first launch of the printer, for the rest of the session.

## Screenshots

![image](https://github.com/kwsch/ItemPrinterDeGacha/assets/6393368/855a3056-0506-4fe8-8660-60ab8300a8c1)

## Configuration

The program saves a `settings.json` next to the executable when opening/closing. To change the language of the program when it next launches, change the 2-character language code to one of the following:

* "ja" = Japanese
* "en" = English
* "fr" = French
* "es" = Spanish
* "de" = German
* "it" = Italian
* "ko" = Korean
* "zh" = Chinese (Simplified)
* "zh2" = Chinese (Traditional)

Currently, only Item Names are localized; no GUI/message localization. The program is still usable since the main interest is in items and datetime seeds.

Additionally, the setting can be changed from the default `"yyyy-MM-dd HH:mm:ss"` to any valid date-time formatter to change how datetime values are displayed. `""` will use the computer's current culture formatting. Time Zones are irrelevant; when using a seed, the console seeds the RNG based on the console's local time (seconds) elapsed since 1970.

## Building

ItemPrinterDeGacha is a Windows Forms application which requires [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

The executable can be built with any compiler that supports C# 12, such as [Visual Studio](https://visualstudio.microsoft.com/downloads/) by opening the .sln or .csproj file.

### Build Configurations

Use the Debug or Release build configurations when building. There isn't any platform specific code to worry about!

## Dependencies

ItemPrinterDeGacha's image and item localization is taken from [PKHeX](https://github.com/kwsch/pkhex), which is licensed under [the GPLv3 license](https://github.com/kwsch/pkhex/blob/master/LICENSE). *All calculation is done with zero interaction with the console/game besides user input.*
