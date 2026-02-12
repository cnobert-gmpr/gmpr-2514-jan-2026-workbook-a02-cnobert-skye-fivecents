using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson02;

public class IntroGame : Game
{
    // Represents the screen
    private GraphicsDeviceManager _graphics;
    // Batches up draw commands so they can be sent all at once
    private SpriteBatch _spriteBatch;

    private Texture2D _pixel;

    private float _xPosition = 100, _yPosition = 150;
    private float _speed = 150;
    private int _width = 80, _height = 50;

    public IntroGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create new 1x1 pixel texture
        _pixel = new Texture2D(GraphicsDevice,1,1);
        // Set all pixels in texture to white
        _pixel.SetData(new [] {Color.White});

    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // ElapsedGameTime is the delta time
        _xPosition += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Wheat);

        // All draw commands should be within the spritebatch
        _spriteBatch.Begin();
            Rectangle rect = new Rectangle((int)_xPosition,(int)_yPosition,_width,_height);
            _spriteBatch.Draw(_pixel,rect,Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
