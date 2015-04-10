# Winium для Desktop
[![Build Status](https://img.shields.io/jenkins/s/http/opensource-ci.2gis.ru/Winium.Desktop.svg?style=flat-square)](http://opensource-ci.2gis.ru/job/Winium.Desktop/)
[![GitHub release](https://img.shields.io/github/release/2gis/Winium.Desktop.svg?style=flat-square)](https://github.com/2gis/Winium.Desktop/releases/)
[![GitHub license](https://img.shields.io/badge/license-MPL 2.0-blue.svg?style=flat-square)](LICENSE)

<p align="center">
<img src="https://raw.githubusercontent.com/2gis/Winium.StoreApps/assets/winium.png" alt="Winium.Desktop это реализация Selenium Remote WebDriver для автоматизации тестирования Windows приложений построенных на WinFroms и WPF платформах">
</p>

Winium.Desktop это open-source инструмент для автоматизации тестирования Windows приложений построенных на WinFroms и WPF платформах.

## Поддерживаемые платформы
- WinForms
- WPF

Для автоматизации Windows Phone 8.1 есть [Windows StoreApps Driver](https://github.com/2gis/Winium.StoreApps).
Для автоматизации Windows Phone 8 Silverlight есть [Windows Phone Driver](https://github.com/2gis/winphonedriver).

## Почему Winium?
Уже есть Selenium WebDriver для тестирования веб приложений, Appium для тестирования iOS и Android приложений. А теперь появился Selenium-based инструмент для тестирования Windows приложений. Какие он дает преимущества? Цитируя Appium:
> - You can write tests with your favorite dev tools using any WebDriver-compatible language such as Java, Objective-C, JavaScript with Node.js (in promise, callback or generator flavors), PHP, Python, Ruby, C#, Clojure, or Perl with the Selenium WebDriver API and language-specific client libraries.
> - You can use any testing framework.

А по-русски можно?
- Пишите тесты, используя ваши любимые инструменты, любой WebDriver-совместимый язык программирования, например, Java, Objective-C, JavaScript with Node.js, PHP, Python, Ruby, C#, Clojure...
- Используйте любой тестовый фреймворк.

## Требования
* Microsoft .NET Framework 4.5.1

## Быстрый старт
1. Пишите тесты на удобном языке. В тесте используйте `app` [desired capability](https://github.com/2gis/Winium.Desktop/wiki/Capabilities) для задания исполняемого файла приложения. Это пример на python:
	```python
	# put it in setUp
	self.driver = webdriver.Remote(command_executor='http://localhost:9999',
	                               desired_capabilities={'app': 'C:\\testApp.exe'})
	# put it in test method body
	win = self.driver.find_element_by_id('WpfTestApplicationMainWindow')
	win.find_element_by_id('SetTextButton').click()
	assert 'CARAMBA' == self.driver.find_element_by_id('MyTextBox').text
	```

2. Запустите `Winium.Desktop.Driver.exe` ([загрузить последнюю версию с github](https://github.com/2gis/Winium.Desktop/releases) или соберите проект у себя)

3. Запустите тесты и балдейте от происходящей магии

## Как это работает
**Winium.Desktop.Driver** реализует Selenium Remote WebDriver и слушает команды в JsonWireProtocol. Для автоматизации действий над приложением используется фреймворк [Winium.Cruciatus](https://github.com/2gis/Winium.Cruciatus).

## Вклад в развитие

Мы открыты для сотрудничества!

1. Проверьте нет ли уже открытого issue или заведите новый issue для обсуждения новой фичи или бага.
2. Форкните репозиторий и начните делать свои изменения в ветке мастер или новой ветке
3. Мы советуем написать тест, который покажет, что баг был починен или что новая фича работает как ожидается.
4. Создайте pull-request и тыкайте в мэнтейнера до тех пор, пока он не примет и не сольет ваши изменения.  :smiley:

## Контакты

Есть вопросы? Нашли ошибку? Создавайте [новое issue](https://github.com/2gis/Winium.Desktop/issues/new) или пишите g.golovin@2gis.ru

## Лицензия

Winium выпущен под MPL 2.0 лицензией. [Подробности](LICENSE).
