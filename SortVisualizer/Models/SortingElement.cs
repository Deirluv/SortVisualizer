using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SortVisualizer.Models;

public class SortingElement : INotifyPropertyChanged
{
    private int _value;
    private string? _color;
    public event PropertyChangedEventHandler? PropertyChanged;

    public int Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
    
    public string Color
    {
        get => _color;
        set => SetField(ref _color, value);
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}