﻿using ChargeMonitor.Views;

namespace ChargeMonitor;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
	}
}
