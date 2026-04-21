using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;
using System;
using System.Threading.Tasks;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class AddBookViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        // Variables privadas que el Toolkit convierte en Propiedades Públicas (Title, Author, etc.)
        [ObservableProperty] private string title = string.Empty;
        [ObservableProperty] private string author = string.Empty;
        [ObservableProperty] private string genre = string.Empty;
        [ObservableProperty] private string pages = "0";
        [ObservableProperty] private string coverUrl = string.Empty; // ¡Esta faltaba!

        public AddBookViewModel()
        {
            _databaseService = new DatabaseService();
        }

        [RelayCommand]
        private async Task SaveBookAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
            {
                await Shell.Current.DisplayAlert("Error", "Título y Autor son obligatorios", "OK");
                return;
            }

            string finalCoverUrl = CoverUrl;
            if (string.IsNullOrWhiteSpace(finalCoverUrl))
            {
                finalCoverUrl = "portada_por_defecto.png";
            }

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                Genre = Genre,
                CoverUrl = finalCoverUrl,
                Pages = int.TryParse(Pages, out int p) ? p : 0,
                DateAdded = DateTime.Now,
                IsRead = false,
                Rating = 0,
                Notes = ""
            };

            await _databaseService.SaveBookAsync(newBook);

            await Shell.Current.DisplayAlert("Éxito", "Libro agregado a tu biblioteca", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
    }
}