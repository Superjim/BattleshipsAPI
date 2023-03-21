namespace BattleshipsAPI.Models
{
	public class Ship
	{
		public string Name { get; }
		public int Size { get; }
		public bool Sunk { get; private set; }
		public List<Coordinate> Hits { get; }
		public Coordinate StartPosition { get; }
		public bool Horizontal { get; }

		public Ship(string name, int size, Coordinate startPosition, bool horizontal)
		{
			Name = name;
			Size = size;
			Sunk = false;
			Hits = new List<Coordinate>();
			StartPosition = startPosition;
			Horizontal = horizontal;
		}

		public bool CheckValidHit(Coordinate shotCoordinate, int boardSize)
		{
			int row = shotCoordinate.Row;
			int col = shotCoordinate.Column;

			// shot is outside the game board
			if (row < 0 || col < 0 || row >= boardSize || col >= boardSize)
			{
				return false;
			}

			// shot misses the ship
			if (Horizontal)
			{
				if (row != StartPosition.Row || col < StartPosition.Column || col >= StartPosition.Column + Size)
				{
					return false;
				}
			}
			else
			{
				if (col != StartPosition.Column || row < StartPosition.Row || row >= StartPosition.Row + Size)
				{
					return false;
				}
			}

			// check shot location isn't already in the hits list
			if (Hits.Any(hit => hit.Row == row && hit.Column == col))
			{
				return false;
			}

			// ship is hit
			Hits.Add(shotCoordinate);
			if (Hits.Count == Size)
			{
				Sunk = true;
			}

			return true;
		}
	}
}