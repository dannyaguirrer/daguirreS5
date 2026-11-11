using daguirreS5.Repositories;
using daguirreS5.Utilis;
using Microsoft.Extensions.Logging;

namespace daguirreS5
{
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
            // Base de datos SQLite local
            string dbPath = FileAccessHelper.getFilepath("personas.db3");
            builder.Services.AddSingleton<PersonRepository>(s =>
                ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));

            return builder.Build();
        }
    }
}
