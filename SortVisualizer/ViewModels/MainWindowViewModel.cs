using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SortVisualizer.Infrastructure;
using SortVisualizer.Models;
 
namespace SortVisualizer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private CancellationTokenSource? _cancellationTokenSource;
    
    private readonly AudioService _audioService = new();
    
    public List<ISortStrategy> SortingStrategies { get; } = new()
    {
        new BubbleSortStrategy(),
        new InsertionSortStrategy()
    };
    
    private ISortStrategy? _selectedSortingStrategy;

    public ISortStrategy? SelectedSortingStrategy
    {
        get => _selectedSortingStrategy;
        set => SetField(ref _selectedSortingStrategy, value);
    }
    
    public ObservableCollection<SortingElement> SortingElements { get; } = new();
    
    public ICommand ShuffleCommand { get; }
    public ICommand StartSortCommand { get; }

    private int? _speed;

    public int? Speed
    {
        get => _speed;
        set => SetField(ref _speed, value);
    }
    
    private int? _numberOfElements;

    public int? NumberOfElements
    {
        get => _numberOfElements;
        set => SetField(ref _numberOfElements, value);
    }

    public MainWindowViewModel()
    {
        SelectedSortingStrategy = SortingStrategies[0];
        ShuffleCommand = new RelayCommand(Shuffle);
        StartSortCommand = new AsyncRelayCommand(StartSortAsync);
        Shuffle();
    }

    private void Shuffle()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        SortingElements.Clear();
        for (var i = 0; i < (NumberOfElements ?? 10); i++)
        {
            SortingElements.Add(new SortingElement { Value = Random.Shared.Next(1, 200), Color = "Blue"});
        }
    }

    private async Task StartSortAsync()
    {
        if (SelectedSortingStrategy == null) return;
        try
        {
            await SelectedSortingStrategy.SortAsync(SortingElements, Speed ?? 20, _audioService,
                _cancellationTokenSource!.Token);
        }
        catch (OperationCanceledException) { }
    }
    
    
    
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    
}