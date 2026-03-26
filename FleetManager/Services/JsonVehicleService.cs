using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services
{
    public class JsonVehicleService : IVehicleService
    {
        private static string FilePath => Path.Combine(AppContext.BaseDirectory, "vehicles.json");

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine($"Plik nie istnieje: {FilePath}");
                    return new List<Vehicle>();
                }

                var json = await File.ReadAllTextAsync(FilePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };

                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json, options);

                Console.WriteLine($"Loaded vehicles: {vehicles?.Count ?? 0}");
                return vehicles ?? new List<Vehicle>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania pojazdów: {ex.Message}");
                return new List<Vehicle>();
            }
        }

        public async Task SaveVehiclesAsync(List<Vehicle> vehicles)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                };

                var json = JsonSerializer.Serialize(vehicles, options);
                await File.WriteAllTextAsync(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieudane zapisywanie pojazdów: {ex.Message}");
            }
        }

        public async Task RefuelAsync(Vehicle vehicle, int amount)
        {
            if (vehicle.Status == VehicleStatus.InRoute)
                return;

            try
            {
                var vehicles = await GetVehiclesAsync();
                var target = vehicles.FirstOrDefault(v => v.RegistrationNumber == vehicle.RegistrationNumber);
                if (target == null) return;

                target.FuelLevel = Math.Min(100, target.FuelLevel + amount);
                await SaveVehiclesAsync(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieudane tankowanie pojazdu {vehicle.Name}: {ex.Message}");
            }
        }

        public async Task ChangeStatusAsync(Vehicle vehicle, VehicleStatus newStatus)
        {
            try
            {
                var vehicles = await GetVehiclesAsync();
                var target = vehicles.FirstOrDefault(v => v.RegistrationNumber == vehicle.RegistrationNumber);
                if (target == null) return;

                if (newStatus == VehicleStatus.InRoute)
                {
                    if (target.FuelLevel < 15 || target.Status == VehicleStatus.Service)
                        return;
                }

                target.Status = newStatus;
                await SaveVehiclesAsync(vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nieudana zmiana statusu pojazdu {vehicle.Name}: {ex.Message}");
            }
        }
    }
}