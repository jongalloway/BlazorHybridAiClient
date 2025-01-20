using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Client
{
    public class Program : MauiApplication
    {
        public Program() : base()
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
