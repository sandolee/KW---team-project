#nullable enable

namespace Galaga.Game {
	public class GameManager {
		private Game? game = null;

		public GameManager(Game? game = null) {
			this.game = game;
		}
	}
}
