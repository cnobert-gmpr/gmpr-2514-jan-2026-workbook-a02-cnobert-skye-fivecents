using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson03_Loops;

public class Lesson03_Game : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _pixel;

    private Vector2 _position, _dimensions;

    private int _count;

    private float _spacing;

    private Rectangle[] _rects;

    public Lesson03_Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _position = new Vector2(50,10);
        _dimensions = new Vector2(60,40);
        _count = 9;
        _spacing = 10;

        _rects = new Rectangle[_count];
        for(int c = 0; c < _count; c++)
        {
            float y = _position.Y + c*(_dimensions.Y + _spacing);
            _rects[c]= new Rectangle((int)_position.X, (int)y, (int)_dimensions.X, (int)_dimensions.Y);
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice,1,1);
        _pixel.SetData(new [] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach(Rectangle rect in _rects)
        {
            _spriteBatch.Draw(_pixel, rect, Color.BlanchedAlmond);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
