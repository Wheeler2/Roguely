namespace Roguely.Core;

public abstract class SystemBase : ISystem
{
    /// <summary>
    /// Executes the system logic.
    /// </summary>
    /// <param name="deltaTime">The time elapsed since the last update.</param>
    public abstract void Execute(float deltaTime);
}