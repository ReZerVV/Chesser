using Chesser.App.UI.Views;

namespace Chesser.App.Services;

public class NavigationSerivce
{
    private Stack<IView> Histories { get; set; }
    public IView? View { get; set; }
    public bool IsViewChanged { get; set; }

    public NavigationSerivce()
    {
        Histories = new Stack<IView>();
        View = null;
        IsViewChanged = false;
    }

    public void Navigate(IView view)
    {
        if (View != null)
        {
            Histories.Push(View);
        }
        View = view;
        IsViewChanged = true;
    }

    public void Back()
    {
        if (Histories.TryPop(out IView? view))
        {
            View = view;
            IsViewChanged = true;
        }
    }
}