<p align="right">
English description | <a href="README_RU.md">Описание на русском</a>
</p>

# Winium for Desktop
[![Build Status](https://img.shields.io/jenkins/s/http/opensource-ci.2gis.ru/Winium.Desktop.svg?style=flat-square)](http://opensource-ci.2gis.ru/job/Winium.Desktop/)
[![GitHub release](https://img.shields.io/github/release/2gis/Winium.Desktop.svg?style=flat-square)](https://github.com/2gis/Winium.Desktop/releases/)
[![GitHub license](https://img.shields.io/badge/license-MPL 2.0-blue.svg?style=flat-square)](LICENSE)

<p align="center">
<img src="https://raw.githubusercontent.com/2gis/Winium.StoreApps/assets/winium.png" alt="Winium.Desktop is Selenium Remote WebDriver implementation for automated testing of Windows application based on WinForms and WPF platforms">
</p>

Winium.Desktop is an open source test automation tool for automated testing of Windows application based on WinForms and WPF platforms.

## Supported Platforms
- WinForms
- WPF

For Windows Phone 8.1 test automation tool see [Windows Phone Driver](https://github.com/2gis/Winium.StoreApps).
For Windows Phone 8 Silverlight test automation tool see [Windows Phone Driver](https://github.com/2gis/winphonedriver).

## Why Winium?
You have Selenium WebDriver for testing of web apps, Appium for testing of iOS and Android apps. And now you have Selenium-based tools for testing of Windows apps too. What are some of the benefits? As said by Appium:
> - You can write tests with your favorite dev tools using any WebDriver-compatible language such as Java, Objective-C, JavaScript with Node.js (in promise, callback or generator flavors), PHP, Python, Ruby, C#, Clojure, or Perl with the Selenium WebDriver API and language-specific client libraries.
> - You can use any testing framework.

## Requirements
* Microsoft .NET Framework 4.5.1

## Quick Start
1. Write your tests using you favorite language. In your tests use `app` [desired capability](https://github.com/2gis/Winium.Desktop/wiki/Capabilities) to set path to tested app's exe file. Here is python example:
	```python
	# put it in setUp
	self.driver = webdriver.Remote(command_executor='http://localhost:9999',
	                               desired_capabilities={'app': 'C:\\testApp.exe'})
	# put it in test method body
	win = self.driver.find_element_by_id('WpfTestApplicationMainWindow')
	win.find_element_by_id('SetTextButton').click()
	assert 'CARAMBA' == self.driver.find_element_by_id('MyTextBox').text
	```

2. Start `Winium.Desktop.Driver.exe` ([download release from github](https://github.com/2gis/Winium.Desktop/releases) or build it yourself)

3. Run your tests and watch the magic happening

## How it works
**Winium.Desktop.Driver** implements Selenium Remote WebDriver and listens for JsonWireProtocol commands. It is responsible for automation of app under test using [Winium.Cruciatus](https://github.com/2gis/Winium.Cruciatus).

## Contributing

Contributions are welcome!

1. Check for open issues or open a fresh issue to start a discussion around a feature idea or a bug.
2. Fork the repository to start making your changes to the master branch (or branch off of it).
3. We recommend to write a test which shows that the bug was fixed or that the feature works as expected.
4. Send a pull request and bug the maintainer until it gets merged and published. :smiley:

## Contact

Have some questions? Found a bug? Create [new issue](https://github.com/2gis/Winium.Desktop/issues/new) or contact us at g.golovin@2gis.ru

## License

Winium is released under the MPL 2.0 license. See [LICENSE](LICENSE) for details.
