using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;

namespace SFPCalculator
{
    internal static class DBManager
    {
        public static List<T> GetData<T>(string Path)
        {
            Stream DB = Assembly.GetExecutingAssembly().GetManifestResourceStream(Path) ?? File.OpenRead(Path);
            using (var Reader = new StreamReader(DB))
            using (var csv = new CsvReader(Reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }

        public static bool SetData<T>(string FilePath, List<T> Records)
        {
            try
            {
                using (var Writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(Writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(Records);
                }
            }
            catch (Exception e) { throw e; }
            return true;
        }
    }
}
