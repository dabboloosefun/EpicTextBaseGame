public static class Program{
    public static void Main(){
        Cow utku = new("Utku");
        utku.Moo();
    }

    
}

public class Cow{
    string Name;
    public Cow(string name){
        Name = name;
    }
    public void Moo(){
        Console.WriteLine("Moo!");
    }
}
