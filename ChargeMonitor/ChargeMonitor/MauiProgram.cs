using Microsoft.Extensions.Logging;

namespace ChargeMonitor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()  //  <- Needed 
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		//Register Services
		builder.Services.AddSingleton<ISettingsService, SettingsService>();

		//Register VM first as it needs to be injected in the View
		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<SettingsPageViewModel>();
        builder.Services.AddSingleton<SettingsPage>();
        return builder.Build();
	}
}
