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

        private readonly Timer _timer;

        //private Label _label = new Label();
        
        public Form1(string ID) {
            InitializeComponent();
            this.ID = ID;
            //Controls.Add(_label);
            
            _manager = new GameManager();
            _gameRenderer = new GameRenderer(this, _manager);

            _timer = new Timer {
                Interval = 50, Enabled = true
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
            KeyDown += Form1_KeyDown;

            var world = _manager.GetWorld();
            
            // 테스트적 생성
            var enemy = new TestEnemy(world);
            world.EntityManager.AddEntity(enemy);
            var bossenemy = new TestBossEnemy(world);
            world.EntityManager.AddEntity(bossenemy);


            world.EntityManager.AddEntity(new TestEnemy(_manager.GetWorld()));

            _manager.Start();
        }

        private void OnTimerTick(object obj, EventArgs args) {
            if (_manager.State is GameOverState) {
                _timer.Enabled = false;
                
                MessageBox.Show("게임 오버!", "게임 오버");
                Close();
            } else if (_manager.State is CompleteState) {
                _timer.Enabled = false;

                MessageBox.Show("게임 클리어", "게임 클리어");
                Close();
            }else {
                _manager.Tick(_tick++);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
             base.OnPaint(e);
            
            _gameRenderer.Draw(e.Graphics);
            
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
