namespace Utilities;

public static class ListExtensions
{
    private static Random _random;

    static ListExtensions()
    {
        _random = new Random();
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        return list[_random.Next() % list.Count];
    }
}