using System;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Game;

namespace Galaga {
    public partial class Form1 : Form {
        private int _tick;
        
        private GameRenderer _gameRenderer;
        private Game.Game _game;
        private Player _player;
        
        public Form1() {
            InitializeComponent();
            var world = new World(new EnemySpawnerImpl());
            _player = new Player(world);
            _game = new Game.Game(new SimpleGameDelegate(world));

            _gameRenderer = new GameRenderer(this, _game);

            world.EntityManager.AddEntity(_player);

            var timer = new Timer {
                Interval = 50, Enabled = true,
            };
            timer.Tick += OnTimerTick;
            timer.Start();
            KeyDown += Form1_KeyDown;
        }

        private void OnTimerTick(object obj, EventArgs args) {
            _game.OnTick(_tick++);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            
            _gameRenderer.Draw(e.Graphics);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _player.Move(5,0);
                    break;
                case Keys.Left:

                    _player.Move(-5, 0);
                    break;
                case Keys.Space:
                    _game.GetWorld().EntityManager.AddEntity(new Ammo(new Position(_player.Position.X + 4, _player.Position.Y - 4), _game.GetWorld(), 1));
                    _game.GetWorld().EntityManager.AddEntity(new Ammo(new Position(_player.Position.X - 4, _player.Position.Y - 4), _game.GetWorld(), 1));
                    break;
                default:
                    break;
            }
        }

    }
}
