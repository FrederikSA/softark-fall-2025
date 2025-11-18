using Hashing;

public class HashSetChaining : HashSet
{
    private Node[] buckets;
    private int currentSize;

    private class Node
    {
        public Node(Object data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public Object Data { get; set; }
        public Node Next { get; set; }
    }

    public HashSetChaining(int size)
    {
        buckets = new Node[size];
        currentSize = 0;
    }

    public bool Contains(Object x)
    {
        int h = HashValue(x);
        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }
        return found;
    }

    public bool Add(Object x)
    {
        // rehash hvis mere end 75% fyldt
        double loadFactor = (double)currentSize / buckets.Length;
        if (loadFactor > 0.75)
        {
            Rehash();
        }
        
        int h = HashValue(x);

        Node bucket = buckets[h];
        bool found = false;
        while (!found && bucket != null)
        {
            if (bucket.Data.Equals(x))
            {
                found = true;
            }
            else
            {
                bucket = bucket.Next;
            }
        }

        if (!found)
        {
            Node newNode = new Node(x, buckets[h]);
            buckets[h] = newNode;
            currentSize++;
        }

        return !found;
    }

    public bool Remove(Object x)
    {
        int h = HashValue(x); // h svarer til hashværdien for objekter, som = det index objektet står på i vores bucket array
        // buckets[h] vil altid tage den første node i den linkedlist der ligger på h's index
        Node current = buckets[h];
        Node previous = null;

        // Gå igennem kæden i den pågældende bucket
        while (current != null)
        {
            if (current.Data.Equals(x))
            {
                // Hvis det er første node i bucketen
                if (previous == null)
                {
                    buckets[h] = current.Next;
                }
                else
                {
                    // Spring den nuværende node over
                    previous.Next = current.Next;
                }

                currentSize--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        // Hvis vi kommer herned, fandt vi ikke x
        return false;
    }

    private int HashValue(Object x)
    {
        int h = x.GetHashCode();
        if (h < 0)
        {
            h = -h;
        }
        h = h % buckets.Length;
        return h;
    }

    public int Size()
    {
        return currentSize;
    }
    
    private void Rehash()
    {
        Node[] oldBuckets = buckets;
        buckets = new Node[oldBuckets.Length * 2];
        currentSize = 0; // Add indsætter igen og tæller op

        foreach (Node head in oldBuckets)
        {
            Node temp = head;
            while (temp != null)
            {
                Add(temp.Data);  // bliver rehash'et korrekt baseret på ny størrelse
                temp = temp.Next;
            }
        }
    }

    public override String ToString()
    {
        String result = "";
        for (int i = 0; i < buckets.Length; i++)
        {
            Node temp = buckets[i];
            if (temp != null)
            {
                result += i + "\t";
                while (temp != null)
                {
                    result += temp.Data + " (h:" + HashValue(temp.Data) + ")\t";
                    temp = temp.Next;
                }
                result += "\n";
            }
        }
        return result;
    }
}
