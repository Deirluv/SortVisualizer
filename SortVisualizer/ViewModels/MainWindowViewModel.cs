using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SortVisualizer.Infrastructure;
using SortVisualizer.Models;
 
namespace SortVisualizer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly AudioService _audioService = new();
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
        ShuffleCommand = new RelayCommand(Shuffle);
        StartSortCommand = new AsyncRelayCommand(StartSortAsync);
        Shuffle();
    }

    private void Shuffle()
    {
        SortingElements.Clear();
        for (int i = 0; i < (NumberOfElements ?? 10); i++)
        {
            SortingElements.Add(new SortingElement { Value = Random.Shared.Next(1, 200), Color = "Blue"});
        }
    }

    private async Task StartSortAsync()
    {
        var elementCount = SortingElements.Count;
        for (var i = 0; i < elementCount; i++)
        {
            for (var j = 0; j < elementCount - i - 1; j++)
            {
                SortingElements[j].Color = "Red";
                SortingElements[j + 1].Color = "Red";
                if (SortingElements[j].Value > SortingElements[j + 1].Value)
                {
                    _audioService.PlayTone(SortingElements[j].Value);
                    (SortingElements[j].Value, SortingElements[j + 1].Value) = (SortingElements[j + 1].Value, SortingElements[j].Value);
                }
                await Task.Delay(Speed ?? 20);
                SortingElements[j].Color = "Blue";
                SortingElements[j + 1].Color = "Blue";
            }

            SortingElements[elementCount - i - 1].Color = "Green";
        }
    }
    
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    
}