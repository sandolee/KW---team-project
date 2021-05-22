using System;
using Galaga.Entity.AmmoEntity;
using Galaga.Game;

namespace Galaga.Entity.EnemyEntity {
	public class Boss: Enemy {
		public const int Speed = 3;

		private int _targetX = -1;
		private int _targetY = -1;

		private int _lastMovement = -1;

		private bool _isShooting = false;

		private Random _random = new Random();
		
		public Boss(World world) : this(new Position(0, 0), world) {
		}

		public Boss(Position position, World world) : base(position, world, new Size(10, 10), 60) {
		}

		public override void OnTick(int currentTick) {
			if (_isShooting) {
				World.EntityManager.AddEntity(new StraightEnemyAmmo(new Position(Position.X, Position.Y), World));
			}
			
			if (_targetX == -1 && _targetY == -1) {
				if (currentTick - _lastMovement < _random.Next(20, 60)) {
					return;
				}

				var size = Size;
				
				_targetX = _random.Next(size.Width, World.Size.Width - size.Width);
				_targetY = _random.Next(size.Height, World.Size.Height - size.Height);

				_isShooting = false;
			}

			var dX = _targetX - Position.X;
			var dY = _targetY - Position.Y;
			var distance = Math.Sqrt(dX * dX + dY * dY);
			if (distance < 4) {
				_lastMovement = currentTick;
				_targetX = -1;
				_targetY = -1;

				if (_random.NextDouble() < 0.8) {
					_isShooting = true;
				}
				return;
			}

			var speedX = dX / distance * Speed;
			var speedY = dY / distance * Speed;

			Position.X += (int) Math.Round(speedX);
			Position.Y += (int) Math.Round(speedY);
		}
	}
}
