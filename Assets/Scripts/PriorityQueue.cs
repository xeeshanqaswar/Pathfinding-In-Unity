using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Implemeted using binary heap
leftChild = parent * 2 + 1;
RightChild = parent * 2 + 2;
parent = Mathf.Floor((child - 1) / 2)
 */

public class PriorityQueue<T> where T:IComparable<T>
{

    List<T> data;

    public int Count { get { return data.Count; } }

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int childIndex = data.Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (data[childIndex].CompareTo(data[parentIndex]) >= 0)
            {
                break;
            }

            T temp = data[childIndex];
            data[childIndex] = data[parentIndex]; 
            data[parentIndex] = temp;

            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        int lastIndex = data.Count - 1;

        T firstItem = data[0];

        data[0] = data[lastIndex];
        data.RemoveAt(lastIndex);
        lastIndex--;

        int parentIndex = 0;

        while (true)
        {
            int childIndex = (parentIndex * 2) + 1;

            if (childIndex > lastIndex) // means invalid Child Index
            {
                break;
            }

            int rightIndex = childIndex + 1;
            if (rightIndex <= lastIndex && data[rightIndex].CompareTo(data[childIndex]) < 0)
            {
                childIndex = rightIndex;
            }

            if (data[parentIndex].CompareTo(data[childIndex]) <= 0)
            {
                break;
            }

            T tmp = data[parentIndex];
            data[parentIndex] = data[childIndex];
            data[childIndex] = tmp;

            parentIndex = childIndex;
        }
        return firstItem;
    }

    public T Peak()
    {
        return data[0];
    }

    public bool Contains(T item)
    {
        return data.Contains(item);
    }

    public List<T> ToList()
    {
        return data;
    }

}
