
# DotNetRemoteWebDriver

The DotNetRemoteWebDriver is meant to be a port of the Java stand alone remote web driver in order to be able to
reuse or connect remotely to a selenium session without having to have a specific Java version installed.

The project is based on the [Winium.Desktop](https://github.com/2gis/Winium.Desktop) project since this had a full selenium web API
created already. The implementation has been change to tunnel any incoming requests to a local instance of the given driver.

## Supported drivers
- Firefox
- Chrome
- Internet Explorer

## Requirements
* Microsoft .NET Framework 4.5.1

## Quick Start
1. Write your test script, connecting to the `DotNetRemoteWebDriver`
```ruby
options = {
	desired_capabilities: Selenium::WebDriver::Remote::Capabilities.chrome,
	url: 'http://localhost:4444/'
}
driver = Selenium::WebDriver::Driver.for(:remote, options)
driver.get('http://google.com')
driver.quit()
```
2. Place the `DotNetRemoteWebDriver.exe` and additional driver exe's (`chrome.exe`)
in the same folder or accessible in the `%PATH%` and run:
```
C:\Drivers>DotNetRemoteWebDriver.exe
```
3. Run your script

## License
DotNetRemoteWebDriver is released under the MPL 2.0 license. See [LICENSE](LICENSE) for details.
