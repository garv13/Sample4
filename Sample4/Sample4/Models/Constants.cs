using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sample4.Models
{
    class Constants
    {
        public const string DatabaseFilename = "TodoSQLite.db3";

        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
