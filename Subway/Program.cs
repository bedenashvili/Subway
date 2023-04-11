using Subway;

internal class Program
{
    private static void Main(string[] args)
    {
        List<List<string>> subway = new()
        {
            new() { "A", "B", "C", "D", "E", "F" },
            new() { "N", "L", "D", "J", "O" },
            new() { "C", "J", "E", "M", "L", "K", "C" },
            new() { "B", "H", "J", "F", "G" }
        };
        List<string> uniqueStations = UniqueStations.GetList(subway);
        uniqueStations.Sort();

        int count = uniqueStations.Count;
        IDataStorage data = new DataStorage(count, subway, uniqueStations);

        //for(int i = 0; i < data.Storage.GetLength(0); i++)
        //{
        //    for(int j = 0; j < data.Storage.GetLength(1); j++)
        //    {
        //        Console.Write($"{data.Storage[i, j]}\t");
        //    }
        //    Console.WriteLine();
        //}
        Console.WriteLine("Start station:");
        string? first = GetStationFromUser(uniqueStations);

        Console.WriteLine("Finish station:");
        string? last = GetStationFromUser(uniqueStations);

        PathFinder path = new(data, first, last, uniqueStations, subway);
        List<string?> route = path.GetPath();

        Console.WriteLine("The route is:");

        foreach (var station in route)
        {
            Console.Write($"{station}\t");
        }
        int transfersCount = path.GetTransfersCount(route);

        Console.WriteLine($"\nTrasnfers count: {transfersCount}");
        Console.ReadKey();
    }
    static string? GetStationFromUser(List<string> uniqueStations)
    {
        string? userInput;
        while (true)
        {
            Console.Write("\nPlease specify a subway station:\t");
            userInput = Console.ReadLine()?.ToUpper();

            if (userInput != null && !uniqueStations.Contains(userInput.ToUpper()))
            {
                Console.WriteLine("The station is not found, please try again");
            }

            else
            {
                break;
            }
        }        

        Console.WriteLine();

        return userInput;
    }
}