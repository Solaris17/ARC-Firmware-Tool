# ARC Firmware Tool

A GUI application for Intel ARC firmware flashing.

Flashing guide provided at:

https://www.techpowerup.com/forums/threads/guide-flashing-intel-arc-gpus.311964/

Intel ARC FW Matrix located here:

https://www.techpowerup.com/forums/threads/intel-arc-firmware-compilation-matrix.312440/

Project was built using my modified version of Intels IGSC:

https://github.com/Solaris17/igsc

There original repo is located here: 

https://github.com/intel/igsc

Trademarks are property of their respective owners.

## A few screenshots of the flash sequence as well as the save log function.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/main-window.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/device-scan.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-start.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-prog.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/save-log.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/save-done.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/flash-log.png?raw=true)

## The software also has the ability to check for new versions in addition to an aboutbox with basic info.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/update-check.png?raw=true)

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/about-box.png?raw=true)

-----

# Design Form

## Versioning

Versioning is done as follows:

Major.Minor.Patch

Major versions are based on generation. For example **_1_**.2.0 would be Intels first generation GPU; ARC.

Minor versions follow tick-tock policy. Even numbers are stable releases, odd numbers are BETA. For example 1.**_2_**.0 would be a stable release.

Patch versions are generally just that though a feature may sneak in; usually in BETA builds.

-----

# Todo
- Create seperate page for documentation