using System;
using System.Collections.Generic;
public class PriorityQueue<TElement> {

    private List<(int, TElement)> _heapArray;
    private Dictionary<TElement, int> _mapping;


    public PriorityQueue() {
        _heapArray = new List<(int, TElement)>();
        _mapping = new Dictionary<TElement, int>();
    }

    public bool IsEmpty() {
        return _heapArray.Count == 0;
    }

    public void Clear() {
        _heapArray.Clear();
        _mapping.Clear();
    }

    public bool Contains(TElement element) {
        return _mapping.ContainsKey(element);
    }

    public void Remove(TElement element) {
        throw new NotImplementedException();
    }

    public void Enqueue(TElement element, int priority) {
        _heapArray.Add((priority, element));
        int index = _heapArray.Count - 1;
        _mapping.Add(element, index);
        _heapifyUp(index);
    }

    public TElement Dequeue() {
        if (IsEmpty()) throw new InvalidOperationException("Priority Queue is empty");
        TElement result = _heapArray[0].Item2;
        int end = _heapArray.Count - 1;
        _mapping.Remove(result);
        _heapArray[0] = _heapArray[end];
        _heapArray.RemoveAt(end);
        _heapifyDown(0);

        return result;
    }

    public TElement Peek() {
        return IsEmpty() ? default(TElement) : _heapArray[0].Item2;
    }

    private void _heapifyDown(int index) {
        int leftIndex = _left(index);
        int rightIndex = _right(index);
        int smallestIndex = index;

        if (leftIndex < _heapArray.Count && _heapArray[leftIndex].Item1 < _heapArray[smallestIndex].Item1) {
            smallestIndex = leftIndex;
        }

        if (rightIndex < _heapArray.Count && _heapArray[rightIndex].Item1 < _heapArray[smallestIndex].Item1) {
            smallestIndex = rightIndex;
        }

        if (smallestIndex != index) {
            _swap(index, smallestIndex);
            _heapifyDown(smallestIndex);
        }
    }

    private void _heapifyUp(int index) {
        if (index == 0) return;
        int parentIndex = _parent(index);
        if (_heapArray[index].Item1 < _heapArray[parentIndex].Item1) {
            _swap(index, parentIndex);
            _heapifyUp(parentIndex);
        }
    }

    private void _swap(int index1, int index2) {
        _mapping[_heapArray[index1].Item2] = index2;
        _mapping[_heapArray[index2].Item2] = index1;

        (int, TElement) temp = default;
        temp = _heapArray[index1];
        _heapArray[index1] = _heapArray[index2];
        _heapArray[index2] = temp;
    }

    private int _parent(int i) {
        return (i - 1) / 2;
    }

    private int _left(int i) {
        return 2 * i + 1;
    }

    private int _right(int i) {
        return 2 * i + 2;
    }
}