﻿using Galaga.Game;
using System.Windows.Forms;

#nullable enable

namespace Galaga.Entity
{
    public class Player : Entity
    {
        public event KeyEventHandler? keyPress;
        public Player(World world) : this(new Position(0, 0), world, 10)
        {
            var world1 = world;
        }
        public void Player_KeyPress(object sender, KeyEventArgs e, Game.Game game)
        {
            var world = game.GetWorld();
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (this.Position.X <= World.Size.Width - this.Size.Width)
                        this.Position.X += 7;
                    break;
                case Keys.Left:
                    if (this.Position.X >= this.Size.Width)
                        this.Position.X -= 7;
                    break;
                case Keys.Space:
                    world.EntityManager.AddEntity(new Ammo(new Position(Position.X + 4, Position.Y - 4), world, 1));
                    world.EntityManager.AddEntity(new Ammo(new Position(Position.X - 4, Position.Y - 4), world, 1));
                    break;
                default:
                    break;
            }

        }
        public Player(Position position, World world, int health) : base(
            new Position(world.Size.Width / 2, (int)(world.Size.Height)),
            world,
            new Size(10, 10),
            health
        )
        { }

        public override void OnTick(int currentTick) {
            
        }
    }
}
