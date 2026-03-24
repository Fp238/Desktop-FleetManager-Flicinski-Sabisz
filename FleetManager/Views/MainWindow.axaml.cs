using Avalonia.Controls;
using FleetManager.Services;
using FleetManager.ViewModels;

namespace FleetManager;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var service = new JsonVehicleService();
        DataContext = new MainWindowViewModel(service);
    }
}