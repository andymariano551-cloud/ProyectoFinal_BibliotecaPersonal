using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProyectoFinal_BibliotecaPersonal.Models;

namespace ProyectoFinal_BibliotecaPersonal.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database = null!;

        public DatabaseService()
        {
        }

        // Manejo de conexión e inicialización de BD
        private async Task InitAsync()
        {
            if (_database is not null)
                return;

            // Crea la ruta local en el dispositivo del usuario
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "BibliotecaPersonal.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Book>();
        }

        // 1. Obtener todos los libros
        public async Task<List<Book>> GetBooksAsync()
        {
            await InitAsync();
            return await _database.Table<Book>().ToListAsync();
        }

        // 2. Obtener un libro por ID
        public async Task<Book> GetBookAsync(int id)
        {
            await InitAsync();
            return await _database.Table<Book>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        // Método para buscar libros localmente por título o autor
        // Método para buscar libros localmente por título o autor
        public async Task<List<Book>> SearchBooksLocalAsync(string query)
        {
            await InitAsync();
            if (string.IsNullOrWhiteSpace(query))
            {
                return await GetBooksAsync();
            }

            query = query.ToLower();

            // CORRECCIÓN AQUÍ: Cambiamos _databaseService por _database
            return await _database.Table<Book>()
                                  .Where(b => b.Title.ToLower().Contains(query) ||
                                              b.Author.ToLower().Contains(query))
                                  .ToListAsync();
        }

        // 3. Insertar / Actualizar inteligente (SaveBookAsync)

        public async Task<int> SaveBookAsync(Book book)
        {
            await InitAsync();
            if (book.Id != 0)
            {
                return await _database.UpdateAsync(book);
            }
            else
            {
                book.DateAdded = DateTime.Now;
                return await _database.InsertAsync(book);
            }
        }

        // 4. Eliminar libro
        public async Task<int> DeleteBookAsync(Book book)
        {
            await InitAsync();
            return await _database.DeleteAsync(book);
        }

        // 5. Filtrar por género
        public async Task<List<Book>> GetBooksByGenreAsync(string genre)
        {
            await InitAsync();
            return await _database.Table<Book>().Where(b => b.Genre == genre).ToListAsync();
        }

        // 6. Obtener solo leídos
        public async Task<List<Book>> GetReadBooksAsync()
        {
            await InitAsync();
            return await _database.Table<Book>().Where(b => b.IsRead).ToListAsync();
        }

        // 7. Obtener pendientes (No leídos)
        public async Task<List<Book>> GetUnreadBooksAsync()
        {
            await InitAsync();
            return await _database.Table<Book>().Where(b => !b.IsRead).ToListAsync();
        }

        // 8. Actualizar solo el estado de lectura (UpdateReadStatusAsync)
        public async Task<int> UpdateReadStatusAsync(int id, bool isRead)
        {
            await InitAsync();
            var book = await GetBookAsync(id);
            if (book != null)
            {
                book.IsRead = isRead;
                return await _database.UpdateAsync(book);
            }
            return 0;
        }

        // 9. Contar total de libros en la base de datos
        public async Task<int> GetTotalBooksCountAsync()
        {
            await InitAsync();
            return await _database.Table<Book>().CountAsync();
        }

        // 10. Obtener Estadísticas (Retorna una tupla con los totales)
        public async Task<(int Total, int Read, int Unread)> GetStatisticsAsync()
        {
            await InitAsync();
            var total = await GetTotalBooksCountAsync();
            var read = await _database.Table<Book>().Where(b => b.IsRead).CountAsync();
            var unread = total - read;

            return (total, read, unread);
        }
    }
}