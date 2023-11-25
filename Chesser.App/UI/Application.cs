using System.Diagnostics;
using Chesser.App.UI.Views;
using Chesser.Drawing;

namespace Chesser.App.UI;

public class Application
{
    private Canvas MainCanvas;
    public int FPS { get; set; }
    private System.Timers.Timer fpsTimer;
    private Stopwatch fpsSynchronizationTimer;
    private Queue<ConsoleKeyInfo> inputEventQueue = new();
    public static bool IsOpen { get; private set; }

    private IView? mainView = null;
    public IView? MainView
    {
        get
        {
            return mainView;
        }
        set
        {
            mainView = value;
            mainView.Position = Vector2.Zero;
            mainView.Size = new Vector2(MainCanvas.Width, MainCanvas.Height);
        }
    }

    public Application(string title = "App (cyril project)")
    {
        Console.Title = title;
        Initialize();
    }

    public void Initialize()
    {
        MainCanvas = new Canvas(Console.WindowWidth, Console.WindowHeight, Color.Black);
        IsOpen = true;
        FPS = 60;
        fpsTimer = new System.Timers.Timer(1000);
        fpsSynchronizationTimer = new Stopwatch();
    }

    public void ApplicationLoop()
    {
        Thread consoleInputThread = new Thread(ConsoleInput);
        consoleInputThread.Start();
        fpsTimer.Start();

        while (IsOpen)
        {
            fpsSynchronizationTimer.Restart();
            {
                if (AppState.NavigationSerivce.IsViewChanged)
                {
                    MainView = AppState.NavigationSerivce.View;
                    AppState.NavigationSerivce.IsViewChanged = false;
                }
                if (MainView != null)
                {
                    if (inputEventQueue.TryDequeue(out ConsoleKeyInfo keyInfo))
                    {
                        OnInput(keyInfo);
                    }
                    OnUpdate();
                    OnLayout();
                    OnRender();
                }
            }
            MainCanvas.RenderBuffer();
            fpsSynchronizationTimer.Stop();

            if (MainCanvas.Width != Console.WindowWidth ||
                MainCanvas.Height != Console.WindowHeight)
            {
                MainCanvas = new Canvas(Console.WindowWidth, Console.WindowHeight, Color.Black);
                mainView.Position = Vector2.Zero;
                mainView.Size = new Vector2(MainCanvas.Width, MainCanvas.Height);
            }

            int timeSynchronizationSleep = (int)(0.5f / FPS * 1000.0f - fpsSynchronizationTimer.Elapsed.TotalMilliseconds);
            if (timeSynchronizationSleep > 0)
            {
                Thread.Sleep(timeSynchronizationSleep);
            }

            MainCanvas.ClearBuffer();
        }
        fpsTimer.Stop();
        consoleInputThread.Join();
    }

    private void ConsoleInput()
    {
        while (IsOpen)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.X)
                    IsOpen = false;
                else
                    inputEventQueue.Enqueue(keyInfo);
            }
        }
    }

    public static void Exit()
    {
        IsOpen = false;
    }

    private void OnInput(ConsoleKeyInfo keyInfo)
    {
        MainView.Input(keyInfo);
    }

    private void OnUpdate()
    {
        MainView.Update();
    }

    private void OnRender()
    {
        MainView.Render(MainCanvas);
    }

    private void OnLayout()
    {
    }
}