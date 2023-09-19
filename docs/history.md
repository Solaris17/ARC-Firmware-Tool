# How it all started
[Home](https://github.com/Solaris17/ARC-Firmware-Tool)

## TL;DR

Project was built using my modified version of Intel's IGSC:

https://github.com/Solaris17/igsc

There original repo is located here: 

https://github.com/intel/igsc

Trademarks are property of their respective owners.

-----

## Finding the answer

ARC is in its infancy and I have playing with the platform since around the end of 2022 year. I have several cards and found it curious that when I installed my older A380 the BIOS was older. After some searching I realized Intel takes a different approach to firmware updates.
The updates are done during driver installation. It should be noted this FW update ONLY happens during the driver install; NOT if you are putting another Intel card in IE: A card returned from RMA, or an upgrade.

Accessing the firmware and the tools was as easy as unzipping the newer drivers, or "opening file location" from task manager on older drivers with the installer running. With executable and firmware in hand I soon found that the flash tool included in the drivers could not be easily manipulated on its own. I eventually found that Intel themselves host this code as open source in there git.
With the repo in hand I compiled the tool and started to see what could be done. It should be noted that you **CANNOT** backup the BIOS using this tool (**YET**). However flashing may prove useful for those that like a certain driver version but want the various clock or power limit changes of a newer one.