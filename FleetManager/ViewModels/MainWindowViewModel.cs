using System;
using System.Collections.ObjectModel;
using System.IO;
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

        Console.WriteLine($"Loaded vehicles: {vehicles.Count}");
        Console.WriteLine(File.Exists("vehicles.json"));

        Vehicles.Clear();
        foreach (var v in vehicles)
            Vehicles.Add(v);
    }
}