using System.Windows;
using TaskFlow.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.View;

namespace TaskFlow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceProvider.GetService<TasksViewModel>();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TasksViewModel>();
            services.AddTransient<CreateTaskViewModel>();
            services.AddTransient<TasksPage>();
            services.AddTransient<CreateTaskPage>();
            services.AddTransient<PomodoroPage>();
        }
    }

}
