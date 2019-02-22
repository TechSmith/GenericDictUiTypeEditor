using System.Collections.Generic;
using System.ComponentModel;

namespace ApplicationSettings
{
   [Editor( "System.ComponentModel.Design.CollectionEditor", "System.Drawing.Design.UITypeEditor" )]
   public class DimensionInfoList : List<DimensionInfo>
   {
      public DimensionInfoList() { }
      public DimensionInfoList( int capacity )
         : base( capacity ) { }
      public DimensionInfoList( IEnumerable<DimensionInfo> collection )
         : base( collection ) { }
   }
}