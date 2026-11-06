using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotasMVVM.Models;
using NotasMVVM.Services;

namespace NotasMVVM.ViewModels;

public partial class HistorialViewModel : ObservableObject
{
    private readonly NotasDatabase _db;

    public HistorialViewModel(NotasDatabase db)
    {
        _db = db;
    }

    public ObservableCollection<Historial> Registros { get; } = new();

    [RelayCommand]
    private async Task CargarHistorial()
    {
        var registros = await _db.GetHistorialAsync();
        Registros.Clear();
        foreach (var r in registros)
            Registros.Add(r);
    }
}
