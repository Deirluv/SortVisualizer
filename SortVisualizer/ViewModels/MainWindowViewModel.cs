using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SortVisualizer.Models;

namespace SortVisualizer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<SortingElement> SortingElements { get; }
    
    public ICommand ShuffleCommand { get; }
    public ICommand StartSortCommand { get; }

    public MainWindowViewModel()
    {
        ShuffleCommand = new RelayCommand(Shuffle);
        // StartSortCommand = new AsyncRelayCommand(StartSortCommand);
        Shuffle();
    }

    private void Shuffle()
    {
        
    }

    private async Task StartSortAsync()
    {
        
    }
    
}