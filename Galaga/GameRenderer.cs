#nullable enable

using System.Drawing;
using System.Windows.Forms;

namespace Galaga {
    public class GameRenderer {
        private Graphics _graphics;
        private readonly Game.Game _game;
        
        public GameRenderer(Control form, Game.Game game) {
            _graphics = form.CreateGraphics();
            _game = game;
        }

        public void Draw() {
            // TODO draw background

            var entities = _game.GetEntityManager()?.Entities;
            if (entities == null) return;

            foreach (var entity in entities) {
                // TODO draw entity
            }
        }
    }
}
