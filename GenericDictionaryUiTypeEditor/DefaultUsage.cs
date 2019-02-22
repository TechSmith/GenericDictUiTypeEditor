namespace ApplicationSettings
{
   /// <summary>
   /// Used to specify whether the requested default value is to be used as Key or Value of a dictionary entry
   /// </summary>
   /// <remarks>If the default value is to be used as Key it may NOT be null (because the Dictionary doesn't allow null as Key)</remarks>
   public enum DefaultUsage
    {
        /// <summary>
        /// The requested default value is to be used as Key in the dictionary
        /// </summary>
        Key,
        /// <summary>
        /// The requested default value is to be used as Value in the dictionary
        /// </summary>
        Value
    }
}
