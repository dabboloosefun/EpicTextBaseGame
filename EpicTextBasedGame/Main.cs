public static void Main()
{
    public static class Program
    {
        public static void Main()
        {
            Cow utku = new Cow("Utku");
            utku.Moo();
            Gator jerry = new Gator(51);
            jerry.Kill();
            Gator bob = new Gator(49);
            bob.Kill();
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

    public class Gator
    {
        int Deadlyness;
        public Gator(int deadlyness)
        {
            Deadlyness = deadlyness;
        }
        public void Kill()
        {
            Console.WriteLine(Deadlyness > 50 ? "He rips your throat" : "He a shleepy boi");
        }
    }
}
