﻿using System;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Game;

namespace Galaga {
    public partial class Form1 : Form {
        private int _tick;
        
        private readonly GameRenderer _gameRenderer;
        private readonly GameManager _manager;
        
        public Form1() {
            InitializeComponent();
            
            _manager = new GameManager();
            _gameRenderer = new GameRenderer(this, _manager);

            var timer = new Timer {
                Interval = 50, Enabled = true,
            };
            timer.Tick += OnTimerTick;
            timer.Start();
            KeyDown += Form1_KeyDown;

            var world = _manager.GetWorld();
            // 테스트적 생성
            Enemy enemy = new TestEnemy(_manager.GetWorld());
            world.EntityManager.AddEntity(enemy);
            
            _manager.Start();
        }

        private void OnTimerTick(object obj, EventArgs args) {
            _manager.Tick(_tick++);

            Invalidate();
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
                case Keys.Space:
                    _manager.Shoot();
                    break;
            }
        }
    }
}
