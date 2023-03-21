using NUnit.Framework;
using BattleshipsAPI.Models;

namespace BattleshipsAPI.Tests
{
	[TestFixture]
	public class ShipTests
	{
		private Ship _destroyer;
		private Ship _carrier;

		[SetUp]
		public void SetUp()
		{
			_destroyer = new Ship("Destroyer", 3, (0, 0), true);
			_carrier = new Ship("Aircraft Carrier", 5, (5, 5), false);
		}

		[Test]
		public void CheckValidHit_HitWithinBounds_ReturnsTrue()
		{
			// Arrange
			int row = 0;
			int col = 1;

			// Act
			bool result = _destroyer.CheckValidHit(row, col, 10);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void CheckValidHit_HitOutOfBounds_ThrowsError()
		{
			// Arrange
			int row = -1;
			int col = 1;

			// Act + Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => _destroyer.CheckValidHit(row, col, 10));
		}

		[Test]
		public void CheckValidHit_Cant_ReturnsFalse()
		{
			// Arrange
			int row = 0;
			int col = 1;
			_destroyer.CheckValidHit(row, col, 10);

			// Act
			bool result = _destroyer.CheckValidHit(row, col, 10);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void CheckValidHit_AllCellsHit_SetsSunkTrue()
		{
			// Arrange
			_destroyer.CheckValidHit(0, 0, 10);
			_destroyer.CheckValidHit(0, 1, 10);

			_carrier.CheckValidHit(5, 5, 10);
			_carrier.CheckValidHit(6, 5, 10);
			_carrier.CheckValidHit(7, 5, 10);
			_carrier.CheckValidHit(8, 5, 10);


			// Act
			_destroyer.CheckValidHit(0, 2, 10);
			_carrier.CheckValidHit(9, 5, 10);

			// Assert
			Assert.IsTrue(_destroyer.Sunk);
			Assert.IsTrue(_carrier.Sunk);
		}
	}
}