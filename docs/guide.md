# Flash Guide
[Home](https://github.com/Solaris17/ARC-Firmware-Tool)

## Getting Started

The firmware tool is a wrapper for IGSC the console tool used to interface with the GPUs. Intel delivers firmware during the display driver package install.

This comes with some cavets.

+ Windows auto install will not update the firmware.
+ Installing a new Intel GPU and using the previous installation will not update the firmware.
+ If the flash process times out during the driver install it will not attempt to install the firmware again.

As you can see too much can be left up in the air. As development matures and driver release cycles slow down depending on how you maintain your system you may find yourself behind on firmware.

## How it works

Intel unlike Nvidia or AMD does not utilize a single binary to update firmware. Instead they trigger several different firmware upgrades during driver install depending on the card and component.

This is a short definition of the fields provided by the tool:

Firmware: Main FW payload. This will have "_gfx_fwupdate_ in the title; like dg2_gfx_fwupdate_SOC1.bin.

Oprom (Data): Oprom data. This will have a "_d_" in the title; like dg2_d_oprom_asr770.rom.

Oprom (Code): Oprom code. This will have a "_c_" in the title; like dg2_c_oprom.rom.

FW Data / Config Data: 
 - FW data. This will have "_fwdata_" in the title; like dg2_fwdata_asrock770.bin.
 - FW Config data. This will have "_config" in the title; like dg2_intel_a770_config-data.bin.
	- (They utilize the same field on the flash tool.)

The sequence chosen reflects the sequence of the flash as it appears in the intel driver flash log. While no ill has been seen from flashing out of order the sequence was kept anyway.

## How do I begin?

First lets take a look at the firmware matrix I have assembled. You can access it here:

https://www.techpowerup.com/forums/threads/intel-arc-firmware-compilation-matrix.312440/