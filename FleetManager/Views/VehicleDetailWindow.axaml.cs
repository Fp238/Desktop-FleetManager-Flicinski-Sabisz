using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FleetManager.Models;
using System;

namespace FleetManager.Views;

public partial class VehicleDetailWindow : Window
{
    public VehicleDetailWindow()
    {
        InitializeComponent();
    }

    public VehicleDetailWindow(Vehicle vehicle)
    {
        InitializeComponent();

        DataContext = vehicle;

        var comboBox = this.FindControl<ComboBox>("StatusComboBox");
        if (comboBox != null)
        {
            comboBox.ItemsSource = Enum.GetValues(typeof(VehicleStatus));
        }
    }

    private void CloseButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}