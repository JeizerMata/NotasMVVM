using SQLite;

namespace NotasMVVM.Models;

public class Historial
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Accion { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public DateTime Fecha { get; set; } = DateTime.Now;
}
