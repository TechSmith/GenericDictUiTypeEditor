# GenericDictUiTypeEditor
Allows editing of generic dictionary items for Project Application Settings

# Project Description
A UITypeEditor for editing generic dictionaries (Dictionary<TKey, TValue>) in the PropertyGrid.

## Features include:

 - 100% managed code (C#), derived from System.ComponentModel.Design.CollectionEditor
 - Customizable editor form title
 - Custom Editors for Key and/or Value
 - Custom TypeConverters for Key and/or Value
 - Custom DefaultProviders for Key and/or Value
 - Custom AttributeProviders for Key and/or Value
 - Custom DisplayNames for Key and/or Value

# Example 1: 
```C#
public class Example
{
   [Editor(typeof(DictionaryEditor<string, string>), typeof(UITypeEditor))]
   [GenericDictionaryEditor(Title="This is an example",ValueEditorType=typeof(FileNameEditor))]
   public Dictionary<string, string> ExampleDict {get;set;}

   [Editor(typeof(DictionaryEditor<int, Example>), typeof(UITypeEditor))]
   [GenericDictionaryEditor(ValueConverterType=typeof(ExpandableObjectConverter))]
   public Dictionary<int, Example> ExampleDict2 {get;set;}

   public Example()
   {
      ExampleDict = new Dictionary<string, string>();
      ExampleDict2 = new Dictionary<int, Example>();
   }
}
```

## Example 2
```C#
[Serializable()]
[Editor(typeof(DictionaryEditor<int, List<string>>), typeof(UITypeEditor))]
[GenericDictionaryEditor(ValueConverterType=typeof(ExpandableObjectConverter))]
public class CustomDictionary : Dictionary<string, List<string>>
{
  public override ToString()
  {
    JsonConvert.SerializeObject(this);
  }
}
```