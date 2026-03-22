using System.Collections.Generic;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;


public interface IVehicleService
{
    Task<List<Vehicle>> GetVehiclesAsync();
    Task SaveVehiclesAsync(List<Vehicle> vehicles);
    Task RefuelAsync(Vehicle vehicle, int amount);
    Task ChangeStatusAsync(Vehicle vehicle, VehicleStatus newStatus);
}