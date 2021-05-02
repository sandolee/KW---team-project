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

            var factorWidth = width / world.Size.Width;
            var factorHeight = height / world.Size.Height;
            graphics.FillRectangle(Brushes.Black, 0, 0, world.Size.Width * factorWidth, world.Size.Height * factorHeight);

            foreach (var entity in entities)
            {
                switch (entity)
                {
                    case Enemy enemy:
                        graphics.DrawImage(_resources.Enemy, EntityToRect(enemy,factorWidth, factorHeight));
                        break;
                    case Ammo ammo:
                        graphics.DrawImage(_resources.Ammo, EntityToRect(ammo, factorWidth, factorHeight));
                        break;
                    case Player player:
                        graphics.DrawImage(_resources.Player, EntityToRect(player, factorWidth, factorHeight));
                        break;
                    case Heart heart:
                        graphics.DrawImage(_resources.Heart, EntityToRect(heart, factorWidth, factorHeight));
                        break;
                    case Portion portion:
                        graphics.DrawImage(_resources.Portion, EntityToRect(portion, factorWidth, factorHeight));
                        break;
                }
            }
        }

        private static Point PositionToPoint(Position position, float factor) {
            return new Point((int) (position.X * factor), (int) (position.Y * factor));
        }

        private static Rectangle EntityToRect(Entity.Entity entity, float factorWidth, float factorHeight) {
            var width = entity.Size.Width;
            var height = entity.Size.Height;

            return new Rectangle(
                (int) ((entity.Position.X - width / 2f) * factorWidth),
                (int) ((entity.Position.Y - height / 2f) * factorHeight),
                (int) (width * factorWidth),
                (int) (height * factorHeight)
            );
        }
    }

    internal class ImageResources {
        public readonly Image Ammo = Properties.Resources.ammo;
        public readonly Image Enemy = Properties.Resources.Enemy;
        public readonly Image Player = Properties.Resources.Entity1;
        public readonly Image Heart = Properties.Resources.heart;
        public readonly Image Portion = Properties.Resources.potion;
    }
}
