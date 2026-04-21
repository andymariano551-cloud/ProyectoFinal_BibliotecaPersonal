using ProyectoFinal_BibliotecaPersonal.ViewModels;

namespace ProyectoFinal_BibliotecaPersonal.Views;

public partial class LibraryPage : ContentPage
{
    public LibraryPage()
    {
        InitializeComponent();
    }

    // Este método se dispara cada vez que entras a la pestaña o vuelves atrás
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Verificamos si el BindingContext es nuestro ViewModel
        if (BindingContext is LibraryViewModel vm)
        {
            // Ejecutamos el comando de cargar libros para refrescar la lista
            if (vm.LoadBooksCommand.CanExecute(null))
            {
                vm.LoadBooksCommand.Execute(null);
            }
        }
    }
}