using HealthCare.Repository;
using System.Collections.Generic;

namespace HealthCare.Service
{
    public abstract class Service<T> where T : Identifier, ISerializable, new()
    {
        protected Repository<T> _repository;

        public Service(string filepath)
        {
            _repository = new Repository<T>(filepath);
        }

        // can return null if not found
        public T? TryGet(object id)
        {
            return _repository.Get(id);
        }

        // must return an object
        public T Get(object id)
        {
            T? item = _repository.Get(id);
            if (item is null) throw new KeyNotFoundException();
            return item;
        }

        public void Add(T item)
        {
            _repository.Add(item);
        }

        public void Remove(object id)
        {
            _repository.Remove(id);
        }

        public void Update(T item)
        {
            _repository.Update(item);
        }

        public bool Contains(object id)
        {
            return _repository.Contains(id);
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public List<T> GetAll()
        {
            return _repository.Items();
        }
    }
}
