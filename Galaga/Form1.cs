using System.Windows.Forms;

namespace Galaga {
    public partial class Form1 : Form {
        private GameRenderer _gameRenderer;
        private Game.Game _game;
        
        public Form1() {
            InitializeComponent();

            _game = new Game.Game();
            _gameRenderer = new GameRenderer(this, _game);
        }
    }
}
