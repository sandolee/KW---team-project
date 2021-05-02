using System;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Game;

namespace Galaga {
    public partial class Form1 : Form {
        private int tick;
        
        private readonly GameRenderer gameRenderer;
        private readonly GameManager manager;
        
        public Form1() {
            InitializeComponent();
            
            manager = new GameManager();
            gameRenderer = new GameRenderer(this, manager);

            var timer = new Timer {
                Interval = 50, Enabled = true,
            };
            timer.Tick += OnTimerTick;
            timer.Start();
            KeyDown += Form1_KeyDown;

            var world = manager.GetWorld();
            // 테스트적 생성
            Enemy enemy = new TestEnemy(manager.GetWorld());
            world.EntityManager.AddEntity(enemy);
        }

        private void OnTimerTick(object obj, EventArgs args) {
            manager.Tick(tick++);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            
            gameRenderer.Draw(e.Graphics);
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            var player = manager.GetPlayer();
            var world = manager.GetWorld();
            
            switch (e.KeyCode)
            {
                case Keys.Right:
                    player.Move(5,0);
                    break;
                case Keys.Left:
                    player.Move(-5, 0);
                    break;
                case Keys.Space:
                    player.World.EntityManager.AddEntity(new Ammo(new Position(player.Position.X + 4, player.Position.Y), player.World, 1));
                    player.World.EntityManager.AddEntity(new Ammo(new Position(player.Position.X - 4, player.Position.Y), player.World, 1));
                    break;
            }
        }
    }
}
