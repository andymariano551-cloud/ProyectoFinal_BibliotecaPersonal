using ProyectoFinal_BibliotecaPersonal.Models;
using Microsoft.Maui.Controls; 

namespace ProyectoFinal_BibliotecaPersonal.Views;

[QueryProperty(nameof(SelectedBook), "Book")]
public partial class BookDetailPage : ContentPage
{
    private Book _selectedBook;
    public Book SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            OnPropertyChanged();
        }
    }

    public BookDetailPage()
    {
        InitializeComponent();
    }
}