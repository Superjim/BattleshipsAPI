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
			_destroyer = new Ship("Destroyer", 3, new Coordinate(0, 0), true);
			_carrier = new Ship("Aircraft Carrier", 5, new Coordinate(5, 5), false);
		}

		[Test]
		public void CheckValidHit_HitWithinBounds_ReturnsTrue()
		{
			// Arrange
			Coordinate shotCoordinate = new Coordinate(0, 1);

			// Act
			bool result = _destroyer.CheckValidHit(shotCoordinate, 10);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void CheckValidHit_HitOutOfBounds_ReturnsFalse()
		{
			// Arrange
			Coordinate shotCoordinate = new Coordinate(-1, 1);

			// Act
			bool result = _destroyer.CheckValidHit(shotCoordinate, 10);

			// Asset
			Assert.IsFalse(result);
		}

		[Test]
		public void CheckValidHit_CantHitSameCellTwice_ReturnsFalse()
		{
			// Arrange
			Coordinate shotCoordinate = new Coordinate(0, 1);

			_destroyer.CheckValidHit(shotCoordinate, 10);

			// Act
			bool result = _destroyer.CheckValidHit(shotCoordinate, 10);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void CheckValidHit_AllCellsHit_SetsSunkTrue()
		{
			// Arrange
			int destroyerSize = 3;
			int carrierSize = 5;

			// Act
			for (int i = 0; i < destroyerSize; i++)
			{
				Coordinate shotCoordinate = new Coordinate(0, i);
				_destroyer.CheckValidHit(shotCoordinate, 10);
			}

			for (int i = 5; i < 5 + carrierSize; i++)
			{
				Coordinate shotCoordinate = new Coordinate(i, 5);
				_carrier.CheckValidHit(shotCoordinate, 10);
			}

			// Assert
			Assert.IsTrue(_destroyer.Sunk);
			Assert.IsTrue(_carrier.Sunk);
		}

	}
}