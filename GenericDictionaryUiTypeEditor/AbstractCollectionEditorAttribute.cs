using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
