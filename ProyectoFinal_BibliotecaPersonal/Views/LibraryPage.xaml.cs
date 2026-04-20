using ProyectoFinal_BibliotecaPersonal.Models;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class LibraryPage : ContentPage
{
    public LibraryPage()
    {
        InitializeComponent();

        listaLibros.ItemsSource = new List<Book>
        {
            new Book
            {
                Title = "El Psicoanalista",
                Author = "John Katzenbach",
                Genre = "Thriller",
                Year = 2002,
                IsRead = true
            },
            new Book
            {
                Title = "Cien años de soledad",
                Author = "Gabriel García Márquez",
                Genre = "Realismo Mágico",
                Year = 1967,
                IsRead = false
            }
        };
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddBookPage));
    }
}