using NotasMVVM.ViewModels;

namespace NotasMVVM.Views;

public partial class HistorialPage : ContentPage
{
    private readonly HistorialViewModel _vm;

    public HistorialPage(HistorialViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CargarHistorialCommand.Execute(null);
    }
}
