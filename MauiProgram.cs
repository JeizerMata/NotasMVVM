using Microsoft.Extensions.Logging;
using NotasMVVM.Services;
using NotasMVVM.ViewModels;
using NotasMVVM.Views;

namespace NotasMVVM;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<NotasDatabase>();
		builder.Services.AddSingleton<NotasViewModel>();
		builder.Services.AddTransient<DetalleNotaViewModel>();
		builder.Services.AddTransient<HistorialViewModel>();
		builder.Services.AddTransient<NotasPage>();
		builder.Services.AddTransient<DetalleNotaPage>();
		builder.Services.AddTransient<HistorialPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
