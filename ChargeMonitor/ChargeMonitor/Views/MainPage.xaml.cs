namespace ChargeMonitor;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}

}

