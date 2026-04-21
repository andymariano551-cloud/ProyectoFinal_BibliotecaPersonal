using CommunityToolkit.Mvvm.ComponentModel;
using ProyectoFinal_BibliotecaPersonal.Services;
using ProyectoFinal_BibliotecaPersonal.Drawables;

namespace ProyectoFinal_BibliotecaPersonal.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private StatisticsDrawable statsGraphics;

        public StatisticsViewModel()
        {
            _databaseService = new DatabaseService();
            StatsGraphics = new StatisticsDrawable();
            LoadStatsAsync();
        }

        public async Task LoadStatsAsync()
        {
            var stats = await _databaseService.GetStatisticsAsync();

            StatsGraphics.ReadBooks = stats.Read;
            StatsGraphics.UnreadBooks = stats.Unread;

            // Forzamos la actualización de la propiedad para que la UI se repinte
            OnPropertyChanged(nameof(StatsGraphics));
        }
    }
}