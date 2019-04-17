# prestoq-code-exercise
[![](https://ci.appveyor.com/api/projects/status/m8qnf0d1n91mo5eb?svg=true)](https://ci.appveyor.com/project/AndrewOberhardt/prestoq-code-exercise)

My response to [https://github.com/prestoqinc/code-exercise-services](https://github.com/prestoqinc/code-exercise-services)


## Instructions
 1. Clone the project locally
 2. Open the solution (PrestoQServicesEncoding.sln) in Visual Studio
 3. Build
 4. Run - this will execute the PrestoQServicesEncodingExample project, read [input-sample.txt](https://github.com/aoby/prestoq-code-exercise/blob/master/PrestoQServicesEncodingExample/input-sample.txt) and generate output-sample.json in the executing directory (e.g. bin/Debug/netcoreapp2.1)

 
## Contents
### PrestoQServicesEncoding (library)
This library contains the ProductRecord class, ProductCatalogReader to read and parse a store product info file into ProductRecord objects, and various enums and supporting code.

### PrestoQServicesEncodingTest (tests)
Unit tests for PrestoQServicesEncoding

### PrestoQServicesEncodingExample (executable)
This is a simple executable that reads input-sample.txt with the ProductCatalogReader to generate a list of ProductRecords and serializes them to a json file (output-sample.json).