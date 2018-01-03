using System;
using System.Configuration;

namespace Book.Dao
{
    public static class ContextString
    {
        public const String NAME = "BookContext";

        /// <summary>
        /// Default behavior.
        /// Load connection string by name of "BookContext".
        /// </summary>
        /// <returns></returns>
        public static String Default()
        {
            return Resolve(NAME);
        }

        /// <summary>
        /// Load connection string by name.
        /// 
        /// 1. Seek in AppSettings.
        /// 2. If (1) failed, seek in ConnectionStrings.
        ///     (1) failed means: null, empty, or consists only of white-space chars.
        /// </summary>
        /// <param name="name">The name of setting key</param>
        /// <returns>Connection String, or null if failed.</returns>
        public static String Resolve(String name)
        {
            var @return = ConfigurationManager.AppSettings[name]?.ToString();
            if (String.IsNullOrWhiteSpace(@return))
            {
                @return = ConfigurationManager.ConnectionStrings[name]?.ToString();
            }

            return @return;
        }
    }
}
