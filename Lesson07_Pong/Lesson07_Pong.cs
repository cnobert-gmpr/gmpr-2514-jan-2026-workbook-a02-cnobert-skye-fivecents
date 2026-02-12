using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07_Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450;
    private const int _PlayAreaEdgeLineWidth = 12;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _backgroundTexture;

    private Ball _ball;
    private Paddle _paddleRight, _paddleLeft;

    internal Rectangle PlayAreaBoundingBox
    {
        get
        {
            return new Rectangle(0, _PlayAreaEdgeLineWidth, _WindowWidth, _WindowHeight - 2*_PlayAreaEdgeLineWidth);
        }
    }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ball = new Ball();
        _ball.Initialize(new Vector2(150,195),new Vector2(21,21),new Vector2(-1,-1),100,PlayAreaBoundingBox);

        _paddleRight = new Paddle();
        _paddleRight.Initialize(new Vector2(690,198),new Vector2(8,124),240,PlayAreaBoundingBox);

        _paddleLeft = new Paddle();
        _paddleLeft.Initialize(new Vector2(54,198),new Vector2(8,124),240,PlayAreaBoundingBox);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _paddleRight.LoadContent(Content);
        _paddleLeft.LoadContent(Content);
        _ball.LoadContent(Content);
    }

        protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kbState = Keyboard.GetState();

        _ball.Update(gameTime);

        #region Right Player Movement
            if(kbState.IsKeyDown(Keys.Up))
                _paddleRight.Direction = new Vector2(0,-1);
            else if(kbState.IsKeyDown(Keys.Down))
                _paddleRight.Direction = new Vector2(0,1);
            else
                _paddleRight.Direction = Vector2.Zero;

            _paddleRight.Update(gameTime);
        #endregion

        #region Left Player Movement
            if(kbState.IsKeyDown(Keys.W))
                _paddleLeft.Direction = new Vector2(0,-1);
            else if(kbState.IsKeyDown(Keys.S))
                _paddleLeft.Direction = new Vector2(0,1);
            else
                _paddleLeft.Direction = Vector2.Zero;

            _paddleLeft.Update(gameTime);
        #endregion

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

            _ball.Draw(_spriteBatch);

            _paddleRight.Draw(_spriteBatch);
            _paddleLeft.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}