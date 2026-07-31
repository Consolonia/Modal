// DUPFINDER_ignore

using Avalonia;

namespace Consolonia.Sandbox
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += (_, eventArgs) =>
                ThreadPool.QueueUserWorkItem(_ => throw new Exception("UnobservedTaskException", eventArgs.Exception));
            
            BuildAvaloniaApp()
                .StartWithConsoleLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UseConsolonia()
                .UseAutoDetectedConsole()
                .LogToException();
        }
    }
}