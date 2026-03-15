using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using SortVisualizer.Infrastructure;

namespace SortVisualizer.Models;

public class InsertionSortStrategy : ISortStrategy 
{
    public string Name { get; } = "Insertion Sort";
    public async Task SortAsync(ObservableCollection<SortingElement> sortingElements, int speed, AudioService audioService, CancellationToken cancellationToken)
    {
        int n = sortingElements.Count;

        for (int i = 1; i < n; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            var key = sortingElements[i].Value;
            int j = i - 1;
            sortingElements[i].Color = "Yellow"; 

            while (j >= 0 && sortingElements[j].Value > key)
            {
                sortingElements[j].Color = "Red";
                audioService.PlayTone(sortingElements[j].Value);
                sortingElements[j + 1].Value = sortingElements[j].Value;
                await Task.Delay(speed, cancellationToken).ContinueWith(t => {});
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                sortingElements[j].Color = "Blue";
                j--;
            }
            
            sortingElements[j + 1].Value = key;
            for (int k = 0; k <= i; k++) {
                sortingElements[k].Color = "Green";
            }
        }
    }
}