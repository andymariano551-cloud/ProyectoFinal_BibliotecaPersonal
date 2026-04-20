using ProyectoFinal_BibliotecaPersonal.Models;
using ProyectoFinal_BibliotecaPersonal.Services;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class SearchPage : ContentPage
{
    ApiService api = new ApiService();

    public SearchPage()
    {
        InitializeComponent();
    }

    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        var libros = await api.BuscarPorTitulo(txtBuscar.Text ?? string.Empty);
        listaLibros.ItemsSource = libros;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Book libro)
        {
            await api.GuardarLibro(libro);

            await DisplayAlert("Éxito", "Libro guardado", "OK");
        }
    }
}