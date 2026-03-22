using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;


public class JsonVehicleService : IVehicleService
{
    private const string FilePath="vehicles.json";

    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new List<Vehicle>();

            var json = await File.ReadAllTextAsync(FilePath);

            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json);

            return vehicles ?? new List<Vehicle>();
        }
        catch
        {
            return new List<Vehicle>();
        }
    }

    
    public async Task SaveVehiclesAsync(List<Vehicle> vehicles)
    {
        try
        {
            var json = JsonSerializer.Serialize(vehicles, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(FilePath, json);
        }
        catch
        {
            Console.WriteLine("Nieudane zapisywanie pojazdów");
        }
    }

    
    public async Task RefuelAsync(Vehicle vehicle, int amount)
    {
        if (vehicle.Status == VehicleStatus.InRoute)
            return;

        try
        {
            var vehicles = await GetVehiclesAsync();

            var target = vehicles.FirstOrDefault(v =>
                v.RegistrationNumber == vehicle.RegistrationNumber);

            if (target == null) return;

            target.FuelLevel = Math.Min(100, target.FuelLevel + amount);

            await SaveVehiclesAsync(vehicles);
        }
        catch
        {
            Console.WriteLine($"Nieudane tankowanie pojazdu {vehicle.Name}");
        }
    }

    
    public async Task ChangeStatusAsync(Vehicle vehicle, VehicleStatus newStatus)
    {
        try
        {
            var vehicles = await GetVehiclesAsync();

            var target = vehicles.FirstOrDefault(v =>
                v.RegistrationNumber == vehicle.RegistrationNumber);

            if (target == null) return;

            if (newStatus == VehicleStatus.InRoute)
            {
                if (target.FuelLevel < 15 || target.Status == VehicleStatus.Service)
                    return;
            }

            target.Status = newStatus;

            await SaveVehiclesAsync(vehicles);
        }
        catch
        {
            Console.WriteLine($"Nieudana zmiana statusu pojazdu {vehicle.Name}");
        }
    }
}