using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Roguely.Core;
using Roguely.Core.Components;

namespace Roguely.Entities
{
    /// <summary>
    /// A simple test entity for performance testing.
    /// Moves in a circular pattern and has basic rendering.
    /// </summary>
    public class TestEntity : Entity
    {
        private float _time;
        private float _speed;
        private float _radius;
        private Vector2 _centerPosition;

        public TestEntity(Vector2 centerPosition, float radius = 50f, float speed = 1f)
        {
            _centerPosition = centerPosition;
            _radius = radius;
            _speed = speed;
            _time = (float)(new Random().NextDouble() * Math.PI * 2); // Random starting position

            // Load texture and create components
            Texture2D texture = GameManager.Instance.Content.Load<Texture2D>("Sprites/Barbarian");
            Sprite sprite = new Sprite(texture);
            Transform transform = new Transform();

            // Set initial position
            transform.Position = _centerPosition + new Vector2(
                MathF.Cos(_time) * _radius,
                MathF.Sin(_time) * _radius
            );

            AddComponent(transform);
            AddComponent(new SpriteRenderer(sprite, transform));
        }

        protected override void Update()
        {
            _time += Time.DeltaTime * _speed;

            // Move in a circle
            Transform transform = GetComponent<Transform>();
            transform.Position = _centerPosition + new Vector2(
                MathF.Cos(_time) * _radius,
                MathF.Sin(_time) * _radius
            );
        }
    }
}
