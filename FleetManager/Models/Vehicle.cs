using System.Reflection.Metadata.Ecma335;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.Models;

public class Vehicle
{
   [Reactive]
   public string  Name { get; set; } = string.Empty;
   
   
   [Reactive]
   public string  RegistrationNumber {
      get;
      set;
   } = string.Empty;
   [Reactive] public int FuelLevel {
      get;
      set;
   } = 0;

   [Reactive]
   public VehicleStatus Status
   {
      get;
      set;
   } = VehicleStatus.Available;
}
