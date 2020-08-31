# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

[Quick introduction](https://akovanev.com/2020/08/26/data-generator/)

[Custom data generator](https://akovanev.com/2020/08/27/custom-data-generator/)

## Execution

```
> DataGenerator <filename.json>
```

In case of success the result will be stored to <filename.json>.out.json

## Input configuration

`root` points to the main entry object in definitions.

`definitions` defines objects and arrays.

### Properties

`name` the property name.

`type` the property type. 
* `string` string.
* `set` one of the list of values.
* `file` file of values, separated by commma by default.
* `bool` True or False.
* `int` integer.
* `double` double.
* `datetime` datatime.
* `object` object from definition.
* `array` array from definition.

`pattern` 
* for `string` defines all possible characters, e.g. "abcdefghABCFEDGH". Spaces will be added additionally.
* for `set` defines all posiible values separated by comma by default.
* for `file` specifies the path to an existing file. 
* for `double` specifies the output format, e.g. "0.00".
* for `datetime` specifies the output format, e.g. "yyyy-MM-dd".
* for `array` and `object` points to the definition item name.

`minLength` minimum output data length for `string`.

`maxLength` maximum output data length for `string`, maximum size for `array`.

`minSpaceCount` minimum count of spaces in the string (for `string` only).

`maxSpaceCount` maximum count of spaces in the string (for `string` only).

`minValue` minimum value for `int`, `double` and `datetime`.

`maxValue` maximum value for `int`, `double` and `datetime`.

`failure` stands for inconsitent data appearing with the specified probability. 

* `nullable` the probability that *null* appears.

* `custom` the probability that the invalid value appears.

* `range` the probability that the value will be out of range. For `string` it currently means that the string length will be out of the specified interval.
 
`customFailure` specifies the value that will appear for the `custom` failure.

## Example

[data.json](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator.Console/data.json)



