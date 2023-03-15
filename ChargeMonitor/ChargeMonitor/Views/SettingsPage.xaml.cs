namespace ChargeMonitor.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}