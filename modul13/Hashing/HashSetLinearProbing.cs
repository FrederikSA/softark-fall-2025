using Hashing;

public class HashSetLinearProbing : HashSet {
    private Object[] buckets;
    private int currentSize;
    private bool[] isDeleted;
    private enum State { DELETED }

    public HashSetLinearProbing(int bucketsLength) {
        buckets = new Object[bucketsLength];
        isDeleted = new bool[bucketsLength];
        currentSize = 0;
    }

    public bool Contains(object x)
    {
        int h = HashValue(x);
        int index = h;
        int length = buckets.Length;

        while (buckets[index] != null)
        {
            if (!isDeleted[index] && buckets[index].Equals(x))
            {
                return true;
            }

            index = (index + 1) % length;

            if (index == h) 
            {
                break;
            }
        }

        return false;
    }

    public bool Add(object x)
    {
        int h = HashValue(x);
        int index = h;
        int length = buckets.Length;

        int firstDeletedIndex = -1;

        // Først: find plads eller tjek om x allerede findes
        while (buckets[index] != null)
        {
            // Hvis værdien allerede findes (og ikke er slettet)
            if (!isDeleted[index] && buckets[index].Equals(x))
                return false; // findes allerede

            // Husk første slettede plads
            if (isDeleted[index] && firstDeletedIndex == -1)
                firstDeletedIndex = index;

            index = (index + 1) % length;

            if (index == h)
                break;
        }

        // Hvis eksisterer en DELETED-plads
        if (firstDeletedIndex != -1)
        {
            buckets[firstDeletedIndex] = x;
            isDeleted[firstDeletedIndex] = false;
            currentSize++;
            return true;
        }

        // Ellers indsæt i den første null-plads
        if (buckets[index] == null)
        {
            buckets[index] = x;
            isDeleted[index] = false;
            currentSize++;
            return true;
        }

        return false; // tabellen er fuld
    }


    public bool Remove(object x)
    {
        int h = HashValue(x);
        int index = h;
        int length = buckets.Length;

        while (buckets[index] != null)
        {
            // Fundet og ikke slettet
            if (!isDeleted[index] && buckets[index].Equals(x))
            {
                isDeleted[index] = true;   // tombstone
                currentSize--;
                return true;
            }

            index = (index + 1) % length;

            if (index == h)
                break;
        }

        return false; // ikke fundet
    }


    public int Size() {
        return currentSize;
    }

    private int HashValue(Object x) {
        int h = x.GetHashCode();
        if (h < 0) {
            h = -h;
        }
        h = h % buckets.Length;
        return h;
    }

    public override String ToString() {
        String result = "";
        for (int i = 0; i < buckets.Length; i++) {
            int value = buckets[i] != null && !buckets[i].Equals(State.DELETED) ? 
                    HashValue(buckets[i]) : -1;
            result += i + "\t" + buckets[i] + "(h:" + value + ")\n";
        }
        return result;
    }

}
