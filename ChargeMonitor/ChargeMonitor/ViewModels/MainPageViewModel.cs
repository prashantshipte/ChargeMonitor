
namespace ChargeMonitor.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {       
        [ObservableProperty]
        bool isBatteryWatched;

        [ObservableProperty]
        int batteryChargeLimit;

        private readonly ISettingsService settingsService;
        private readonly INotificationService notificationService;
        private const int defaultBatteryChargeLimit = 80;
        private bool userNotified = false;

        public MainPageViewModel(ISettingsService _settingsService, INotificationService  _notificationService)
        {
            settingsService = _settingsService;
            notificationService = _notificationService;
            ApplyTheme();
            LoadSettings();
            RegisterEvents();
        }

        private void ApplyTheme()
        {
            var darkModeEnabled = settingsService.Get<bool>(GlobalKeys.DarkModeEnabled, false);
            Application.Current.UserAppTheme = darkModeEnabled ? AppTheme.Dark : AppTheme.Light;
        }

        private void LoadSettings()
        {
            IsBatteryWatched = settingsService.Get<bool>(GlobalKeys.IsBatteryWatched, false);
            BatteryChargeLimit = settingsService.Get<int>(GlobalKeys.BatteryChargeLimit, defaultBatteryChargeLimit);
        }

        private void RegisterEvents()
        {
            if (IsBatteryWatched)
            {
                Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;                
            }
        }

        [RelayCommand]
        private async Task GoToSettingsPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage), true);
        }

        [RelayCommand]
        private void SetBatteryChargeLimit()
        {
            settingsService.Save(GlobalKeys.BatteryChargeLimit, BatteryChargeLimit);
        }

        [RelayCommand]
        private void BatteryMonitorSwitchToggled()
        {
            if (IsBatteryWatched)
            {
                Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
            }
            else
            {
                Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
            }

            settingsService.Save(GlobalKeys.IsBatteryWatched, IsBatteryWatched);
        }

        private async void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            switch (e.State)
            {
                case BatteryState.Charging:
                    {
                        var currentChargeLevel = (int)Math.Round(e.ChargeLevel * 100);

                        if (currentChargeLevel > BatteryChargeLimit && !userNotified)
                        {
                            userNotified = true;
                            await NotifyUserAsync();
                        }
                    }
                    break;
                case BatteryState.NotCharging:
                case BatteryState.Discharging:
                    {
                        userNotified = false;
                    }
                    break;
                case BatteryState.Full:
                    {
                        await BatteryFullNotificationAsync();
                    }
                    break;                                
                case BatteryState.NotPresent:
                case BatteryState.Unknown:                    
                    {
                        await BatteryUnknownNotificationAsync();
                    }
                    break;
            }
        }      

        private async Task NotifyUserAsync()
        {
            await ShowPushNotificationAsync();
            await Shell.Current.DisplayAlert("Alert", "Battery charged above the desired charge limit. \r\nPlease disconnect charger", "OK");
        }

        private async Task ShowPushNotificationAsync()
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            var pushNotificationsEnabled = settingsService.Get<bool>(GlobalKeys.PushNotificationsEnabled, true);
            var notificationSoundEnabled = settingsService.Get<bool>(GlobalKeys.NotificationSoundEnabled, true);
            var vibrationEnabled = settingsService.Get<bool>(GlobalKeys.VibrationEnabled, true);

            if (pushNotificationsEnabled)
            {
                var notificationRequest = new NotificationRequest()
                {
                    Title = "Battery charged above the desired charge limit. \r\nPlease disconnect charger",                                       
                    Silent = !notificationSoundEnabled,
                };                              

                if (vibrationEnabled)
                {
                    notificationRequest.Android = new AndroidOptions()
                    {
                        VibrationPattern = new long[] { 500, 1000 }
                    };
                }

                await notificationService.Show(notificationRequest);
            }
        }

        private async Task BatteryFullNotificationAsync()
        {
            await Shell.Current.DisplayAlert("Alert", "Battery fully charged. \r\nPlease disconnect charger", "OK");
        }

        private async Task BatteryUnknownNotificationAsync()
        {
            await Shell.Current.DisplayAlert("Warning", "Battery status unknown.", "OK");
        }

        private void getBatteryHealth()
        {
            //System.Management.ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            //ManagementObjectCollection collection = searcher.Get();

            //foreach (ManagementObject mo in collection)
            //{
            //    foreach (PropertyData property in mo.Properties)
            //    {
            //        Console.WriteLine("Property {0}: Value is {1}", property.Name, property.Value);
            //    }
            //}
        }
    }
}
