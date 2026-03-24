using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FleetManager.Views;

public partial class VehicleItemView : UserControl
{
    public VehicleItemView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}