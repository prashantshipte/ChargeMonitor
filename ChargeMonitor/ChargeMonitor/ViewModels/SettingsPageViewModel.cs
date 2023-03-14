
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

        public SettingsPageViewModel(ISettingsService _settingsService)
        {
            settingsService = _settingsService;
            LoadSettings();
        }

        private void LoadSettings()
        {
            PushNotificationsEnabled = settingsService.Get<bool>(GlobalKeys.PushNotificationsEnabled, true);
            NotificationSoundEnabled = settingsService.Get<bool>(GlobalKeys.NotificationSoundEnabled, true);
            VibrationEnabled = settingsService.Get<bool>(GlobalKeys.VibrationEnabled, true);
            DarkModeEnabled = settingsService.Get<bool>(GlobalKeys.DarkModeEnabled, false);
        }

        private void SaveSettings()
        {
            settingsService.Save(GlobalKeys.PushNotificationsEnabled, PushNotificationsEnabled);
            settingsService.Save(GlobalKeys.NotificationSoundEnabled, NotificationSoundEnabled);
            settingsService.Save(GlobalKeys.VibrationEnabled, VibrationEnabled);
            settingsService.Save(GlobalKeys.DarkModeEnabled, DarkModeEnabled);            
        }

        [RelayCommand]
        private void EnableDarkMode()
        {
            settingsService.Save(GlobalKeys.DarkModeEnabled, DarkModeEnabled);
            Application.Current.UserAppTheme = DarkModeEnabled ? AppTheme.Dark : AppTheme.Light;
        }

        [RelayCommand]
        private async Task GoToMainPageAsync()
        {
            SaveSettings();
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
