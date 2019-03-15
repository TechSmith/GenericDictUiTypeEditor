using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using GenericDictionaryUiTypeEditor;

namespace ApplicationSettings
{
   public class AbstractCollectionEditor : CollectionEditor
   {

      private readonly Type[] _supportedTypesList;

      public AbstractCollectionEditor( Type type ) : base( type )
      {
         var attribute = Attribute.GetCustomAttribute( type, typeof( AbstractCollectionEditorAttribute ) );
         if ( attribute != null && attribute is AbstractCollectionEditorAttribute abstractCollectionEditor)
         {
            _supportedTypesList = abstractCollectionEditor.DerivedTypesToEdit;
         }
         else
         {
            _supportedTypesList = new[] {type};
         }
      }

      protected override Type[] CreateNewItemTypes()
      {
         return _supportedTypesList;
      }
   }
}