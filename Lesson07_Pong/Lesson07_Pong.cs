using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07_Pong;

public class Pong : Game
{
    private const int _WindowWidth = 750, _WindowHeight = 450;
    private const int _PlayAreaEdgeLineWidth = 12;
    private const int _PaddleWidth = 8, _PaddleHeight = 124;
    private const float _PaddleSpeed = 240;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    private Texture2D _backgroundTexture, _paddleTexture;

    private Ball _ball;
    private Vector2 _paddlePosition, _paddleDirection, _paddleDimensions;
    private Vector2 _paddle2Position, _paddle2Direction, _paddle2Dimensions;
    private float _paddleSpeed, _paddle2Speed;


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

        _paddlePosition = new Vector2(690, 198);
        _paddleSpeed = _PaddleSpeed;
        _paddleDimensions = new Vector2(_PaddleWidth,_PaddleHeight);
        _paddleDirection = Vector2.Zero;

        _paddle2Position = new Vector2(52, 198);
        _paddle2Speed = _PaddleSpeed;
        _paddle2Dimensions = new Vector2(_PaddleWidth,_PaddleHeight);
        _paddle2Direction = Vector2.Zero;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _paddleTexture = Content.Load<Texture2D>("Paddle");
        _ball.LoadContent(Content);
    }

        protected override void Update(GameTime gameTime)
    {
        float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kbState = Keyboard.GetState();

        _ball.Update(gameTime);

        #region Right Player Movement
            if(kbState.IsKeyDown(Keys.Up))
            {
                _paddleDirection = new Vector2(0,-1);
            }
            else if(kbState.IsKeyDown(Keys.Down))
            {
                _paddleDirection = new Vector2(0,1);
            }
            else
            {
                _paddleDirection = Vector2.Zero;
            }

            _paddlePosition += _paddleDirection * _paddleSpeed * dt;

            if(_paddlePosition.Y <= PlayAreaBoundingBox.Top)
            {
                _paddlePosition.Y = PlayAreaBoundingBox.Top;   
            }
            else if(_paddlePosition.Y + _paddleDimensions.Y >= PlayAreaBoundingBox.Bottom)
            {
                _paddlePosition.Y = PlayAreaBoundingBox.Bottom - _paddleDimensions.Y;
            }
        #endregion

        #region Left Player Movement
            if(kbState.IsKeyDown(Keys.W))
            {
                _paddle2Direction = new Vector2(0,-1);
            }
            else if(kbState.IsKeyDown(Keys.S))
            {
                _paddle2Direction = new Vector2(0,1);
            }
            else
            {
                _paddle2Direction = Vector2.Zero;
            }

            _paddle2Position += _paddle2Direction * _paddle2Speed * dt;

            if(_paddle2Position.Y <= PlayAreaBoundingBox.Top)
            {
                _paddle2Position.Y = PlayAreaBoundingBox.Top;   
            }
            else if(_paddle2Position.Y + _paddle2Dimensions.Y >= PlayAreaBoundingBox.Bottom)
            {
                _paddle2Position.Y = PlayAreaBoundingBox.Bottom - _paddle2Dimensions.Y;
            }
        #endregion

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

            _ball.Draw(_spriteBatch);

            Rectangle paddleRectangle = new Rectangle((int)_paddlePosition.X, (int)_paddlePosition.Y, (int)_paddleDimensions.X, (int)_paddleDimensions.Y);
            _spriteBatch.Draw(_paddleTexture, paddleRectangle, Color.White);

            Rectangle paddle2Rectangle = new Rectangle((int)_paddle2Position.X, (int)_paddle2Position.Y, (int)_paddle2Dimensions.X, (int)_paddle2Dimensions.Y);
            _spriteBatch.Draw(_paddleTexture, paddle2Rectangle, Color.White);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}