#nullable enable

using System.Drawing;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Entity.AmmoEntity;
using Galaga.Entity.EnemyEntity;
using Galaga.Game;
using System.IO;

namespace Galaga {
    public class GameRenderer {
        private readonly GameManager _manager;
        private readonly Control _control;

        private static readonly ImageResources Resources = new ImageResources();

        public GameRenderer(Control form, GameManager manager) {
            _control = form;
            this._manager = manager;
        }

        public void Draw(Graphics graphics) {
            // TODO GameManager.State에 따라 다른 형태의 화면 표시가 필요함
            
            var world = _manager.GetWorld();

            var entities = world.EntityManager.Entities;

            float width = _control.DisplayRectangle.Right - _control.DisplayRectangle.Left;
            float height = _control.DisplayRectangle.Bottom - _control.DisplayRectangle.Top;

            var factorWidth = width / world.Size.Width;
            var factorHeight = height / world.Size.Height;
            graphics.FillRectangle(Brushes.Black, 0, 0, world.Size.Width * factorWidth, world.Size.Height * factorHeight);
            graphics.DrawString($@"Game state: {_manager.State switch {
                PlayingState _ => "Playing",
                PausedState _ => "Paused",
                CompleteState _ => "Complete",
                CompleteAllState _ => "CompleteAll",
                IntermediateState _ => "Intermediate",
                GameOverState _ => "GameOver",
                _ => "Else"
            }}", new Font("Arial", 16), Brushes.White, 0, 0);

            graphics.FillRectangle(Brushes.Black, 0, 0, world.Size.Width * factorWidth, world.Size.Height * factorHeight);
            Pen pen = new Pen(Brushes.DeepSkyBlue);
            foreach (var entity in entities)
            {
                switch (entity)
                {
                    case Boss boss:
                        graphics.DrawImage(Resources.Boss, EntityToRect(boss, factorWidth, factorHeight));

                        graphics.DrawRectangle(pen, (int)((boss.Position.X - boss.Size.Width / 2f) * factorWidth), (int)((boss.Position.Y - boss.Size.Height) * factorHeight),
                            (int)(boss.Size.Width * factorWidth), (int)(boss.Size.Height * factorHeight / 2));

                        graphics.FillRectangle(Brushes.Red, (int)((boss.Position.X - boss.Size.Width / 2f) * factorWidth), (int)((boss.Position.Y - boss.Size.Height) * factorHeight),
                            (int)(boss.Size.Width * factorWidth / boss.MaxHealth * boss.Health), (int)(boss.Size.Height * factorHeight / 2));

                        break;
                    case Enemy enemy:
                        graphics.DrawImage(Resources.Enemy, EntityToRect(enemy, factorWidth, factorHeight));

                        graphics.DrawRectangle(pen, (int)((enemy.Position.X - enemy.Size.Width / 2f) * factorWidth), (int)((enemy.Position.Y - enemy.Size.Height) * factorHeight),
                            (int)(enemy.Size.Width * factorWidth), (int)(enemy.Size.Height * factorHeight / 2));

                        //graphics.FillRectangle(Brushes.Black, (int) ((enemy.Position.X - enemy.Size.Width / 2f) * factorWidth ), (int) ((enemy.Position.Y - enemy.Size.Height ) * factorHeight), 
                        //(int) (enemy.Size.Width * factorWidth),  (int) (enemy.Size.Height * factorHeight/2));

                        graphics.FillRectangle(Brushes.Red, (int)((enemy.Position.X - enemy.Size.Width / 2f) * factorWidth), (int)((enemy.Position.Y - enemy.Size.Height) * factorHeight),
                            (int)(enemy.Size.Width * factorWidth / enemy.MaxHealth * enemy.Health), (int)(enemy.Size.Height * factorHeight / 2));

                        break;
                    case Ammo ammo:
                        graphics.DrawImage(Resources.Ammo, EntityToRect(ammo, factorWidth, factorHeight));
                        break;
                    case Player player:
                        if(!player.GodMode.IsGodMode) {
                            graphics.DrawImage(Resources.Player, EntityToRect(player, factorWidth, factorHeight));
                        }else{
                            graphics.DrawImage(Resources.God, EntityToRect(player, factorWidth, factorHeight));
                        }
                        graphics.DrawRectangle(pen, (int)((player.Position.X - player.Size.Width / 2f) * factorWidth), (int)((player.Position.Y - player.Size.Height / 2f) * factorHeight * 0.9),
                            (int)(player.Size.Width * factorWidth), (int)(player.Size.Height * factorHeight / 2));

                        graphics.FillRectangle(Brushes.White, (int)((player.Position.X - player.Size.Width / 2f) * factorWidth), (int)((player.Position.Y - player.Size.Height / 2f) * factorHeight * 0.9),
                            (int)(player.Size.Width * factorWidth), (int)(player.Size.Height * factorHeight / 2));

                        graphics.FillRectangle(Brushes.Red, (int)((player.Position.X - player.Size.Width / 2f) * factorWidth + player.Size.Width * factorWidth / player.MaxHealth * player.Health), (int)((player.Position.Y - player.Size.Height / 2f) * factorHeight * 0.9),
                            (int)(player.Size.Width * factorWidth / 10 * (10 - player.Health)), (int)(player.Size.Height * factorHeight / 2));
                        break;
                    case Heart heart:
                        graphics.DrawImage(Resources.Heart, EntityToRect(heart, factorWidth, factorHeight));
                        break;
                    case Potion potion:
                        graphics.DrawImage(Resources.Potion, EntityToRect(potion, factorWidth, factorHeight));
                        break;
                    case StraightEnemyAmmo _:
                        graphics.DrawImage(Resources.EnemyAmmo, EntityToRect(entity, factorWidth, factorHeight));
                        break;
                    default:
                        throw new InvalidDataException("unknown entity type");

                }
            }
            foreach (Control c in _control.Controls)
            {
                if (c.Name=="lblScore")
                {
                    c.Text = _manager.Game.GetScore().ToString();
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
        public readonly Image Ammo = Properties.Resources.Ammo2;
        public readonly Image EnemyAmmo = Properties.Resources.Ammo1;
        public readonly Image Enemy = Properties.Resources.Enemy2;
        public readonly Image Player = Properties.Resources.Player;
        public readonly Image Heart = Properties.Resources.Heart2;
        public readonly Image Potion = Properties.Resources.Potion2;
        public readonly Image Boss = Properties.Resources.Boss;
        public readonly Image God = Properties.Resources.GodPalyer;

        public ImageResources() {
            EnemyAmmo.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }
    }
}
