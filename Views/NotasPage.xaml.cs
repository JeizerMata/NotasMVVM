using NotasMVVM.ViewModels;

namespace NotasMVVM.Views;

public partial class NotasPage : ContentPage
{
    private readonly NotasViewModel _vm;

    public NotasPage(NotasViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CargarNotasCommand.Execute(null);
    }
}
