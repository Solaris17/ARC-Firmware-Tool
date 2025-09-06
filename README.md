<h1 align="center">ARC Firmware Tool</h1>

## Stats

<p align="center">
<a href="https://github.com/Solaris17/ARC-Firmware-Tool/releases"><img alt="GitHub release (with filter)" src="https://img.shields.io/github/v/release/Solaris17/ARC-Firmware-Tool"></a>
<a href="https://github.com/Solaris17/ARC-Firmware-Tool/releases"><img alt="GitHub all releases" src="https://img.shields.io/github/downloads/Solaris17/ARC-Firmware-Tool/total?label=Downloads"></a>
</p>

## About

> [!NOTE]
> *This tool and its author are not affiliated with Intel in any way.*

A GUI application for Intel ARC firmware flashing!

[Take a look at the Flashing guide](docs/guide.md)

This tool allows the flashing and downgrading of firmware on Intel discrete graphics cards.

Looking for fancy maybe broken features? Check out the [beta branch](https://github.com/Solaris17/ARC-Firmware-Tool/tree/beta).

Don't forget to peek at the [firmware matrix](https://github.com/Solaris17/Arc-Firmware)

> [!TIP]
> **Please do not request flashing help in this repo!**

Instead feel free to ask in a more appropriate forum like [TechPowerUp forums](https://www.techpowerup.com/forums/forums/intel-arc-gpus.94/).

-----

## Utility

> [!NOTE]
> A few screen shots including the flash sequence as well as the various log functions.

Scan the hardware to get device and FW Information!

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Scanning.gif?raw=true)

Check a firmware file if its of unknown origin!

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Checking.gif?raw=true)

Flash a firmware file to the device!

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Flashing.gif?raw=true)

Upload the HW scan or the general out for easy sharing, or simply save it locally!

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Log-save.gif?raw=true)

## Features

> [!NOTE]
> This software also has the ability to:

- Check for new versions Stable and Beta
- Download the latest vbios's
- Download the lastest Driver
- Upload Logs for easy sharing
- Multiple instances disabled for safe flashing

Check for updates

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Update.gif?raw=true)

Download the latest vBios pack

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Bios-Download.gif?raw=true)

Download the latest driver

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/Driver-Download.gif?raw=true)

-----

## Misc

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/master/pictures/About.gif?raw=true)

-----

# Design Form

## Versioning

Versioning is done as follows:

Major.Minor.Patch

Major versions are based on and upto the GPU generation. For example **_1_**.2.0 would be Intel's first generation GPU; Alchemist.

Minor versions follow tick-tock policy. Even numbers are stable releases, odd numbers are BETA. For example 1.**_2_**.0 would be a stable release.

Patch versions are generally just that though a feature may sneak in; usually in BETA builds.

## Supported GPUs

- Alchemist
- Meteor Lake (Reading)
- Lunar Lake (Reading)
- Battlemage

-----

# Todo
- Make it pretty someday
- Streamline docs

-----

# Information

[How it all started](docs/history.md)

[Requirements](docs/requirements.md)
