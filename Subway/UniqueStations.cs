
namespace Subway
{
    public class UniqueStations
    {        
        public static List<string> GetList(List<List<string>> stations)
        {
            List<string> uniqueStations = new();

            foreach (var lines in stations)
            {
                foreach (var station in lines)
                {
                    if (!uniqueStations.Contains(station))
                    {
                        uniqueStations.Add(station);
                    }
                }
            }
            return uniqueStations;
        }
    }
}