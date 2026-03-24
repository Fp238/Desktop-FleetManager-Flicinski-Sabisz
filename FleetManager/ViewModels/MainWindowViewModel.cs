using System.Collections.ObjectModel;
using ReactiveUI;
using FleetManager.Models;
using FleetManager.Services;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private readonly IVehicleService _vehicleService;

    public ObservableCollection<Vehicle> Vehicles { get; } = new();

    public MainWindowViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        LoadVehiclesAsync();
    }

    private async void LoadVehiclesAsync()
    {
        var vehicles = await _vehicleService.GetVehiclesAsync();

        Vehicles.Clear();
        foreach (var v in vehicles)
            Vehicles.Add(v);
    }
}