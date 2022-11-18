# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

Generates data randomly based on 1) json file, 2) class attributes, 3) fluent profiles (since version 1.4). 

Key features:
* Calculated properties with custom rules.
* Random failures.

[Project documentation](https://github.com/akovanev/DataGenerator/wiki)

**History**:

Originally this library was created in order to prepare json mocked data for testing api, which was under development. Api was supposed to aggregate data from a bunch of backend systems, while the client had to import several hundred thousand objects to a new frontend cms. It was extremely important to make sure that all data format surprises, which we were getting, could have been handled properly. That's why **random failures** were added to `DG`. As well as random sized arrays. At the same time, it was impossible to import an object if all properties contained completely random data. This is where **calculated properties** hepled. 

After that I didn't work with `DG` for almost two years. During this time a couple of nice tools, helping out with fake data, appeared. Some time ago, I was working on the other issue, where data couldn't be too random again. I realized, that `DG` may get its second chance and got back to updating it with new features, primarily focusing on building a more convenient setup, based on the fluent syntax.

`DG`, at this moment, is quite far from being optimal in terms of performance as well as in code experience, test coverage or documentation. But if you see that there could be nice features added, feel free to create a github issue.  


**Changelog**: 
* Versions **1.0 - 1.2** are not supported. 
* Version **1.3** mapping of type `T` based on attributes from `Akov.DataGenerator.Attributes` added. 
* Version **1.3.1** min array size support added, range validation for `DgLength`, `DgSpacesCount`, `DgFailure` added on attributes level.
* Version **1.4.0** fluent support added.
* Version **1.4.1** object generation interface added.
* Version **1.5.0** assigning of calculated properties added to fluent support.

## Author's blog

Articles temporarily available on https://github.com/akovanev/kovanev.github.io/tree/master/_posts

[Data Generator](https://akovanev.com/blogs/2020/08/26/data-generator/) - a quick introduction.

[Custom Data Generator](https://akovanev.com/blogs/2020/08/27/custom-data-generator/) - creating a custom generator based on the library.

[Calculated Properties with Data Generator](https://akovanev.com/blogs/2020/08/31/calculated-properties-with-data-generator/) - calculated properties and files as custom data sources.

[Data Generator Attributes](https://akovanev.com/blogs/2020/09/07/data-generator-attributes) - using the program approach.

[Integration Testing with Data Generator](https://akovanev.com/blogs/2020/09/08/integration-testing-with-data-generator) - shows how to mock and test solutions with the generated data.

[Fluent support profile example](https://github.com/akovanev/DataGenerator/blob/feature/1.4/Akov.DataGenerator.Demo/StudentsSampleTests/Tests/Profiles/StudentsTestProfile.cs) - an alternative to using attributes.

