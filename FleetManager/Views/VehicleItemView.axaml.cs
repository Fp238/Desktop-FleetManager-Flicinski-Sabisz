using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using FleetManager.Models;
using FleetManager.Views;

namespace FleetManager.Views;

public partial class VehicleItemView : UserControl
{
    public VehicleItemView()
    {
        InitializeComponent();

        this.PointerPressed += VehicleItemView_PointerPressed;
    }

    private void VehicleItemView_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is Vehicle vehicle)
        {
            var window = new VehicleDetailWindow(vehicle);
            window.Show();
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}