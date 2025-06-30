namespace Roguely.Core;

public interface IComponent
{
    public void Init();
    public void Update(float deltaTime);
    public void Destroy();
}