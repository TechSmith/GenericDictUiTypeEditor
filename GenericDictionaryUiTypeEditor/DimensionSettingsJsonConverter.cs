using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApplicationSettings
{
   public class DimensionSettingsJsonConverter : ExpandableObjectConverter
   {
      public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
      {
         return sourceType == typeof( string ) || sourceType == typeof( Dictionary<ProjectDimensionType, DimensionInfoList> ) || base.CanConvertFrom( context, sourceType );
      }

      public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType )
      {
         return destinationType == typeof( string ) || base.CanConvertTo( context, destinationType );
      }

      public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
      {
         if ( value == null )
         {
            return new DimensionSettings();
         }

         if ( value is string jsonData )
         {
            var resultObject = JObject.Parse( jsonData ).ToObject<DimensionSettings>();
            return resultObject;
         }

         if ( value is Dictionary<ProjectDimensionType, DimensionInfoList> data )
         {
            return new DimensionSettings( data );
         }

         return base.ConvertFrom( context, culture, value );

      }

      public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType )
      {
         if ( value == null && destinationType == typeof( string ) )
         {
            return JsonConvert.SerializeObject( new DimensionInfoList() );
         }

         if ( value == null && destinationType == typeof( Dictionary<ProjectDimensionType, DimensionInfoList> ) )
         {
            return new DimensionInfoList();
         }

         if ( destinationType == typeof( Dictionary<ProjectDimensionType, DimensionInfoList> ) )
         {
            return value as Dictionary<ProjectDimensionType, DimensionInfoList>;
         }

         if ( destinationType == typeof( string ) )
         {
            return JsonConvert.SerializeObject( value );
         }
         else
         {
            return base.ConvertTo( context, culture, value, destinationType );
         }
      }
   }
}
