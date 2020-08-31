# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

[Data generator](https://akovanev.com/2020/08/26/data-generator/) - a quick introduction.

[Custom data generator](https://akovanev.com/2020/08/27/custom-data-generator/) - creating a custom generator based on the library.

[Calculated properties with Data generator](https://akovanev.com/2020/08/31/calculated-properties-with-data-generator/) - calculated properties and files as custom data sources.

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
* `file` file of values.
* `bool` True or False.
* `int` integer.
* `double` double.
* `datetime` datatime.
* `object` object from definition.
* `array` array from definition.

`pattern` 
* for `string` defines all possible characters, e.g. "abcdefghABCFEDGH". Spaces will be added additionally.
* for `set` defines all possible values separated by comma by default.
* for `file` specifies the path to an existing file. The data are separated by comma by default. 
* for `double` specifies the output format, e.g. "0.00".
* for `datetime` specifies the output format, e.g. "yyyy-MM-dd".
* for `array` and `object` points to the definition item name.

`sequenceSeparator` the separator for `set` and within a file. 

`minLength` the minimum output data length for `string`.

`maxLength` the maximum output data length for `string`, size for `array`.

`minSpaceCount` the minimum count of spaces in the `string`.

`maxSpaceCount` the maximum count of spaces in the `string`.

`minValue` the minimum value for `int`, `double` and `datetime`.

`maxValue` the maximum value for `int`, `double` and `datetime`.

`failure` stands for inconsitent data appearing with the specified probability. 

* `nullable` the probability that *null* appears.

* `custom` the probability that the invalid value appears.

* `range` the probability that the value will be out of range. For `string` it currently means that the string length will be out of the specified interval.
 
`customFailure` specifies the value that will appear for the `custom` failure.

## Example

[data.json](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator.Console/data.json)



