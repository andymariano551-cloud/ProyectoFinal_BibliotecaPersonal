using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class LibraryViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Book> books = new();

        public LibraryViewModel()
        {
            _databaseService = new DatabaseService();
            LoadBooksCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadBooksAsync()
        {
            var data = await _databaseService.GetBooksAsync();
            Books.Clear();
            foreach (var book in data)
            {
                Books.Add(book);
            }
        }

        [RelayCommand]
        private async Task AddBookAsync()
        {
            // Esto navegará a la página de agregar (cuando la conectemos en el AppShell)
            await Shell.Current.GoToAsync("AddBookPage");
        }
    }
}