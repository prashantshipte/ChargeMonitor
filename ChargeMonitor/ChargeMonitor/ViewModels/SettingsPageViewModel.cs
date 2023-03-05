
namespace ChargeMonitor.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {       
        private readonly ISettingsService settingsService;

        [ObservableProperty]
        bool pushNotificationsEnabled;

        [ObservableProperty]
        bool soundEnabled;

        [ObservableProperty]
        bool vibrationEnabled;

        [ObservableProperty]
        bool darkModeEnabled;

        public AsyncRelayCommand GoToMainPageCommand { get; }

        public SettingsPageViewModel(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
            GoToMainPageCommand = new AsyncRelayCommand(GoToMainPageAsync);
            LoadSettings();
        }

        private void LoadSettings()
        {
            PushNotificationsEnabled = settingsService.Get<bool>(nameof(PushNotificationsEnabled), true);
            SoundEnabled = settingsService.Get<bool>(nameof(SoundEnabled), true);
            VibrationEnabled = settingsService.Get<bool>(nameof(VibrationEnabled), true);
            DarkModeEnabled = settingsService.Get<bool>(nameof(darkModeEnabled), true);
        }

        private void SaveSettings()
        {
            settingsService.Save(nameof(PushNotificationsEnabled), PushNotificationsEnabled);
            settingsService.Save(nameof(SoundEnabled), SoundEnabled);
            settingsService.Save(nameof(VibrationEnabled), VibrationEnabled);
            settingsService.Save(nameof(DarkModeEnabled), DarkModeEnabled);            
        }

        private async Task GoToMainPageAsync()
        {
            SaveSettings();
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
