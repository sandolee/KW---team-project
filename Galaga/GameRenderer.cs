#nullable enable

using System;
using System.Drawing;
using System.Windows.Forms;
using Galaga.Entity;

namespace Galaga {
    public class GameRenderer {
        private readonly Game.Game _game;
        private readonly Control _control;

        private readonly ImageResources _resources = new ImageResources();

        public GameRenderer(Control form, Game.Game game) {
            _control = form;
            _game = game;
        }

        public void Draw(Graphics graphics) {
            var world = _game.GetWorld();
            if (world == null) return;

            var entities = world.EntityManager.Entities;

            float width = _control.DisplayRectangle.Right - _control.DisplayRectangle.Left;
            float height = _control.DisplayRectangle.Bottom - _control.DisplayRectangle.Top;
            var factor = Math.Min(width / world.Size.Width, height / world.Size.Height);

            graphics.FillRectangle(Brushes.Black, 0, 0, world.Size.Width * factor, world.Size.Height * factor);

            foreach (var entity in entities) {
                switch (entity) {
                    case Enemy enemy:
                        graphics.DrawImage(_resources.Enemy, EntityToRect(enemy, factor));
                        break;
                    case Ammo ammo:
                        graphics.DrawImage(_resources.Ammo, EntityToRect(ammo, factor));
                        break;
                    case Player player:
                        graphics.DrawImage(_resources.Player, EntityToRect(player, factor));
                        break;
                }
            }
        }

        private static Point PositionToPoint(Position position, float factor) {
            return new Point((int) (position.X * factor), (int) (position.Y * factor));
        }

        private static Rectangle EntityToRect(Entity.Entity entity, float factor) {
            var width = entity.Size.Width;
            var height = entity.Size.Height;

            return new Rectangle(
                (int) ((entity.Position.X - width / 2f) * factor),
                (int) ((entity.Position.Y - height / 2f) * factor),
                (int) (width * factor),
                (int) (height * factor)
            );
        }
    }

    internal class ImageResources {
        public readonly Image Ammo = Properties.Resources.ammo;
        public readonly Image Enemy = Properties.Resources.Enemy;
        public readonly Image Player = Properties.Resources.Entity1;
    }
}
