using NotasMVVM.ViewModels;

namespace NotasMVVM.Views;

public partial class DetalleNotaPage : ContentPage
{
    public DetalleNotaPage(DetalleNotaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
