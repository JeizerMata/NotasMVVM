using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotasMVVM.Models;
using NotasMVVM.Services;

namespace NotasMVVM.ViewModels;

[QueryProperty(nameof(NotaId), "id")]
public partial class DetalleNotaViewModel : ObservableObject
{
    private readonly NotasDatabase _db;
    private Nota _nota = new();

    public DetalleNotaViewModel(NotasDatabase db)
    {
        _db = db;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GuardarCommand))]
    private string titulo = string.Empty;

    [ObservableProperty]
    private string contenido = string.Empty;

    [ObservableProperty]
    private string notaId = string.Empty;

    [ObservableProperty]
    private bool esEdicion;

    partial void OnNotaIdChanged(string value)
    {
        if (int.TryParse(value, out int id))
            _ = CargarNota(id);
    }

    private async Task CargarNota(int id)
    {
        var nota = await _db.GetNotaAsync(id);
        if (nota is not null)
        {
            _nota = nota;
            Titulo = nota.Titulo;
            Contenido = nota.Contenido;
            EsEdicion = true;
        }
    }

    private bool PuedeGuardar()
    {
        return !string.IsNullOrWhiteSpace(Titulo) && Titulo.Length <= 100;
    }

    [RelayCommand(CanExecute = nameof(PuedeGuardar))]
    private async Task Guardar()
    {
        _nota.Titulo = Titulo.Trim();
        _nota.Contenido = Contenido?.Trim() ?? string.Empty;

        if (!EsEdicion)
            _nota.FechaCreacion = DateTime.Now;

        await _db.SaveNotaAsync(_nota);

        string accion = EsEdicion ? "Editada" : "Creada";
        await _db.RegistrarHistorialAsync(accion, $"Nota: {_nota.Titulo}");

        await Shell.Current.GoToAsync("..");
    }
}
