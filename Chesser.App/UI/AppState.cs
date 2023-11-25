using Chesser.Drawing;
using Chesser.App.Services;
using Chesser.App.UI.Views;

namespace Chesser.App.UI;

public static class AppState
{
    public static NavigationSerivce NavigationSerivce { get; set; } = new NavigationSerivce();

    public static class Colors
    {
        public static Color BackgroundDark { get; set; } = new Color(33, 33, 33);
        public static Color Background { get; set; } = new Color(39, 40, 41);
        
        public static Color ForegroundDark { get; set; } = new Color(187, 187, 187);
        public static Color Foreground { get; set; } = new Color(238, 238, 238);
        
        public static Color BoardLight { get; set; } = new Color(216, 217, 218);
        public static Color BoardDark { get; set; } = new Color(97, 103, 122);

        public static Color Green {get;set;} = new Color(65, 208, 97);
        public static Color Yellow {get;set;} = new Color(206, 192, 64);
    }
}