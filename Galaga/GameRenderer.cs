#nullable enable

using System.Drawing;
using System.Windows.Forms;
using Galaga.Entity;
using Galaga.Game;

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
                    case Enemy enemy:
                        graphics.DrawImage(Resources.Enemy, EntityToRect(enemy,factorWidth, factorHeight));
                        break;
                    case Ammo ammo:
                        graphics.DrawImage(Resources.Ammo, EntityToRect(ammo, factorWidth, factorHeight));
                        break;
                    case Player player:
                        graphics.DrawImage(Resources.Player, EntityToRect(player, factorWidth, factorHeight));
                        
                        graphics.DrawRectangle(pen, (int) ((player.Position.X - player.Size.Width / 2f) * factorWidth), (int) ((player.Position.Y - player.Size.Height / 2f) * factorHeight*0.9), 
                            (int) (player.Size.Width * factorWidth),  (int) (player.Size.Height * factorHeight/2));
                                              
                        graphics.FillRectangle(Brushes.White, (int) ((player.Position.X - player.Size.Width / 2f) * factorWidth ), (int) ((player.Position.Y - player.Size.Height / 2f) * factorHeight*0.9), 
                            (int) (player.Size.Width * factorWidth),  (int) (player.Size.Height * factorHeight/2));
                                                
                        graphics.FillRectangle(Brushes.Red, (int) ((player.Position.X - player.Size.Width / 2f) * factorWidth + player.Size.Width * factorWidth/10*player.Health), (int) ((player.Position.Y - player.Size.Height / 2f) * factorHeight*0.9), 
                            (int) (player.Size.Width * factorWidth/10*(10-player.Health)),  (int) (player.Size.Height * factorHeight/2));

                        break;
                    case Heart heart:
                        graphics.DrawImage(Resources.Heart, EntityToRect(heart, factorWidth, factorHeight));
                        break;
                    case Potion potion:
                        graphics.DrawImage(Resources.Potion, EntityToRect(potion, factorWidth, factorHeight));
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
        public readonly Image Potion = Properties.Resources.potion;
    }
}
