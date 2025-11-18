namespace Sortering;

public static class MergeSort
{

    public static void Sort(int[] array)
    {
        _mergeSort(array, 0, array.Length - 1);
    }

    private static void _mergeSort(int[] array, int l, int h)
    {
        if (l < h)
        {
            int m = (l + h) / 2;
            _mergeSort(array, l, m);
            _mergeSort(array, m + 1, h);
            Merge(array, l, m, h);
        }
    }

    private static void Merge(int[] array, int low, int middle, int high)
    {
        int n1 = middle - low + 1;   // størrelse venstre bunke
        int n2 = high - middle;      // størrelse højre bunke

        int[] left = new int[n1];
        int[] right = new int[n2];

        // Kopiér venstre bunke
        for (int i = 0; i < n1; i++)
            left[i] = array[low + i];

        // Kopiér højre bunke
        for (int j = 0; j < n2; j++)
            right[j] = array[middle + 1 + j];

        int l = 0; // index i venstre bunke
        int r = 0; // index i højre bunke
        int k = low; // index i det samlede array

        // Flet bunkerne tilbage i array
        while (l < n1 && r < n2)
        {
            if (left[l] <= right[r])
            {
                array[k] = left[l];
                l++;
            }
            else
            {
                array[k] = right[r];
                r++;
            }
            k++;
        }

        // Hvis der er rester i venstre bunke
        while (l < n1)
        {
            array[k] = left[l];
            l++;
            k++;
        }

        // Hvis der er rester i højre bunke
        while (r < n2)
        {
            array[k] = right[r];
            r++;
            k++;
        }
    }

}
