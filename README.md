# SerialPop

Get a pop up whenever a serial port connects to or disconnects from your machine.
Connect to a serial port with just three mouse clicks using your favourite tool.

![This is what the context menu might look like](docs/images/menu.png)


## Table of Contents
* [Installation and Usage](#installation-and-usage)
* [Reporting bugs or requesting features](#reporting-bugs-or-requesting-features)
* [Source Code and Building](#source-code-and-building)
* [How to test](#how-to-test)
* [Future and Roadmap](#future-and-roadmap)
* [Contributing](#contributing)


## Installation and Usage
### Installing and running for the first time
There is no explicit installtion step, just grab the [latest release from the repo](https://github.com/alk0ve/SerialPop/releases), unpack it and run. To make sure it runs without errors, you can either right-click on the SerialPop task bar icon (and see a menu that looks like the one in the screenshot above), or connect or disconnect a serial device to have SerialPop generate a notification.
Once SerialPop runs, you should open the Settings tab to define how you want to connect to serial ports by specifying the tool you want to run and its arguments, with optional placeholders **{COM}** and **{BAUDRATE}**, which will be replaced by SerialPop with the serial port address and baud rate you click on in the context menu (there's an example of what your arguments look like *after* replacing the placeholders with sample values).

### Usage
SerialPop does two things: it displays notifications whenever you connect or disconnect a serial device from your machine, and it allows you to quickly connect to any available serial port via the context (right-click) menu. In order to connect to a serial device you need to first point to the executable you want to use (*putty.exe*'s path, for example), and then specify its arguments, where you can use the placeholders **{COM}** and **{BAUDRATE}**. When you click on a specific menu entry SerialPop will use that entry's COM port address and baud rate instead of the placeholders. The Settings form also includes an example that demonstrates how your arguments will end up after replacing the placeholders.

![A sample Settings form](docs/images/settings.png)

### Quality of life improvements
There are three things you should probably do in order to get the most out of SerialPop:
* Populate the baud rate list with the baud rates you usually see (the two most common ones are 57600 and 115200, but this very much depends on the device you're dealing with)
* Make SerialPop always visible in your taskbar (on the current Windows 10 you need to right-click the taskbar --> Taskbar Settings --> scroll down to Notification area --> *Select which icons appears on the taskbar* --> click *On* in the SerialPop row)
* Make SerialPop run on startup (last time I checked you could add a shortcut to SerialPop in *C:\Users\[your user name]\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup*)

Note that your settings are stored in *config.xml*, in the same folder as SerialPop itself (which means that it should have enough permissions to create the file).
The configuration file's format is simple, and typically looks similar to this:
```xml
<?xml version="1.0" encoding="utf-8"?>
<ConfigurationStruct xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ExecutablePath>putty.exe</ExecutablePath>
  <Arguments>-serial {COM} -sercfg {BAUDRATE},8,n,1,N</Arguments>
  <BaudRates>
    <int>115200</int>
    <int>57600</int>
    <int>500000</int>
  </BaudRates>
</ConfigurationStruct>
```


### Errors and issues
various edge cases (only one baud rate, for example)
	mention why menu entries might be disabled
	mention error pop-ups
	


## Reporting bugs or requesting features
If you find any bugs or issues - please [create an issue](https://github.com/alk0ve/SerialPop/issues), and make sure you include instructions for reproducing the issue; I'll try to fix it as soon as possible.
If you have suggestions for features you'd like to see in SerialPop you can create and issue detailing your idea, and I might get around to implementing it, depending on how much public interest there is (and how much I personally think it's a good idea).


## Source Code and Building
	write a high-level overview of threads and classes
	
	document the build process :)
		- mention the weird issue with Settings not rendering before the first build


## How to test
	explain how to use dummy.bat for testing
	explain that you can use com0com's setupg.exe (UI) and setupc.exe (command line) to create new virtual COM ports (just enable "use Ports class")

## Future and Roadmap
	refactoring this into a service that can send COM port updates to several subscribers, one of them being SerialPop
		- and that you might consider doing it if there's enough interest
		- using some message queue, UDP, etc.



## Contributing
There are three ways you can show appreciation for this project or contribute to it
* [Buy me a coffee](https://ko-fi.com/alk0ve) (also under 'Sponsor this project' to your right)
* [Report any bugs or issues you find](https://github.com/alk0ve/SerialPop/issues) (and don't forget to include intructions on how to them)
* If you want to help me implement more features or refactor the source code - get in touch with me (my email should be visible in my profile)


	
