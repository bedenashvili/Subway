
namespace Subway
{
    public class PathFinder
    {
        private readonly IDataStorage _data;
        private readonly string? _firstStation;
        private readonly string? _lastStation;
        private readonly List<string> _uniqueStations;
        private readonly List<List<string>> _subway;

        public PathFinder(IDataStorage storage, string? firstStation, string? lastStation, List<string> uniqueStations, List<List<string>> subway)
        {
            _data = storage;
            _firstStation = firstStation;
            _lastStation = lastStation;
            _uniqueStations = uniqueStations;
            _subway = subway;
        }

        private int GetStationIndex(string station)
        {
            return _uniqueStations.IndexOf(station);
        }
        private void GetAdjacentStations(Queue<int> queue, List<int> visited, int index, List<int> predecessor)
        {
            for (int i = 0; i < _data.Storage.GetLength(0); i++)
            {
                if (_data.Storage[index, i] == true)
                {
                    if (visited.Contains(i) == false && queue.Contains(i) == false && predecessor.Contains(i) == false)
                    {
                        queue.Enqueue(i);
                        predecessor.Add(index);
                    }
                }
            }
        }
        public List<string?> GetPath()
        {
            int firstIndex = GetStationIndex(_firstStation!);
            int lastIndex = GetStationIndex(_lastStation!);

            Queue<int> queue = new();
            List<int> visited = new();
            List<int> previous = new();
            List<string?> route = new();

            queue.Enqueue(firstIndex);

            while (queue.Count != 0)
            {
                int index = queue.Dequeue();
                GetAdjacentStations(queue, visited, index, previous);
                visited.Add(index);
            }

            int checker = -1;

            while (checker != firstIndex)
            {
                int queueIndexLast = visited.IndexOf(lastIndex);
                route.Add(_uniqueStations[lastIndex]);
                int previousIndex = previous[queueIndexLast - 1];
                lastIndex = previousIndex;
                checker = previousIndex;
            }
            route.Add(_firstStation);
            route.Reverse();            
            return route;
        }
        public int GetTransfersCount(List<string?> route)
        {
            List<List<int>> subwayTransfers = new();

            foreach (var station in route)
            {
                List<int> stationTransfers = new();

                for (int i = 0; i < _subway.Count; i++)
                {
                    for (var j = 0; j < _subway[i].Count; j++)
                    {
                        if (_subway[i][j] == station && !stationTransfers.Contains(i))
                        {
                            stationTransfers.Add(i);
                        }
                    }
                }
                subwayTransfers.Add(stationTransfers);
            }
            int transfersCount = CheckTransfers(subwayTransfers);
            return transfersCount;
        }
        private static int CheckTransfers(List<List<int>> subwayTransfers)
        {
            int transferCount = 0;

            for (int i = 1; i < subwayTransfers.Count - 1; i++)
            {
                for (int j = 0; j < subwayTransfers[i].Count; j++)
                {
                    if (subwayTransfers[i - 1].Contains(subwayTransfers[i][j]))
                    {
                        if (subwayTransfers[i + 1].Contains(subwayTransfers[i][j]) == false)
                        {
                            transferCount++;
                            break;
                        }
                    }

                }
            }
            return transferCount;
        }
    }
}
