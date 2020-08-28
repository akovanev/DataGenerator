# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://github.com/akovanev/DataGenerator)

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
* `set` one of the list of values, separated by comma.
* `bool` True or False.
* `int` integer.
* `double` double.
* `datetime` datatime
* `object` object from definition.
* `array` array from definition.

`pattern` specifies the template for the specific type. E.g. "abcdefgh" for strings, "0.00" for doubles, "yy/MM/dd" for datateime etc. For arrays and objects points to the definition item name.

`minLength` minimum output data length (for `string` only).

`maxLength` maximum output data length (for `string` and `array` only).

`minSpaceCount` minimum count of spaces (for `string` only).

`maxSpaceCount` maximum count of spaces (for `string` only).

`minValue` minimum value (for `int`, `double` and `datetime`).

`maxValue` maximum value (for `int`, `double` and `datetime`).

`failure` stands for inconsitent data appearing with the specified probability. 
* `nullable` the probability that *null* appears.

* `format` the probability that the invalid value appears.

* `range` the probability that the value will be out of range. For strings it means that the string length will be out of the specified interval.

## Example

[data.json](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator.Console/data.json)



