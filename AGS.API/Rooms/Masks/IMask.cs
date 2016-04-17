﻿namespace AGS.API
{
    public interface IMask
	{
		int Width { get; }
		int Height { get; }
		IObject DebugDraw { get; }

		float MinX { get; }
		float MaxX { get; }
		float MinY { get; }
		float MaxY { get; }

		bool IsMasked(PointF point);
		bool IsMasked(PointF point, ISquare projectionBox, float scaleX, float scaleY);
		bool[][] AsJaggedArray();
		bool[,] To2DArray();

		void ApplyToMask(bool[][] mask);
		string DebugString();
	}
}
