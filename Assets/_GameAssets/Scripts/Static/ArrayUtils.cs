using System;

public static class ArrayUtils
{
    private static readonly Random staticRandom = new Random();
    public static void Shuffle<T>(this T[] array)
    {
        Random random = new Random(staticRandom.Next());
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}