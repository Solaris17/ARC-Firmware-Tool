# ARC Firmware Tool

## Stats

[![Github All Releases](https://img.shields.io/github/downloads/Solaris17/ARC-Firmware-Tool/total.svg)]()

## About

A GUI application for Intel ARC firmware flashing.

[Take a look at the Flashing guide](docs/guide.md)

This tool allows the flashing and downgrading of firmware on Intel discrete graphics cards.

## Features

> [!NOTE]
> A few screen shots of the flash sequence as well as the save log function.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/main-window.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/device-scan.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-start.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-prog.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/save-log.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/save-done.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-log.png?raw=true)

> [!NOTE]
> The software also has the ability to check for new versions in addition to an about-box with basic info.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/update-check.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/about-box.png?raw=true)

-----

# Design Form

## Versioning

Versioning is done as follows:

Major.Minor.Patch

Major versions are based on and upto the GPU generation. For example **_1_**.2.0 would be Intel's first generation GPU; Alchemist.

Minor versions follow tick-tock policy. Even numbers are stable releases, odd numbers are BETA. For example 1.**_2_**.0 would be a stable release.

Patch versions are generally just that though a feature may sneak in; usually in BETA builds.

## Requests and ideas

[Feel free to let me know what you think!](docs/requests.md)

-----

# Todo



-----

# Information

Project was built using my modified version of Intel's IGSC:

https://github.com/Solaris17/igsc

There original repo is located here: 

https://github.com/intel/igsc

Trademarks are property of their respective owners.
