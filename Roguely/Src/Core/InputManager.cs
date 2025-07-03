using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Roguely.Core.Input;

public static class InputManager
{
    private static Vector2 _moveDirection;
    public static Vector2 MoveDirection => _moveDirection;

    public static Vector2 MousePosition { get; private set; }

    private static KeyboardState _keyboardState;
    private static MouseState _mouseState;

    public static void Update()
    {
        _moveDirection = Vector2.Zero;

        _keyboardState = Keyboard.GetState();
        _mouseState = Mouse.GetState();

        MousePosition = new Vector2(_mouseState.X, _mouseState.Y);

        if (_keyboardState.IsKeyDown(Keys.W))
            _moveDirection.Y--;

        if (_keyboardState.IsKeyDown(Keys.S))
            _moveDirection.Y++;

        if (_keyboardState.IsKeyDown(Keys.A))
            _moveDirection.X--;

        if (_keyboardState.IsKeyDown(Keys.D))
            _moveDirection.X++;


    }
}
