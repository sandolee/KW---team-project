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
            
            manager.Start();
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
            switch (e.KeyCode)
            {
                case Keys.Right:
                    manager.MovePlayer(5, 0);
                    break;
                case Keys.Left:
                    manager.MovePlayer(-5, 0);
                    break;
                case Keys.Space:
                    manager.Shoot();
                    break;
            }
        }
    }
}
