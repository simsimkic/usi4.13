using System.Collections.Generic;
using System.IO;

namespace HealthCare.Repository
{
    public static class Serializer<T> where T : ISerializable, new()
    {
        private static readonly char _delimiter = ',';

        public static void ToCSV(string filepath, List<T> objects)
        {
            ValidateFile(filepath);

            using (StreamWriter streamWriter = File.CreateText(filepath))
            {
                foreach (T obj in objects)
                {
                    string line = string.Join(_delimiter.ToString(), obj.ToCSV());
                    streamWriter.WriteLine(line);
                }
            }
        }

        public static List<T> FromCSV(string filepath)
        {
            ValidateFile(filepath);

            List<T> objects = new List<T>();
            foreach (string line in File.ReadLines(filepath))
            {
                if (line.Trim() == "") continue;

                string[] csvValues = line.Split(_delimiter);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }

        public static void ValidateFile(string filepath)
        {
            if (!File.Exists(filepath))
                File.Create(filepath).Dispose();
        }
    }
}