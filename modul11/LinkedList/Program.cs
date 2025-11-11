using LinkedList;

User kristian = new User("Kristian", 1);
User mads = new User("Mads", 2);
User torill = new User("Torill", 3);
User kell = new User("Kell", 4);
User henrik = new User("Henrik", 5);
User klaus = new User("Klaus", 6);
User anders = new User("Anders", 7);

UserLinkedList list = new UserLinkedList();
list.AddSorted(kristian);
list.AddSorted(mads);
list.AddSorted(torill);
list.AddSorted(henrik);
list.AddSorted(klaus);
list.AddSorted(anders);

Console.WriteLine(list.CountUsers());
Console.WriteLine(list);

list.RemoveUser(mads);
list.RemoveFirst();

Console.WriteLine(list.CountUsers());
Console.WriteLine(list);

Console.WriteLine(list.ContainsUser(kristian)); 