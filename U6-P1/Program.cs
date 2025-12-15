using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 1. SCENARIO CONFIGURATION
        // We generate a massive amount of data so the difference is noticeable.
        int dataCount = 10000000; // 10 Million records
        Console.WriteLine($"--- GENERATING {dataCount:N0} RANDOM DATA POINTS ---");

        int[] data = new int[dataCount];
        Random rnd = new Random();

        // Fill the array
        for (int i = 0; i < dataCount; i++)
        {
            data[i] = rnd.Next(0, dataCount * 2); // Numbers between 0 and 20 million
        }

        // We choose a number we KNOW exists (the last one for the worst linear case)
        // Note: In a real scenario, the number could be anywhere.
        int target = data[dataCount - 1];
        Console.WriteLine($"Target to find: {target}");
        Console.WriteLine("--------------------------------------------------\n");

        // 2. LINEAR SEARCH TEST
        Console.WriteLine("STARTING LINEAR SEARCH...");
        Stopwatch sw = Stopwatch.StartNew();

        int linearIndex = LinearSearch(data, target);

        sw.Stop();
        Console.WriteLine($"[Linear] Found at index: {linearIndex}");
        Console.WriteLine($"[Linear] Time elapsed: {sw.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine("Justification: Acceptable for small data, but slow here.");
        Console.WriteLine("\n--------------------------------------------------\n");

        // 3. BINARY SEARCH TEST
        // To use binary search, we MUST sort FIRST.
        Console.WriteLine("PREPARING BINARY SEARCH (SORTING DATA)...");
        sw.Restart();

        Array.Sort(data); // The cost of sorting

        sw.Stop();
        Console.WriteLine($"[Sorting] Preparation time: {sw.Elapsed.TotalMilliseconds} ms");

        Console.WriteLine("STARTING BINARY SEARCH...");
        sw.Restart();

        int binaryIndex = BinarySearch(data, target);

        sw.Stop();
        Console.WriteLine($"[Binary] Found at index (post-sort): {binaryIndex}");
        Console.WriteLine($"[Binary] Time elapsed: {sw.Elapsed.TotalMilliseconds} ms"); // This will be almost 0
        Console.WriteLine("Justification: Once sorted, search is instant.");

        Console.ReadKey();
    }

    // Linear Search Algorithm O(n)
    static int LinearSearch(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value) return i;
        }
        return -1;
    }

    // Binary Search Algorithm O(log n)
    static int BinarySearch(int[] array, int value)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (array[mid] == value)
                return mid;

            if (array[mid] < value)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }
}