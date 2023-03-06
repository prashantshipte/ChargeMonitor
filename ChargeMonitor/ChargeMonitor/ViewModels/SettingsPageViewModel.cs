
namespace ChargeMonitor.ViewModels
{
    public partial class SettingsPageViewModel : BaseViewModel
    {       
        private readonly ISettingsService settingsService;

        [ObservableProperty]
        bool pushNotificationsEnabled;

        [ObservableProperty]
        bool notificationSoundEnabled;

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
            PushNotificationsEnabled = settingsService.Get<bool>(GlobalKeys.PushNotificationsEnabled, true);
            NotificationSoundEnabled = settingsService.Get<bool>(GlobalKeys.NotificationSoundEnabled, true);
            VibrationEnabled = settingsService.Get<bool>(GlobalKeys.VibrationEnabled, true);
            DarkModeEnabled = settingsService.Get<bool>(GlobalKeys.DarkModeEnabled, true);
        }

        private void SaveSettings()
        {
            settingsService.Save(GlobalKeys.PushNotificationsEnabled, PushNotificationsEnabled);
            settingsService.Save(GlobalKeys.NotificationSoundEnabled, NotificationSoundEnabled);
            settingsService.Save(GlobalKeys.VibrationEnabled, VibrationEnabled);
            settingsService.Save(GlobalKeys.DarkModeEnabled, DarkModeEnabled);            
        }

        private async Task GoToMainPageAsync()
        {
            SaveSettings();
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
