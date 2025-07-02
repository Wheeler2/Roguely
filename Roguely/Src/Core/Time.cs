using Microsoft.Xna.Framework;

namespace Roguely.Core;

public static class Time
{
    /// <summary>
    /// The time elapsed since the last frame in seconds.
    /// </summary>
    public static float DeltaTime { get; private set; }

    /// <summary>
    /// The total time elapsed since the start of the game in seconds.
    /// </summary>
    public static float TotalTime { get; private set; }

    /// <summary>
    /// Updates the time variables. This should be called once per frame.
    /// </summary>
    /// <param name="deltaTime">The time elapsed since the last frame in seconds.</param>
    public static void Update(GameTime gameTime)
    {
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        TotalTime += DeltaTime;
    }
}