using System;
using System.Collections.Generic;
using System.Linq;

public static class CustomUtility
{
    static Random rng = new Random();

    public static int[] GetRandomIntArray(int start, int length)
    {
        var array = Enumerable.Range(start, length).ToArray();
        ShuffleArray(ref array);
        return array;
    }

    public static void ShuffleArray<T>(ref T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static void ShuffleList<T>(ref List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
