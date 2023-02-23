
namespace ChargeMonitor.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public ICommand BatteryMonitorSwitchToggledCommand { get; }

        [ObservableProperty]
        bool isBatteryWatched;

        [ObservableProperty]
        int batteryChargeLimit;

        public MainPageViewModel()
        {
            BatteryMonitorSwitchToggledCommand = new Command(BatteryMonitorSwitchToggled);
            BatteryChargeLimit = 80;
        }

        private void BatteryMonitorSwitchToggled(object obj)
        {
            if (!IsBatteryWatched)
            {
                Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
            }
            else
            {
                Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
            }

            IsBatteryWatched = !IsBatteryWatched;
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            // If battery is charging -
            // if current charge level is already more then charge limit - 

            // if current charge level is equal to charge limit - 
            // show popup + notification 


            //BatteryStateLabel.Text = e.State switch
            //{
            //    BatteryState.Charging => "Battery is currently charging",
            //    BatteryState.Discharging => "Charger is not connected and the battery is discharging",
            //    BatteryState.Full => "Battery is full",
            //    BatteryState.NotCharging => "The battery isn't charging.",
            //    BatteryState.NotPresent => "Battery is not available.",
            //    BatteryState.Unknown => "Battery is unknown",
            //    _ => "Battery is unknown"
            //};

            //BatteryLevelLabel.Text = $"Battery is {e.ChargeLevel * 100}% charged.";
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
