using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System.Collections.ObjectModel;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly ApiService _apiService;

        // Propiedad para el texto que el usuario escribe en el buscador
        [ObservableProperty]
        private string searchQuery = string.Empty;

        // Colección de resultados que se mostrará en la UI
        [ObservableProperty]
        private ObservableCollection<Book> searchResults = new();

        // Propiedad para controlar si estamos buscando localmente o en la API
        [ObservableProperty]
        private bool isLocalSearch = true; // Por defecto, busca en la BD

        public SearchViewModel()
        {
            _databaseService = new DatabaseService();
            _apiService = new ApiService();
        }

        [RelayCommand]
        private async Task PerformSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                SearchResults.Clear();
                return;
            }

            SearchResults.Clear();
            List<Book> books;

            if (IsLocalSearch)
            {
                // Busca en tu base de datos SQLite
                books = await _databaseService.SearchBooksLocalAsync(SearchQuery);
            }
            else
            {
                // Busca nuevos libros en la API de Google Books
                books = await _apiService.BuscarPorTitulo(SearchQuery);
            }

            foreach (var book in books)
            {
                SearchResults.Add(book);
            }
        }
    }
}