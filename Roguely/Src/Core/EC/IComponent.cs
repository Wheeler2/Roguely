namespace Roguely.Core;

public interface IComponent
{
    public void Init();
    public void Update();
    public void Destroy();
}