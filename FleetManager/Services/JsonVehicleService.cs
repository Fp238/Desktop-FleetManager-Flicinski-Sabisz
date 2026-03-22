using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;


public class JsonVehicleService
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

    
}