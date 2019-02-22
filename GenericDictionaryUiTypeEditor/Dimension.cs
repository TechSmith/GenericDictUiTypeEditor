namespace ApplicationSettings
{
   public class Dimension
   {
      public int CurrentSize { get; }
      public bool IsFixedSize { get; }
      public int MinimumSize { get; }

      public Dimension( int currentSize, int minimumSize, bool isFixedSize )
      {
         CurrentSize = currentSize;
         IsFixedSize = isFixedSize;
         MinimumSize = minimumSize;
      }
   }
}
