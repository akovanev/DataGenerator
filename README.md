# DataGenerator

## Execution

```
DataGenerator <filename.json>
```

In case of success the result will be stored to <filename.json>.out.json

## Parameters

### General

`templates`the templates.

`root` the root Object.

`definitions` the definitions that could be used for `object` and/or `array` template types.

### Template types

* `string` for the string value.
  * `pattern` specifies accessible characters. The space will be added separately.

* `double` for the double value.
  * `pattern` specifies the double format. E.g. "0.00".
 
* `set` only a value from the set allowed.
  * `pattern` defines the set of values.

* `datetime` for the datetime.
  * `pattern` specifies the datatime format.


### Properties & Attributes

`name` the Object property name.

`template` the template name.

`minLength` minimum output data length (for `string` only).

`maxLength` maximum output data length (for `string` and `array` only).

`minSpaceCount` minimum count of spaces (for `string` only).

`maxSpaceCount` maximum count of spaces (for `string` only).

`minValue` minimum value (for `double` and `datetime` only).

`maxValue` maximum value (for `double` and `datetime` only).

`failure nullable` the probability that *null* appears.

`failure invalid` the probability that invalid value appears.

## Example

[data.json](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator/data.json)

