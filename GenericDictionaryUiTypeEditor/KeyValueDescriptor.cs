using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace ApplicationSettings
{
    internal class KeyValueDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _pd;

        private readonly Type _mConverterType;
        private readonly Type _mEditorType;
        private readonly Type _mAttributeProviderType;

       public KeyValueDescriptor(PropertyDescriptor pd, Type converterType, Type editorType, Type attributeProviderType, string displayName)
            : base(pd)
        {
            _pd = pd;

            _mConverterType = converterType;
            _mEditorType = editorType;
            _mAttributeProviderType = attributeProviderType;
            DisplayName = displayName;
        }

      public override string DisplayName { get; }

       public override bool CanResetValue( object component ) => _pd.CanResetValue( component );

      public override Type ComponentType => _pd.ComponentType;

      public override object GetValue( object component ) => _pd.GetValue( component );

      public override bool IsReadOnly => _pd.IsReadOnly;

      public override Type PropertyType => _pd.PropertyType;

      public override void ResetValue( object component ) => _pd.ResetValue( component );

      public override void SetValue( object component, object value ) => _pd.SetValue( component, value );

      public override bool ShouldSerializeValue( object component ) => _pd.ShouldSerializeValue( component );

      public override TypeConverter Converter
        {
            get
            {
                if (_mConverterType != null)
            {
               return Activator.CreateInstance(_mConverterType) as TypeConverter;
            }
            else
                {
                    return TypeDescriptor.GetConverter(PropertyType);
                }
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (_mEditorType != null)
         {
            return Activator.CreateInstance(_mEditorType) as UITypeEditor;
         }
         else
         {
            return TypeDescriptor.GetEditor(PropertyType, typeof(UITypeEditor));
         }
      }

        public override AttributeCollection Attributes
        {
            get
            {
                if (_mAttributeProviderType != null)
                {
                    return (Activator.CreateInstance(_mAttributeProviderType) as AttributeProvider)?.GetAttributes(PropertyType) ?? throw new InvalidOperationException();
                }
                else
                {
                    return TypeDescriptor.GetAttributes(PropertyType);
                }
            }
        }
    }
}
