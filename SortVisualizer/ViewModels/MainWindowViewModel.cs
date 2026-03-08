using System.Collections.ObjectModel;
using SortVisualizer.Models;

namespace SortVisualizer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<SortingElement> SortingElements { get; }

    public MainWindowViewModel()
    {
        
    }
}