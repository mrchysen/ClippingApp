using System.Collections;

namespace Core.Models.DoubleLinkedLists;

public class DoubleLinkedListEnumerator<T> : IEnumerator<T>
{
    private readonly DoubleLinkedList<T> _list;
    private Node<T> _currentNode;
    private int _index = -1;

    public DoubleLinkedListEnumerator(DoubleLinkedList<T> list)
    {
        _list = list;
    }

    public T Current => _currentNode.Value;

    object IEnumerator.Current => Current ?? throw new NullReferenceException();

    public void Dispose() { }

    public bool MoveNext() 
    {
        _currentNode = _currentNode is null ? _list.Head : _currentNode.Next;
        _index++;

        return _index < _list.Count;
    }

    public void Reset()
    {
        _currentNode = _list.Head;
        _index = 0;
    }
}
