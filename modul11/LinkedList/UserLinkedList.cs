namespace LinkedList
{
    class Node
    {
        public Node(User data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public User Data;
        public Node Next;
    }

    class UserLinkedList
    {
        private Node first = null!;

        public void AddSorted(User user)
        {
            if (first == null!)
            {
                Node firstNode = new Node(user, first);
                first = firstNode;
                return;
            }
            
            Node? currentNode = first;
            Node? previousNode = null!;
            
            while (currentNode != null! && 
                   string.Compare(currentNode.Data.Name, user.Name, StringComparison.OrdinalIgnoreCase) < 0) //hvis den nye kommer efter current i alfabetet 
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            if (previousNode == null!)
            {
                Node newNode = new Node(user, currentNode!);
                first = newNode;
            }

            else
            {
                Node newNode = new Node(user, previousNode.Next);
                previousNode.Next = newNode;
            }
            
        }
        
        public void AddFirst(User user)
        {
            Node node = new Node(user, first);
            first = node;
        }

        public User RemoveFirst()
        {
            if (first == null!)
                return null!;
            Node removedNode = first;
            first = first.Next;
            return removedNode.Data;
        }

        public void RemoveUser(User user)
        {
            Node currentNode = first;
            Node previousNode = null!;
            bool found = false;

            while (!found && currentNode != null!)
            {
                if (currentNode.Data.Name == user.Name)
                {
                    found = true;
                    if (currentNode == first)
                    {
                        RemoveFirst();
                    }
                    else
                    {
                        previousNode.Next = currentNode.Next;
                    }
                }
                else
                {
                    //vi spoler - vi sætter den nuværende node til at være den tidligere og går 1 frem
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
            }
        }

        public User GetFirst()
        {
            return first.Data;
        }

        public User GetLast()
        {
            if (first == null!)
                return null!;
            while (first.Next != null!)
            {
                first = first.Next;
            }
            return first.Data;
        }

        public int CountUsers()
        {
            Node currentNode = first;
            if (first == null!)
                return -1;
            int count = 0;
            while (currentNode != null!)
            {
                count++;
                currentNode = currentNode.Next;
            }
            return count;
        }

        public override String ToString()
        {
            Node node = first;
            String result = "";
            while (node != null)
            {
                result += node.Data.Name + ", ";
                node = node.Next;
            }
            return result.Trim();
        }

        public bool? ContainsUser(User user)
        {
            Node currentNode = first;
            if (first == null!)
                return null!;
            while (currentNode != null!)
            {
                if (currentNode.Data.Name == user.Name)
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }
    }
    
}