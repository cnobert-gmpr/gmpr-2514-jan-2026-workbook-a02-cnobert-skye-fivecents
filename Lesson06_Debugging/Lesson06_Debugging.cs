using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson06_Debugging;

public class Lesson06_Debugging : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _pixel;

    private Vector2 _position;
    private Vector2 _dimensions;

    private float _speed;

    public Lesson06_Debugging()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _position = new Vector2(60f, 80f);
        _dimensions = new Vector2(250f, 50f);

        // I changed the speed just to make it easier to see
        _speed = 10f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        Move(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        Rectangle rect = new Rectangle(
            (int)_position.X,
            (int)_position.Y,
            // These dimensions were backwards
            (int)_dimensions.X,
            (int)_dimensions.Y
        );

        // This draw command was using Color.Black
        _spriteBatch.Draw(_pixel, rect, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void Move(GameTime gameTime)
    {
        float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _position.X += _speed * seconds;

        base.Update(gameTime);

        // This line reset the position each time
        // _position = new Vector2(60f, 80f);
    }
}