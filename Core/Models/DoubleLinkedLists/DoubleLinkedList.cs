using Core.Models.Points;
using Core.Models.Polygons;
using System.Collections;

namespace Core.Models.DoubleLinkedLists;

public class DoubleLinkedList<T> : IEnumerable<T>
{
    public Node<T> Head { get; set; }
    public Node<T> Tail { get; set; }
    public int Count { get; set; }

    public void Add(T item)
    {
        if(Head == null)
        {
            Head = new Node<T>();
            Tail = Head;
            Head.Value = item;
            Head.Next = Head;
            Head.Prev = Tail;

            Count++;
        }
        else
        {
            var newNode = new Node<T>();
            newNode.Value = item;
            newNode.Prev = Tail;
            newNode.Next = Head;
            Tail.Next = newNode;
            Tail = newNode;
            
            Count++;
        }
    }

    public void Add(Node<T> item)
    {
        if (Head == null)
        {
            Head = item;
            Tail = Head;
            Head.Next = Head;
            Head.Prev = Tail;

            Count++;
        }
        else
        {
            item.Prev = Tail;
            item.Next = Head;
            Tail.Next = item;
            Tail = item;

            Count++;
        }
    }

    public void AddAfter(int possition, T item)
    {
        if (possition < 0)
            throw new ArgumentOutOfRangeException();

        if (possition >= Count)
        {
            Add(item);
            return;
        }

        if(possition == 0)
        {
            var newFirstNode = new Node<T>();
            newFirstNode.Value = item;

            newFirstNode.Next = Head;
            newFirstNode.Prev = Tail;

            Head.Prev = newFirstNode;
            Tail.Next = newFirstNode;

            Head = newFirstNode;

            Count++;

            return;
        }

        // 0 < possition < Count
        var newNode = new Node<T>();
        newNode.Value = item;

        var current = Head;
        var index = 0;

        while (index < possition)
        {
            current = current.Next;
            index++;
        }

        var previousNode = current.Prev;

        previousNode.Next = newNode;
        current.Prev = newNode;
        newNode.Next = current;
        newNode.Prev = previousNode;
        
        Count++;
    }

    public IEnumerator<T> GetEnumerator() 
        => new DoubleLinkedListEnumerator<T>(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
