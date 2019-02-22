using System;

namespace ApplicationSettings
{

   /// <summary>
   /// Provides default values for the Key or Value properties for new dictionary entries
   /// </summary>
   /// <typeparam name="T">The type of the Key or Value</typeparam>
   public class DefaultProvider<T>
    {
        /// <summary>
        /// Returns a default value for the Key or Value properties for new dictionary entries
        /// </summary>
        /// <param name="usage">Specifies if the desired default value is to be used as Key or Value</param>
        /// <returns>Returns a value of type T to be used as the default</returns>
        /// <remarks>If the default value is to be used as Key it may NOT be null (because the Dictionary doesn't allow null as Key)</remarks>
        public virtual T GetDefault(DefaultUsage usage)
        {
            Type t = typeof(T);
            if (t.IsPrimitive || t.IsEnum)
         {
            return default(T);
         }
         else if (t == typeof(string))
         {
            return (T)(object)string.Empty;
         }
         else
         {
            return Activator.CreateInstance<T>();
         }
      }
    }
}
