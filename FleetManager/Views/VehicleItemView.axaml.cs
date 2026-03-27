using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using FleetManager.Models;
using FleetManager.ViewModels;
using FleetManager.Views;

namespace FleetManager.Views;

public partial class VehicleItemView : UserControl
{
    public VehicleItemView()
    {
        InitializeComponent();

        this.PointerPressed += VehicleItemView_PointerPressed;
    }

    
    private async void VehicleItemView_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
        {
            var window = new VehicleDetailWindow(vehicle);
            await window.ShowDialog((Window)this.VisualRoot!);

            if (this.VisualRoot is Window mainWindow &&
                mainWindow.DataContext is MainWindowViewModel vm)
            {
                await vm.ReloadVehicles();
            }
        }
    }

    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}