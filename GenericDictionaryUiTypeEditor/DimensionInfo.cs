namespace ApplicationSettings
{
   public class DimensionInfo
   {
      private string _resourceKeyName;
      public string ResourceKeyName
      {
         get => _resourceKeyName;
         set
         {
            _resourceKeyName = value??string.Empty;
            if ( _resourceKeyName != string.Empty )
            {
               DimensionName = WPFElementsRes.WPFResources.Manager.GetString( value );
            }
         }
      }
      public string DimensionName { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
      public bool ShowDimension { get; set; }
      public string GetDisplayString()
      {
         string displayString = DimensionName;
         if ( ShowDimension )
         {
            displayString += $" ({Width}x{Height})";
         }

         return displayString;
      }
      public override string ToString()
      {
         return GetDisplayString();
      }
   }
}
