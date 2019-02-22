using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;

namespace ApplicationSettings
{
   /// <summary>
   /// A <see cref="System.Drawing.Design.UITypeEditor">UITypeEditor</see> for editing generic dictionaries in the <see cref="System.Windows.Forms.PropertyGrid">PropertyGrid</see>.
   /// </summary>
   /// <typeparam name="TKey">The type of the Keys in the dictionary.</typeparam>
   /// <typeparam name="TValue">The type of the Values in the dictionary.</typeparam>
   public class GenericDictionaryEditor<TKey, TValue> : CollectionEditor
   {
      /// <summary>
      /// Initializes a new instance of the GenericDictionaryEditor class using the specified collection type.
      /// </summary>
      /// <param name="type">The type of the collection for this editor to edit.</param>
      public GenericDictionaryEditor( Type type )
          : base( type )
      {

      }

      private GenericDictionaryEditorAttribute _mEditorAttribute;

      private CollectionForm _mForm;

      /// <summary>
      /// Creates a new form to display and edit the current collection.
      /// </summary>
      /// <returns>A <see cref="CollectionEditor.CollectionForm"/>  to provide as the user interface for editing the collection.</returns>
      protected override CollectionForm CreateCollectionForm()
      {
         _mForm = base.CreateCollectionForm();
         _mForm.Text = _mEditorAttribute.Title ?? _mForm.Text;

         Type formType = _mForm.GetType();
         PropertyInfo pi = formType.GetProperty( "CollectionEditable", BindingFlags.NonPublic | BindingFlags.Instance );
         pi.SetValue( _mForm, true, null );

         return _mForm;
      }

      /// <summary>
      /// Edits the value of the specified object using the specified service provider and context.
      /// </summary>
      /// <param name="context">An <see cref="ITypeDescriptorContext"/> that can be used to gain additional context information. </param>
      /// <param name="provider">A service provider object through which editing services can be obtained.</param>
      /// <param name="value">The object to edit the value of.</param>
      /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
      public override object EditValue( ITypeDescriptorContext context, IServiceProvider provider, object value )
      {
         if ( context == null )
         {
            throw new ArgumentNullException( nameof( context ) );
         }

         if ( context.PropertyDescriptor != null && context.PropertyDescriptor.Attributes[typeof( GenericDictionaryEditorAttribute )] is GenericDictionaryEditorAttribute attribute )
         {
            _mEditorAttribute = attribute;

            if ( _mEditorAttribute.KeyDefaultProviderType == null )
            {
               _mEditorAttribute.KeyDefaultProviderType = typeof( DefaultProvider<TKey> );
            }

            if ( _mEditorAttribute.ValueDefaultProviderType == null )
            {
               _mEditorAttribute.ValueDefaultProviderType = typeof( DefaultProvider<TValue> );
            }
         }
         else
         {
            _mEditorAttribute = new GenericDictionaryEditorAttribute
            {
               KeyDefaultProviderType = typeof( DefaultProvider<TKey> ),
               ValueDefaultProviderType = typeof( DefaultProvider<TValue> )
            };
         }

         return base.EditValue( context, provider, value );
      }

      /// <summary>
      /// Gets the data type that this collection contains.
      /// </summary>
      /// <returns>The data type of the items in the collection, or an Object if no Item property can be located on the collection.</returns>
      protected override Type CreateCollectionItemType() => typeof( EditableKeyValuePair<TKey, TValue> );

      /// <summary>
      /// Creates a new instance of the specified collection item type.
      /// </summary>
      /// <param name="itemType">The type of item to create.</param>
      /// <returns>A new instance of the specified type.</returns>
      protected override object CreateInstance( Type itemType )
      {
         TKey key;
         TValue value;

         if ( Activator.CreateInstance( _mEditorAttribute.KeyDefaultProviderType ) is DefaultProvider<TKey> keyDefaultProvider )
         {
            key = keyDefaultProvider.GetDefault( DefaultUsage.Key );
         }
         else
         {
            key = default;
         }

         if ( Activator.CreateInstance( _mEditorAttribute.ValueDefaultProviderType ) is DefaultProvider<TValue> valueDefaultProvider )
         {
            value = valueDefaultProvider.GetDefault( DefaultUsage.Value );
         }
         else
         {
            value = default;
         }

         return new EditableKeyValuePair<TKey, TValue>( key, value, _mEditorAttribute );
      }

      /// <summary>
      /// Gets an array of objects containing the specified collection.
      /// </summary>
      /// <param name="editValue">The collection to edit.</param>
      /// <returns>An array containing the collection objects, or an empty object array if the specified collection does not inherit from ICollection.</returns>
      protected override object[] GetItems( object editValue )
      {
         if ( editValue is Dictionary<TKey, TValue> dictionary )
         {
            object[] objArray = new object[dictionary.Count];
            int num = 0;
            foreach ( KeyValuePair<TKey, TValue> entry in dictionary )
            {
               var entry2 = new EditableKeyValuePair<TKey, TValue>( entry.Key, entry.Value, _mEditorAttribute );
               objArray[num++] = entry2;
            }

            return objArray;
         }
         else
         {
            throw new ArgumentNullException( nameof( editValue ), "Get Value Failed" );
         }
      }

      /// <summary>
      /// Sets the specified array as the items of the collection.
      /// </summary>
      /// <param name="editValue">The collection to edit.</param>
      /// <param name="value">An array of objects to set as the collection items.</param>
      /// <returns>The newly created collection object or, otherwise, the collection indicated by the <paramref name="editValue"/> parameter.</returns>
      protected override object SetItems( object editValue, object[] value )
      {
         if ( editValue == null )
         {
            editValue = base.CollectionType.GetConstructor( new Type[] { } ).Invoke( null );
         }
         if ( value == null )
         {
            throw new ArgumentNullException( nameof( value ) );
         }

         if ( !( editValue is Dictionary<TKey, TValue> dictionary ) )
         {
            dictionary = editValue.GetType().GetConstructor( null )?.Invoke( null ) as Dictionary<TKey, TValue>;
         }

         foreach ( EditableKeyValuePair<TKey, TValue> entry in value )
         {
            if ( dictionary != null && dictionary.ContainsKey( entry.Key ) )
            {
               continue;
            }
            dictionary?.Add( entry.Key, entry.Value );
         }

         return dictionary;
      }

      /// <summary>
      /// Retrieves the display text for the given list item.
      /// </summary>
      /// <param name="value">The list item for which to retrieve display text.</param>
      /// <returns>he display text for <paramref name="value"/>.</returns>
      protected override string GetDisplayText( object value )
      {
         if ( value is EditableKeyValuePair<TKey, TValue> pair )
         {
            return string.Format( CultureInfo.CurrentCulture, "{0}", pair.Key );
         }
         else
         {
            return base.GetDisplayText( value );
         }
      }
   }
}
