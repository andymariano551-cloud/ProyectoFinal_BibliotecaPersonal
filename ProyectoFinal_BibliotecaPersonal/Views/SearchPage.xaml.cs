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

    private void OnBuscarClicked(object sender, EventArgs e)
    {
        // Dejamos este método vacío por ahora para que no dé el error de listaLibros.
        // El encargado de esta vista deberá conectar su ViewModel luego.
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