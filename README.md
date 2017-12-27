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

It is posible to customise behaviour of the comparator by using constructor with optional parameter

    var objectComparator = new ObjectComparer<ExampleSimpleClass>(new ComparerParameters()
            {
                Ignore = new List<string> { "StringValue" },
                Flags = new List<ComparerFlags>(),
                Properties = new List<ComparerProperties>() {
                    new ComparerProperties() {
                          Name = "AnotherStringValue",
                          Flags = new List<ComparerFlags>() { ComparerFlags.CaseInsensitive }
                        }
                }
            });
    
The available options are:

* **Ignore** - specify properties that should be skipped during comparison
* **Flags** - specify options. For now the only available options is **CaseInsensitive**
* **Properties** - specify options for property. For now the only available options is **CaseInsensitive**
