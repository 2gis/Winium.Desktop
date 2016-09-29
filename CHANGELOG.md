# Change Log

<!--## Unreleased
- Support GetCurrentWindowHandle and GetWindowHandles
- Add driver option --silent to disable logging
-->


## v1.6.0

- Fix exception Process Not Found in close/quit function
- Fix throw exceptions in getting some gui element's attributes


## v1.5.0

- Add `SwitchToWindow` command
- Fix XPath attribute value if this ControlType


## v1.4.0

- Add `XPath` strategy for locating elements
- Add `GetCurrentWindowHandle` command
- Add `GetWindowHandles` command
- Add `--silent` option to a driver CLI (suppresses output)
- Fix logger timestamp format


## v1.3.0

- Fix error response format
- Add `args` capability for launching application with arguments
- Set default elements search timeout to 10 seconds (fixed in [Winium.Cruciatus 2.7.0](https://github.com/2gis/Winium.Cruciatus/releases/tag/v2.7.0))
- Single file Driver distribution
- Add extended driver commands (see [Winium.Elements](https://github.com/2gis/Winium.Elements/releases) for bindings). Extended commands simplify usage of following elements:
 - ComboBox (collapse, expand, is expanded, find selected item, scroll to item)
 - DataGrid (row count, column count, find cell, scroll to cell, select cell)
 - ListBox (scroll to item)
 - Menu (find item, select item)


## v1.2.0

New features:
- Support Action Chains from bindings
- Add new script command for setting value to element using ValuePattern



