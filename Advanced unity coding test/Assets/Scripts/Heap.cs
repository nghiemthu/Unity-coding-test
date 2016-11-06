using UnityEngine;
using System.Collections;

public class Heap<T> where T: IHeapItem<T> 
{
	T[] items;
	int currentItemCount = 0;

	public Heap(int maxItemCount)
	{
		items = new T[maxItemCount];
	}

	public void Add(T item)
	{
		item.heapIndex = currentItemCount;
		items [currentItemCount] = item;
		SortUp (items[currentItemCount]);
		currentItemCount++;

	}

	public T RemoveFirstItem()
	{
		T firstItem;
		firstItem = items [0];
		currentItemCount--;
		items [0] = items [currentItemCount];
		items [0].heapIndex = 0;
		SortDown (items[0]);
		return firstItem;
	}

	public bool Contains(T item)
	{
		return Equals (item, items[item.heapIndex]);
	}

	public void SortUp(T item)
	{
		while(true)
		{
			int parentIndex = item.heapIndex / 2 - 1;
			if (parentIndex > 0) {
				if (item.CompareTo (items [parentIndex]) < 0) {
					Swap (item, items [parentIndex]);
				} else
					return;
			} else
				return;
		}

	}

	public void SortDown(T item)
	{
		while(true)
		{
			int leftChildIndex = item.heapIndex * 2 + 1;
			int rightChildIndex = item.heapIndex * 2 + 2;

			if (leftChildIndex < currentItemCount) {
				int swapIndex = leftChildIndex;
				if (rightChildIndex < currentItemCount) {
					if (items [rightChildIndex].CompareTo (items [leftChildIndex]) < 0) {
						swapIndex = rightChildIndex;
					}
				}

				if (item.CompareTo (items [swapIndex]) > 0) {
					Swap (item, items [swapIndex]);
				} else
					return;

			} else
				return;
		}
	}

	public void Swap(T itemA, T itemB)
	{
		items [itemA.heapIndex] = itemB;
		items [itemB.heapIndex] = itemA;

		int temp = itemA.heapIndex;
		itemA.heapIndex = itemB.heapIndex;
		itemB.heapIndex = temp;
	}
}

