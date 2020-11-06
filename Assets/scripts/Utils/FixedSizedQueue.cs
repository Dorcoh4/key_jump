using UnityEngine;
using UnityEditor;
using System.Collections.Concurrent;

namespace Assets.scripts.Utils
{
    public class FixedSizedQueue<T>
    {
        ConcurrentQueue<T> q = new ConcurrentQueue<T>();
        private object lockObject = new object();
        public FixedSizedQueue(int size)
        {
            Limit = size;
        }
        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            lock (lockObject)
            {
                T overflow;
                while (q.Count > Limit && q.TryDequeue(out overflow)) ;
            }
        }
        public void Dequeue()
        {
            q.TryDequeue(out T _);
        }

        public int Count()
        {
            return q.Count;
        }
    }
}