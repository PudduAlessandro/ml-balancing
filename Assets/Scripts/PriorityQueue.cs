using System;
using System.Collections.Generic;

// AI generated
public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> elements = new List<T>();

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
        int i = elements.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (elements[i].CompareTo(elements[parent]) >= 0)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty");

        T frontItem = elements[0];
        int lastIndex = elements.Count - 1;
        elements[0] = elements[lastIndex];
        elements.RemoveAt(lastIndex);

        int i = 0;
        while (true)
        {
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;
            int smallestChild = 0;

            if (leftChild < elements.Count)
            {
                smallestChild = leftChild;

                if (rightChild < elements.Count && elements[rightChild].CompareTo(elements[leftChild]) < 0)
                    smallestChild = rightChild;

                if (elements[i].CompareTo(elements[smallestChild]) <= 0)
                    break;

                Swap(i, smallestChild);
                i = smallestChild;
            }
            else
                break;
        }

        return frontItem;
    }

    private void Swap(int i, int j)
    {
        T temp = elements[i];
        elements[i] = elements[j];
        elements[j] = temp;
    }
}
