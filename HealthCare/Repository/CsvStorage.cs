using System.Collections.Generic;

namespace HealthCare.Repository
{
    public class CsvStorage<T> where T : ISerializable, new()
    {

        private string _filepath;

        public CsvStorage(string filepath)
        {
            _filepath = filepath;
        }

        public List<T> Load()
        {
            return Serializer<T>.FromCSV(_filepath);
        }

        public void Save(List<T> objects)
        {
            Serializer<T>.ToCSV(_filepath, objects);
        }
    }
}
