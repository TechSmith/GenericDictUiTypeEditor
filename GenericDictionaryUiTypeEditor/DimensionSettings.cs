using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApplicationSettings
{
   [TypeConverter( typeof( DimensionSettingsJsonConverter ) )]
   [Editor( typeof( GenericDictionaryEditor<ProjectDimensionType, DimensionInfoList> ), typeof( System.Drawing.Design.UITypeEditor ) )]
   [Serializable]
   public class DimensionSettings : Dictionary<ProjectDimensionType, DimensionInfoList>
   {
      protected DimensionSettings( SerializationInfo info, StreamingContext context )
         : base( info, context )
      {
      }


      internal DimensionSettings( Dictionary<ProjectDimensionType, DimensionInfoList> data )
      {

         foreach ( var keyValuePair in data )
         {
            if ( this.ContainsKey( keyValuePair.Key ) )
            {
               this[keyValuePair.Key] = keyValuePair.Value;
            }
            else
            {
               this.Add( keyValuePair.Key, keyValuePair.Value );
            }
         }
      }

      public DimensionSettings()
      {
      }

      public override string ToString()
      {
         return JsonConvert.SerializeObject( this );
      }
   }
}
