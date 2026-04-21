using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class AddBookViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty] private string title = string.Empty;
        [ObservableProperty] private string author = string.Empty;
        [ObservableProperty] private string genre = string.Empty;
        [ObservableProperty] private string pages = "0";

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

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                Genre = Genre,
                Pages = int.TryParse(Pages, out int p) ? p : 0,
                DateAdded = DateTime.Now
            };

            await _databaseService.SaveBookAsync(newBook);
            await Shell.Current.Navigation.PopAsync(); // Volver a la biblioteca
        }
    }
}