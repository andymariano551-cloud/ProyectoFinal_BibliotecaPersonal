using System.Text.Json;
using ProyectoFinal_BibliotecaPersonal.Models;

namespace ProyectoFinal_BibliotecaPersonal.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Book>> BuscarPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo)) return new List<Book>();

            var url = $"https://www.googleapis.com/books/v1/volumes?q=intitle:{titulo}";
            var json = await _httpClient.GetStringAsync(url);

            return MapearLibros(json);
        }

        public async Task<List<Book>> BuscarPorAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor)) return new List<Book>();

            var url = $"https://www.googleapis.com/books/v1/volumes?q=inauthor:{autor}";
            var json = await _httpClient.GetStringAsync(url);

            return MapearLibros(json);
        }

        private List<Book> MapearLibros(string json)
        {
            var lista = new List<Book>();
            var data = JsonDocument.Parse(json);

            if (!data.RootElement.TryGetProperty("items", out var items))
                return lista;

            foreach (var item in items.EnumerateArray())
            {
                var volumeInfo = item.GetProperty("volumeInfo");

                var book = new Book
                {
                    Title = ObtenerTexto(volumeInfo, "title", "Sin título"),
                    Author = ObtenerAutor(volumeInfo),
                    ISBN = ObtenerISBN(volumeInfo),
                    Year = ObtenerAño(volumeInfo),
                    Genre = ObtenerCategoria(volumeInfo),
                    Pages = ObtenerPaginas(volumeInfo),
                    CoverUrl = ObtenerImagen(volumeInfo),

                    IsRead = false,
                    Rating = 0,
                    Notes = "",
                    DateAdded = DateTime.Now
                };

                lista.Add(book);
            }

            return lista;
        }

        private string ObtenerTexto(JsonElement obj, string prop, string def)
        {
            return obj.TryGetProperty(prop, out var val) && val.GetString() is string s ? s : def;
        }

        private string ObtenerAutor(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("authors", out var authors) && authors.GetArrayLength() > 0)
            {
                return authors[0].GetString() ?? "Desconocido";
            }
            return "Desconocido";
        }

        private string ObtenerISBN(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("industryIdentifiers", out var ids))
            {
                foreach (var id in ids.EnumerateArray())
                {
                    var type = id.GetProperty("type").GetString();
                    if (type != null && type.Contains("ISBN"))
                        return id.GetProperty("identifier").GetString() ?? "";
                }
            }
            return "";
        }

        private int ObtenerAño(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("publishedDate", out var date))
            {
                var texto = date.GetString();
                if (!string.IsNullOrEmpty(texto) && texto.Length >= 4)
                {
                    if (int.TryParse(texto.Substring(0, 4), out int year))
                        return year;
                }
            }
            return 0;
        }

        private string ObtenerCategoria(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("categories", out var cat) && cat.GetArrayLength() > 0)
                return cat[0].GetString() ?? "General";

            return "General";
        }

        private int ObtenerPaginas(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("pageCount", out var pages))
                return pages.GetInt32();

            return 0;
        }

        private string ObtenerImagen(JsonElement volumeInfo)
        {
            if (volumeInfo.TryGetProperty("imageLinks", out var img))
            {
                if (img.TryGetProperty("thumbnail", out var thumb))
                    return thumb.GetString() ?? "";
            }
            return "";
        }

        public async Task GuardarLibro(Book? book)
        {
            if (book == null) return;
            var db = new DatabaseService();
            await db.SaveBookAsync(book); // CORRECCIÓN: Llamando al método correcto de tu BD
        }
    }
}