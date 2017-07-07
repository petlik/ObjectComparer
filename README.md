# ObjectComparer

ObjectComparer (as name sugest) is a tool that check if objects are equal and creates a list of properties that are different.

## Basic example

    var objectA = new SampleClass()
    {
      IntegerValue = 10,
      StringValue = "Hello"
    };

    var objectB = new SampleClass()
    {
      IntegerValue = 10,
      StringValue = "Hello world"
    };

    var objectComparator = ObjectComparator<SampleClass>();

    var result = objectComparator.Compare(objectA, objectA);
    // result.AreEqual - false
    // result.Differences - { "StringValue" }
      
## Options

You are able to customise behaviour of the comparator by using constructor with option parameter

    var objectComparator = ObjectComparator<SampleClass>(new ObjectComparatorSettings() {
      Ignore = new List<string> { "StringValue" },
	  Parameters = new List<PropertiesParametersFlags>(),
      PropertiesParameters = new List<PropertiesParameters>() {
        new PropertiesParameters() {
          Name = "AnotherStringValue",
          Flags = new List<PropertiesParametersFlags>() { PropertiesParametersFlags.CaseInsensitive }
        }
      }
    });
    
The available options are:

* **Ignore** - specify properties that should be skipped during comparison
* **Parameters** - specify options. For now the only available options is **CaseInsensitive**
* **PropertiesParameters** - specify options for property. For now the only available options is **CaseInsensitive**
