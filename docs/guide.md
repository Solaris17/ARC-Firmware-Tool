# Flash Guide
[Home](https://github.com/Solaris17/ARC-Firmware-Tool)

## Getting Started

The firmware tool is a wrapper for IGSC the console tool used to interface with the GPUs. Intel delivers firmware during the display driver package install.

This comes with some caveats.

+ Windows auto install will not update the firmware.
+ Installing a new Intel GPU and using the previous installation will not update the firmware.
+ Installing previous drivers will not rollback firmware (Driver installs are upgrade only)
+ If the flash process times out during the driver install it will not attempt to install the firmware again.

As you can see too much can be left up in the air. As development matures and driver release cycles slow down depending on how you maintain your system you may find yourself behind on firmware.
Further; power limits and clock changes that exist in a certain firmware may not exist in a newer one as power or thermal limits are imposed. This tool will allow you to downgrade which can be beneficial for those chasing performance or stability.

## How it works

Intel unlike Nvidia or AMD does not utilize a single binary to update firmware. Instead they trigger several different firmware upgrades during driver install depending on the card and component.

This is a short definition of the fields provided by the tool:

Firmware: Main FW payload. This will have ```"_gfx_fwupdate_``` in the title; like ```dg2_gfx_fwupdate_SOC1.bin```.

Oprom (*Data*): Oprom data. This will have a ```"_d_"``` in the title; like ```dg2_d_oprom_asr770.rom```.

Oprom (*Code*): Oprom code. This will have a ```"_c_"``` in the title; like ```dg2_c_oprom.rom```.

> [!NOTE]
> The Data and Code versions can differ, but they are often the same.

FW Data / Config Data: 
 - FW data. This will have ```"_fwdata_"``` in the title; like ```dg2_fwdata_asrock770.bin```.
 - FW Config data. This will have ```"_config"``` in the title; like ```dg2_intel_a770_config-data.bin```.
	- (They utilize the same field on the flash tool.)

The sequence chosen reflects the sequence of the flash as it appears in the Intel driver flash log. While no ill has been seen from flashing out of order the sequence was kept anyway.

## How do I begin?

First lets take a look at the firmware matrix I have assembled. You can access it here:

https://www.techpowerup.com/forums/threads/intel-arc-firmware-compilation-matrix.312440/

Once you have your files it as simple as making sure they are put into the correct boxes using the guide above.

> [!NOTE]
> Intel LE card FW files are generally generically named. Such as: ```dg2_d_oprom_IBC512.rom``` (A770) or ```dg2_intel_a750_config-data.bin``` 

Now that you have your files lets load up in the software
> [!NOTE]
> You don't need to flash every firmware type. If you want to experiment and only update a specific feature you can.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/Flashing.gif?raw=true)

The flash output will be displayed in the text box. You can scroll around to check the output or we can save the log which is next.

> [!NOTE]
> "Error: Failed to read." is a generic warning given when a field is empty during processing and nothing to worry about. The actual flash data is still available from all phases of the flash.

Saving the log is as simple as pressing: ```File > Save Log``` from the menu. This can also be done if just doing a hardware scan.

![alt text](https://github.com/Solaris17/ARC-Firmware-Tool/blob/beta/pictures/Log-save.gif?raw=true)

## Finishing up

:tada: you did it! That is really all there is too it!

Intel's flashing tool and protections are resilient. While it is not impossible for a bad flash to happen given that FW updates generally happen during driver install and there are many in circulation the changes are slim.
The flash tool also has methods of cross flash protection so selecting the incorrect firmware wont result in a brick.

Finally; the tool has a really robust flash failure mechanism. In the event the cards I/O doesn't respond and the flash is terminated, not only will you get it in the logs, you can always try again!

Happy flashing!