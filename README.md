Get a pop up whenever a serial port connects to or disconnects from your machine.
Connect to a serial port with just three mouse clicks using your favourite tool.

![This is what the context menu might look like](docs/images/menu.png)


<table of contents > @@@@@@@@@@@@@@@@@@@@@@@@@@@@@


Installation & Usage
<get latest release, run on startup, make it always visible in taskbar, configure settings>

various edge cases (only one baud rate, for example)
	mention why menu entries might be disabled
	mention error pop-ups
	mention configuration file - insert code quote with example


bugs/feature requests? create an issue [link]


Source code & building
	write a high-level overview of threads and classes
	document the build process :)
		- mention the weird issue with Settings not rendering before the first build


testing
	explain how to use dummy.bat for testing
	explain that you can use com0com's setupg.exe (UI) and setupc.exe (command line) to create new virtual COM ports (just enable "use Ports class")

roadmap: refactoring this into a service that can send COM port updates to several subscribers, one of them being SerialPop
		- and that you might consider doing it if there's enough interest
		- using some message queue, UDP, etc.



Contributing - ko-fi, bugs/issues (how to reproduce), feature requests, help refactoring


	