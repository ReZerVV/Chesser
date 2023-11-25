using Chesser.App.UI;
using Chesser.App.UI.Views;

namespace Chesser.App;

internal static class Program
{
    public static void Main(string[] args)
    {
        Application application = new Application();
        application.MainView = new MenuGameView();
        application.ApplicationLoop();
    }
}