using System;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Game;

namespace Galaga {
    public partial class Form1 : Form {
        private int _tick;
        private string ID;
        private readonly GameRenderer _gameRenderer;
        private readonly GameManager _manager;

        //private Label _label = new Label();
        
        public Form1(string ID) {
            InitializeComponent();
            this.ID = ID;
            //Controls.Add(_label);
            
            _manager = new GameManager();
            _gameRenderer = new GameRenderer(this, _manager);

            var timer = new Timer {
                Interval = 50, Enabled = true
            };
            timer.Tick += OnTimerTick;
            timer.Start();
            KeyDown += Form1_KeyDown;

            var world = _manager.GetWorld();
            
            // 테스트적 생성
            var enemy = new TestEnemy(world);
            world.EntityManager.AddEntity(enemy);
            var bossenemy = new TestBossEnemy(world);
            world.EntityManager.AddEntity(bossenemy);

            // 아이템 테스트 
            Heart heart = new Heart(world);
            world.EntityManager.AddEntity(heart);
            Potion potion = new Potion(world);
            world.EntityManager.AddEntity(potion);

            world.EntityManager.AddEntity(new TestEnemy(_manager.GetWorld()));

            _manager.Start();
            
        }

        private void OnTimerTick(object obj, EventArgs args) {
            _manager.Tick(_tick++);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
             base.OnPaint(e);
            
            _gameRenderer.Draw(e.Graphics);
            
            //var player = _manager.GetPlayer();
            //아이템 테스트를 위한 hp 표시 label 
            //_label.Text = $"{player.Health} ${player.GodMode.GodModeStartTick}";
            //_label.ForeColor = System.Drawing.Color.White;
            //_label.BackColor = System.Drawing.Color.Transparent;
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    _manager.MovePlayer(5, 0);
                    break;
                case Keys.Left:
                    _manager.MovePlayer(-5, 0);
                    break;
                //case Keys.Up:
                //    _player.Move(0,-5);
                //    break;
                //case Keys.Down:
                //    _player.Move(0, 5);
                //    break;
                case Keys.Space:
                    _manager.Shoot();
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // player 정보 갱신 
            FileAccess.FileAccess.UpdateInfo(ID, _manager.Stage,_manager.Game.GetScore());
        }
    }
}
