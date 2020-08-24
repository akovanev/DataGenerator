# Markdown syntax guide

## Execution

```
DataGenerator <filename.json>
```

In case of success the result will be stored to <filename.json>.out.json

## Annotation

### General

`objectCount` the count of Objects to be generated.

`attributesCount`the count of attributes per each Object.

`outPropertiesName` the name of root item in the generated json.

`outAttributesName` the name of attributes array item.

`templates`the array of templates.

`properties` the Object properties.

`attributes` the array of the Object attributes.

### Template types

* `string` for the string value.
  * `pattern` specifies accessible characters. The space will be added separately.

* `double` for the double value.
  * `separator` specifies the data format.
 
* `set` only a values from the set allowed.
  * `pattern` the set of values.

* `datetime` for the datetime.
  * `pattern` specifies the data format.


### Properties & Attributes

`name` the Object property name.

`template` the template name.

`minLength` minimum output data length (for `string` only).

`maxLength` maximum output data length (for `string` only).

`minSpaceCount` minimum count of spaces (for `string` only).

`maxSpaceCount` maximum count of spaces (for `string` only).

`minValue` minimum value (for `double` and `datetime` only).

`maxValue` maximum value (for `double` and `datetime` only).

`Failure.Nullable` the value N represents every N number that will be *null*. For properties N equal to the serial object number, for attributes the count of attributes is taken into account.

`Failure.Nullable` the value N represents every N number that will be *!@~*. For properties N equal to the serial object number, for attributes the count of attributes is taken into account.

## Example

[data.json](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator/data.json)

