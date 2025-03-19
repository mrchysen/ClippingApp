using Core.Models.DoubleLinkedLists;

namespace Tests.Core.Models.DoubleLinkedLists;

public class DoubleLinkedListTests
{
    [Fact]
    public void Add_Element_Success()
    {
        // Arrange
        DoubleLinkedList<int> list = new();

        // Act
        list.Add(1);

        //Assert
        Assert.True(list.Head.Value == 1);
        Assert.True(list.Head == list.Tail);
    }

    [Fact]
    public void Add_Elements_Success()
    {
        // Arrange
        DoubleLinkedList<int> list = new();

        // Act
        list.Add(1);
        list.Add(2);
        list.Add(3);

        // Assert
        Assert.True(list.Head.Value == 1);
        Assert.True(list.Head.Next.Next.Next.Value == 1);
        Assert.True(list.Head.Next.Value == 2);
        Assert.True(list.Head == list.Head.Next.Next.Next);
        Assert.True(list.Tail == list.Head.Next.Next);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Foreach_IterateItems_Success(int itemsCount)
    {
        // Arrange
        DoubleLinkedList<int> list = new();
        var count = 0;
        
        for (int i = 0; i < itemsCount; i++)
        {
            list.Add(i);
        }

        // Act
        foreach (int item in list)
        {
            count++;
        }

        // Assert
        Assert.True(count == itemsCount);
    }

    [Fact]
    public void AddAfter_AddElement_Success()
    {
        // Arrange
        DoubleLinkedList<int> list = new();
        list.Add(1); // Head
        list.Add(2); // Head.Next
        list.Add(4); // Head.Next.Next
        var node = list.Head.Next;

        // Act
        list.AddAfter(2, 3);

        // Assert
        Assert.True(list.Head.Value == 1);
        Assert.True(list.Head.Next.Value == 2);
        Assert.True(list.Head.Next.Next.Value == 3);
        Assert.True(list.Head.Next.Next.Next.Value == 4);
        Assert.True(list.Count == 4);
    }

    [Fact]
    public void AddAfter_EmptyList_ItemAddedToHeadAndTail()
    {
        // Arrange
        DoubleLinkedList<int> list = new();

        // Act
        list.AddAfter(5, 3);

        // Assert
        Assert.True(list.Head.Value == 3);
        Assert.True(list.Tail.Value == 3);
        Assert.True(list.Tail == list.Head);
    }

    [Fact]
    public void AddAfter_NegativePossition_ErrorThrown()
    {
        // Arrange
        DoubleLinkedList<int> list = new();
        bool isArgumentOutOfRangeException = false;

        // Act
        try
        {
            list.AddAfter(-1, 3);
        }
        catch (Exception ex) 
        {
            isArgumentOutOfRangeException = ex is ArgumentOutOfRangeException;
        }

        // Assert
        Assert.True(isArgumentOutOfRangeException);
    }

    [Fact]
    public void AddAfter_AddToFirstPossiont_SuccessfulyAdded()
    {
        // Arrange
        DoubleLinkedList<int> list = new();
        list.Add(2);
        list.Add(3);

        // Act
        list.AddAfter(0, 1);

        // Assert
        Assert.True(list.Head.Value == 1);
        Assert.True(list.Head.Next.Value == 2);
        Assert.True(list.Head.Next.Next.Value == 3);
        Assert.True(list.Count == 3);
    }
}
