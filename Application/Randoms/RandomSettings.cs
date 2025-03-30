namespace Application.Randoms;

public class RandomSettings
{
    public RandomArea Area { get; set; } = new();
}

public class RandomArea
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int Width { get; set; } = 15;
    public int Height { get; set; } = 15;
}
