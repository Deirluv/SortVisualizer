using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using SortVisualizer.Infrastructure;

namespace SortVisualizer.Models;

public class BubbleSortStrategy : ISortStrategy 
{
    public string Name { get; } = "Bubble Sort";
    public async Task SortAsync(ObservableCollection<SortingElement> sortingElements, int speed, AudioService audioService, CancellationToken cancellationToken)
    {
        var elementCount = sortingElements.Count;
        for (var i = 0; i < elementCount; i++)
        {
            for (var j = 0; j < elementCount - i - 1; j++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                sortingElements[j].Color = "Red";
                sortingElements[j + 1].Color = "Red";
                if (sortingElements[j].Value > sortingElements[j + 1].Value)
                {
                    audioService.PlayTone(sortingElements[j].Value);
                    (sortingElements[j].Value, sortingElements[j + 1].Value) = (sortingElements[j + 1].Value, sortingElements[j].Value);
                }
                await Task.Delay(speed, cancellationToken).ContinueWith(t => {});
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                sortingElements[j].Color = "Blue";
                sortingElements[j + 1].Color = "Blue";
            }

            sortingElements[elementCount - i - 1].Color = "Green";
        }
    }
}