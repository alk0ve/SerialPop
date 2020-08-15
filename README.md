# SerialPop

Get a pop up whenever a serial port connects to or disconnects from your machine.
Connect to a serial port with just three mouse clicks using your favourite tool.

![This is what the context menu might look like](docs/images/menu.png)


## Table of Contents @@@@@@@@@@@@@@@@@@@@@@@@@@@@@
TODO


## Installation & Usage
<get latest release, run on startup, make it always visible in taskbar, configure settings>

various edge cases (only one baud rate, for example)
	mention why menu entries might be disabled
	mention error pop-ups
	mention configuration file - insert code quote with example


# Reporting bugs or requesting features
If you find any bugs or issues - please [create an issue](https://github.com/alk0ve/SerialPop/issues), and make sure you include instructions for reproducing the issue; I'll try to fix it as soon as possible.
If you have suggestions for features you'd like to see in SerialPop you can create and issue detailing your idea, and I might get around to implementing it, depending on how much public interest there is (and how much I personally think it's a good idea).


# Source Code & Building
	write a high-level overview of threads and classes
	
	document the build process :)
		- mention the weird issue with Settings not rendering before the first build


# How to test
	explain how to use dummy.bat for testing
	explain that you can use com0com's setupg.exe (UI) and setupc.exe (command line) to create new virtual COM ports (just enable "use Ports class")

# Future & Roadmap
	refactoring this into a service that can send COM port updates to several subscribers, one of them being SerialPop
		- and that you might consider doing it if there's enough interest
		- using some message queue, UDP, etc.



# Contributing
There are three ways you can show appreciation for this project or contribute to it
* [Buy me a coffee](https://ko-fi.com/alk0ve) (also under 'Sponsor this project' to your right)
* [Report any bugs or issues you find](https://github.com/alk0ve/SerialPop/issues) (and don't forget to include intructions on how to them)
* If you want to help me implement more features or refactor the source code - get in touch with me (my email should be visible in my profile)


	
