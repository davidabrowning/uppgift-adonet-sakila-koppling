namespace ADOnetSakilaKoppling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Huvudmeny");
                Console.WriteLine("1. Sök efter skådespelarens förnamn");
                Console.WriteLine("2. Sök efter skådespelarens efternamn");
                Console.WriteLine("3. Sök efter både förnamn och efternamn");
                Console.WriteLine("4. Lista ut alla skådespelare");
                Console.WriteLine("5. Avsluta programmet");

                switch(Console.ReadLine().Trim())
                {
                    case "1":
                        Console.Write("Ange förnamn: ");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Ange efternamn: ");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Ange förnamn: ");
                        Console.ReadLine();
                        Console.Write("Ange efternamn: ");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Listar ut alla skådespelare");
                        
                        Console.ReadLine();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Oväntad inmatning. Försök igen.");
                        break;
                }
            }
        }
    }
}
