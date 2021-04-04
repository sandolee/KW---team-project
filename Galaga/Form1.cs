﻿using System;
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
        }

        private void OnTimerTick(object obj, EventArgs args) {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            
            _game.OnTick(_tick++);
            _gameRenderer.Draw(e.Graphics);
        }
    }
}
