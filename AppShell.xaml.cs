using NotasMVVM.Views;

namespace NotasMVVM;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("detalle", typeof(DetalleNotaPage));
	}
}
