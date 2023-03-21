namespace BattleshipsAPI.Models
{
	public class GameBoard
	{
		public int BoardSize { get; private set; }

		public GameBoard(int boardSize = 10)
		{
			BoardSize = boardSize;
		}
	}
}
