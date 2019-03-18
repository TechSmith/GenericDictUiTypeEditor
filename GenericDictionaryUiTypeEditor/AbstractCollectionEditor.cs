using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace GenericDictionaryUiTypeEditor
{
   public class AbstractCollectionEditor : CollectionEditor
   {

      private Type[] _supportedTypesList;
      private readonly Type _type;

      public AbstractCollectionEditor( Type type ) : base( type )
      {
         _type = type;
      }

      protected override Type[] CreateNewItemTypes()
      {
         var attribute = Attribute.GetCustomAttribute( _type, typeof( AbstractCollectionEditorAttribute ) );
         if ( attribute != null && attribute is AbstractCollectionEditorAttribute abstractCollectionEditor)
         {
            _supportedTypesList = abstractCollectionEditor.DerivedTypesToEdit;
         }
         else
         {
            _supportedTypesList = new[] {_type};
         }
         return _supportedTypesList;
      }

      protected override CollectionForm CreateCollectionForm()
      {
         CollectionForm form = base.CreateCollectionForm();
         Type type = form.GetType();
         PropertyInfo propertyInfo = type.GetProperty( "CollectionEditable", BindingFlags.Instance | BindingFlags.NonPublic );
         propertyInfo?.SetValue( form, true );
         return form;
      }
   }
}