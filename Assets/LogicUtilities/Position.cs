using System;
using UnityEngine;

namespace LogicUtilities
{
	public static class PositionUtilities {
		public static Position Distance(this Position position, Position other) {
			return new(Mathf.Abs(position.Layer - other.Layer), Mathf.Abs(position.Coord - other.Coord));
		}
	}

	[Serializable] public struct Position : IComparable<Position> {
		public int Layer;
		/// <summary>
		/// Coord 与 Order 不同，计算规则: coord = 2 * order + (Layer.IsEven() ? 0 : -1)
		/// </summary>
		public int Coord;

		public Position(int _layer, int _coord) {
			Layer = _layer;
			Coord = _coord;
		}
		
		public void Translate(int dltL, int dltC) {
			Layer += dltL;
			Coord += dltC;
		}

		public static bool operator==(Position left, Position right) {
			return left.Layer == right.Layer && left.Coord == right.Coord;
		}
		public static bool operator!=(Position left, Position right) {
			return left.Layer != right.Layer || left.Coord != right.Coord;
		}
		public static Position operator+(Position left, Position right) {
			return new (left.Layer + right.Layer, left.Coord + right.Coord);
		}
		public static Position operator+(Position left, (int x, int y) pair) {
			return new(left.Layer + pair.x, left.Coord + pair.y);
		}

		public readonly bool RoadDown() { return (Layer % 2) == (Coord % 2); }
		public readonly bool RoadUp() { return !RoadDown(); }

		public readonly int CompareTo(Position other) {
			int compareX = Layer.CompareTo(other.Layer);
			int compareY = Coord.CompareTo(other.Coord);
			return compareX != 0 ? compareX : compareY;
		}
		public override readonly bool Equals(object obj) {
			if (obj is Position other)
				return Layer == other.Layer && Coord == other.Coord;
			return false;
		}
		public override readonly int GetHashCode() {
			return HashCode.Combine(Layer, Coord);
		}
		public override readonly string ToString() {
			return $"({Layer}, {Coord})";
		}
	}
}