
namespace ChargeMonitor.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        public AsyncRelayCommand GoToMainPageCommand { get; }

        private readonly ISettingsService settingsService;

        public SettingsPageViewModel(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
            GoToMainPageCommand = new AsyncRelayCommand(GoToMainPageAsync);
            LoadSettings();
        }

        private void LoadSettings()
        {            
        }

        private async Task GoToMainPageAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
