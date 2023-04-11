
namespace Subway
{
    public class DataStorage : IDataStorage
    {
        private readonly int _size;
        private readonly List<List<string>> _stations;
        private readonly List<string> _uniqueStations;

        public bool[,] Storage { get; }
        public DataStorage(int size, List<List<string>> stations, List<string> uniqueStations)
        {
            _size = size;
            _stations = stations;
            _uniqueStations = uniqueStations;
            Storage = GetStorage();
        }

        private bool[,] GetStorage()
        {
            bool[,] matrix = new bool[_size, _size];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = i; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = GetIntersectFromDb(_uniqueStations[i], _uniqueStations[j]);
                    matrix[j, i] = matrix[i, j];
                }
            }
            return matrix;
        }
        private static bool IsFirstStation(List<string> line, string station, int column)
        {
            string? first = line.FirstOrDefault();
            string? last = line.LastOrDefault();

            if ((first == last && column > 0)
                || first == null)
            {
                return false;
            }

            return station.Equals(first);
        }
        private static bool IsLastStation(List<string> line, string station, int column)
        {
            string? last = line.LastOrDefault();
            string? first = line.FirstOrDefault();

            if ((first == last && column == 0)
                || first == null)
            {
                return false;
            }

            return station.Equals(last);
        }
        private static bool IsConnected(int column, string targetStation, List<string> line, string secondStation, string stationFromDb)
        {
            bool result = false;

            if (targetStation == stationFromDb)
            {
                if (IsFirstStation(line, line[column], column))
                {
                    if (line[column + 1] == secondStation)
                    {
                        result = true;
                    }
                }
                else if (IsLastStation(line, line[column], column))
                {
                    if (line[column - 1] == secondStation)
                    {
                        result = true;
                    }
                }
                else
                {
                    if (line[column - 1] == secondStation || (line[column + 1] == secondStation))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        private bool GetIntersectFromDb(string targetStation, string secondStation)
        {
            bool result = false;

            for (int i = 0; i < _stations.Count; i++)
            {
                for (int j = 0; j < _stations[i].Count; j++)
                {
                    string stationFromDb = _stations[i][j];

                    result = IsConnected(j, targetStation, _stations[i], secondStation, stationFromDb);

                    if (result == true)
                    {
                        break;
                    }
                }
                if (result == true)
                {
                    break;
                }
            }
            return result;
        }
    }
}