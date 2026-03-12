using Avalonia.Controls;
using Avalonia.Input;

namespace SortVisualizer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // dirty but works :D
        var input = this.FindControl<NumericUpDown>("SpeedInput");
        var input2 = this.FindControl<NumericUpDown>("NumberInput");
        input!.KeyDown += (s, e) => 
        {
            bool isNumber = (e.Key >= Key.D0 && e.Key <= Key.D9) || 
                            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                            e.Key == Key.Back || e.Key == Key.Delete;
        
            if (!isNumber)
            {
                e.Handled = true;
            }
        };
        input2!.KeyDown += (s, e) => 
        {
            bool isNumber = (e.Key >= Key.D0 && e.Key <= Key.D9) || 
                            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                            e.Key == Key.Back || e.Key == Key.Delete;
        
            if (!isNumber)
            {
                e.Handled = true;
            }
        };
    }
}