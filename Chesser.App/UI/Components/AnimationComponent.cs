using Chesser.Drawing;

namespace Chesser.App.UI.Components;

public class AnimationComponent
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Color Foregroung { get; set; }
    public Color Backgroung { get; set; }
    public string[] Frames { get; set; } = new string[0];
    public int Delay { get; set; } = 10;
    public bool Playing = true;
    private int delayIndex = 0;
    private int frameIndex = 0;

    public void Reset()
    {
        delayIndex = 0;
        frameIndex = 0;
    }

    public void Update()
    {
        if (!Playing)
        {
            return;
        }

        if (delayIndex % Delay == 0)
        {
            if (frameIndex + 1 < Frames.Length)
            {
                frameIndex++;
            }
            else
            {
                frameIndex = 0;
            }
        }
        delayIndex++;
        if (delayIndex >= 1000)
        {
            delayIndex = 0;
        }
    }

    public void Render(Canvas canvas)
    {
        if (!Playing)
        {
            return;
        }

        canvas.DrawText(
            text: Frames[frameIndex],
            x: Position.x,
            y: Position.y,
            foreground: Foregroung,
            background: Backgroung,
            style: PixelStyle.StyleNone
        );
    }
}