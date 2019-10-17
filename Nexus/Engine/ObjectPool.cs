using System;
using System.Collections.Concurrent;

/*
 * -- To Implement a New Pool --
 * ObjectPool<MyClass> pool = new ObjectPool<MyClass>(() => new MyClass());
 * 
 * -- Retrieving a new object from the pool --
 * MyClass mc = pool.GetObject();
 * 
 * -- Returning an object to the pool --
 * pool.ReturnObject(mc);
 */

namespace Nexus.Engine {

	// Generic Object Pool Class
	public class ObjectPool<T> {

		// ConcurrentBag used to store and retrieve objects from Pool.
		private ConcurrentBag<T> _objects;
		private Func<T> _objectGenerator;

		// Object pool contructor used to get a delegate for implementing instance initialization or retrieval process
		public ObjectPool(Func<T> objectGenerator) {
			if(objectGenerator == null) throw new ArgumentNullException("objectGenerator");
			_objects = new ConcurrentBag<T>();
			_objectGenerator = objectGenerator;
		}

		// GetObject retrieves the object from the object pool (if already exists) or else creates an instance of object and returns (if not exists)
		public T GetObject() {
			T item;
			if(_objects.TryTake(out item)) return item;
			return _objectGenerator();
		}

		// ReturnObject stores back the object back to pool.
		public void ReturnObject(T item) {
			_objects.Add(item);
		}
	}
}
