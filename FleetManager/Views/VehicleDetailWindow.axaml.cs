using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FleetManager.Models;
using FleetManager.Services;
using System;

namespace FleetManager.Views;

public partial class VehicleDetailWindow : Window
{
    private readonly IVehicleService _vehicleService;

    public VehicleDetailWindow()
    {
        InitializeComponent();
        _vehicleService = new JsonVehicleService();
    }

    public VehicleDetailWindow(Vehicle vehicle)
    {
        InitializeComponent();

        _vehicleService = new JsonVehicleService();

        DataContext = vehicle;

        var comboBox = this.FindControl<ComboBox>("StatusComboBox");
        if (comboBox != null)
        {
            comboBox.ItemsSource = Enum.GetValues(typeof(VehicleStatus));
        }
    }

    private async void RefuelButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not Vehicle vehicle)
            return;

        await _vehicleService.RefuelAsync(vehicle, 100);

        vehicle.FuelLevel = 100;
    }
    
    
    private async void StatusComboBoxChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is not Vehicle vehicle)
            return;

        if (sender is not ComboBox comboBox)
            return;

        if (comboBox.SelectedItem is not VehicleStatus newStatus)
            return;

        await _vehicleService.ChangeStatusAsync(vehicle, newStatus);

        vehicle.Status = newStatus;
    }

    
    private void CloseButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}