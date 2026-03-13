using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SortVisualizer.Infrastructure;

namespace SortVisualizer.Models;

public interface ISortStrategy
{
    public string Name { get; }
    Task SortAsync(ObservableCollection<SortingElement> list, int speed, AudioService audioService);
}