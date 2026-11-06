using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotasMVVM.Models;
using NotasMVVM.Services;

namespace NotasMVVM.ViewModels;

public partial class NotasViewModel : ObservableObject
{
    private readonly NotasDatabase _db;

    public NotasViewModel(NotasDatabase db)
    {
        _db = db;
    }

    public ObservableCollection<Nota> Notas { get; } = new();

    [ObservableProperty]
    private string textoBusqueda = string.Empty;

    private List<Nota> _todasLasNotas = new();

    [RelayCommand]
    private async Task CargarNotas()
    {
        _todasLasNotas = await _db.GetNotasAsync();
        FiltrarNotas();
    }

    partial void OnTextoBusquedaChanged(string value)
    {
        FiltrarNotas();
    }

    private void FiltrarNotas()
    {
        Notas.Clear();
        var filtradas = string.IsNullOrWhiteSpace(TextoBusqueda)
            ? _todasLasNotas
            : _todasLasNotas.Where(n => n.Titulo.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var nota in filtradas)
            Notas.Add(nota);
    }

    [RelayCommand]
    private async Task AgregarNota()
    {
        await Shell.Current.GoToAsync("detalle");
    }

    [RelayCommand]
    private async Task EditarNota(Nota nota)
    {
        await Shell.Current.GoToAsync($"detalle?id={nota.Id}");
    }

    [RelayCommand]
    private async Task EliminarNota(Nota nota)
    {
        bool confirmar = await Shell.Current.DisplayAlert("Confirmar",
            $"¿Eliminar la nota \"{nota.Titulo}\"?", "Sí", "No");

        if (confirmar)
        {
            await _db.DeleteNotaAsync(nota);
            await _db.RegistrarHistorialAsync("Eliminada", $"Nota: {nota.Titulo}");
            await CargarNotas();
        }
    }
}
