using System;

namespace GenericDictionaryUiTypeEditor
{
   public class AbstractCollectionEditorAttribute : Attribute
   {
      public Type[] DerivedTypesToEdit { get; set; }

      public AbstractCollectionEditorAttribute( params Type[] derivedTypesToEdit )
      {
         DerivedTypesToEdit = derivedTypesToEdit;
      }
   }
}
