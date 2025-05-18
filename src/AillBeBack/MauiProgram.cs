using Microsoft.Extensions.Logging;

namespace AillBeBack;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		Routing.RegisterRoute("loanresult", typeof(LoanResultPage));
		Routing.RegisterRoute("costsresult", typeof(MedicalCostsResultPage));
		Routing.RegisterRoute("f1result", typeof(F1CarResultPage));

		return builder.Build();
	}
}
