using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SortVisualizer.Infrastructure;

public class AsyncRelayCommand : ICommand
{
    private readonly Func<Task> _execute;
    private bool _isExecuting;
    
    public AsyncRelayCommand(Func<Task> execute)
    {
        _execute = execute;
    }
    
    public bool CanExecute(object? parameter)
    {
        return _isExecuting == false;
    }

    public async void Execute(object? parameter)
    {
        _isExecuting = true;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        try
        {
            await _execute();
        }
        finally
        {
            _isExecuting = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? CanExecuteChanged;
}