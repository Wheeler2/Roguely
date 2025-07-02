using Microsoft.Xna.Framework;
using Roguely.Core.Components;
using Roguely.Core.Rendering;

namespace Roguely.Core.Entities;

public class Camera : Entity
{
    private static Camera _mainCamera;
    public static Camera Main
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = new Camera();
            }
            return _mainCamera;
        }
    }

    public Camera()
    {
        AddComponent(new Transform());

        // Initialize camera bounds to a default size
        _cameraViewPortSize = new Rectangle(0, 0, 720, 480);

        // Set the camera to be the main camera
        if (_mainCamera == null)
            _mainCamera = this;
    }

    private Rectangle _cameraViewPortSize;
    private Matrix _viewMatrix = Matrix.Identity;
    public Matrix ViewMatrix => _viewMatrix;

    public void SetViewPortSize(int width, int height)
    {
        _cameraViewPortSize = new Rectangle(0, 0, width, height);
    }

    protected override void Update()
    {
        // Center the camera to its transform position
        Transform transform = GetComponent<Transform>();
        if (transform == null)
            return;

        // Calculate the translation matrix based on the camera's position
        Vector2 position = transform.Position;
        Vector2 scale = CalculateScale();

        _viewMatrix = Matrix.CreateTranslation((_cameraViewPortSize.Width * 0.5f) - position.X, (_cameraViewPortSize.Height * 0.5f) - position.Y, 0)
                    * Matrix.CreateScale(scale.X, scale.Y, 1);
    }

    private Vector2 CalculateScale()
    {
        // Calculate the scale based on the camera bounds and the viewport size
        var viewport = RendererManager.GraphicsDevice.Viewport;
        float scaleX = (float)viewport.Width / _cameraViewPortSize.Width;
        float scaleY = (float)viewport.Height / _cameraViewPortSize.Height;

        return new Vector2(scaleX, scaleY);
    }
}