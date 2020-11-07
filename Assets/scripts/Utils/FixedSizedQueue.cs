using UnityEngine;
using UnityEditor;
using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Assets.scripts.Utils
{
    public class FixedSizedQueue<T>
    {
        public List<T> list;
        public FixedSizedQueue(int size)
        {
            Limit = size;
        }
        public int Limit { get; set; }
        public void Add(T obj)
        {
            list.Add(obj);
            if (list.Count == Limit)
            {
                list.RemoveAt(0);
            }
            
        }
        public void Remove(T toRemove)
        {
            list.Remove(toRemove);
        }

        public T this[int i]
        {
            get => list[i];
        }

        public int Count { get => list.Count; }
    }
}