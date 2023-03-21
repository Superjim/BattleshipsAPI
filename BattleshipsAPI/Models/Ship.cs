namespace BattleshipsAPI.Models
{
	public class Ship
	{
		public string Name { get; }
		public int Size { get; }
		public bool Sunk { get; private set; }
		public List<(int row, int col)> Hits { get; }
		public (int row, int col) StartPosition { get; }
		public bool Horizontal { get; }

		public Ship(string name, int size, (int row, int col) startPosition, bool horizontal)
		{
			Name = name;
			Size = size;
			Sunk = false;
			Hits = new List<(int row, int col)>();
			StartPosition = startPosition;
			Horizontal = horizontal;

		}

		public bool CheckValidHit(int row, int column, int boardSize)
		{
			//shot is outside the game board
			if (row < 0 || column < 0 || row >= boardSize || column >= boardSize)
			{
				throw new ArgumentOutOfRangeException("Shot out of bounds");
			}

			//shot misses the ship
			if (Horizontal)
			{
				if (row != StartPosition.row)
				{
					return false;
				}

				if (column < StartPosition.col || column >= StartPosition.col + Size)
				{
					return false;
				}
			}
			else
			{
				if (column != StartPosition.col)
				{
					return false;
				}

				if (row < StartPosition.row || row >= StartPosition.row + Size)
				{
					return false;
				}
			}

			//check the shot location isn't already stored in the hits list
			if (Hits.Any(hit => hit.row == row && hit.col == column))
			{
				return false;
			}

			//ship is sunk
			Hits.Add((row, column));
			if (Hits.Count == Size)
			{
				Sunk = true;
			}

			return true;
		}
	}
}