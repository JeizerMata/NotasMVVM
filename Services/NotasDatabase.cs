using SQLite;
using NotasMVVM.Models;

namespace NotasMVVM.Services;

public class NotasDatabase
{
    private SQLiteAsyncConnection _db;

    async Task Init()
    {
        if (_db is not null) return;
        string path = Path.Combine(FileSystem.AppDataDirectory, "notas.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<Nota>();
        await _db.CreateTableAsync<Historial>();
    }

    public async Task<List<Nota>> GetNotasAsync()
    {
        await Init();
        return await _db.Table<Nota>().OrderByDescending(n => n.FechaCreacion).ToListAsync();
    }

    public async Task<Nota> GetNotaAsync(int id)
    {
        await Init();
        return await _db.Table<Nota>().Where(n => n.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveNotaAsync(Nota nota)
    {
        await Init();
        if (nota.Id != 0)
            return await _db.UpdateAsync(nota);
        else
            return await _db.InsertAsync(nota);
    }

    public async Task<int> DeleteNotaAsync(Nota nota)
    {
        await Init();
        return await _db.DeleteAsync(nota);
    }

    public async Task RegistrarHistorialAsync(string accion, string descripcion)
    {
        await Init();
        await _db.InsertAsync(new Historial
        {
            Accion = accion,
            Descripcion = descripcion,
            Fecha = DateTime.Now
        });
    }

    public async Task<List<Historial>> GetHistorialAsync()
    {
        await Init();
        return await _db.Table<Historial>().OrderByDescending(h => h.Fecha).ToListAsync();
    }
}
