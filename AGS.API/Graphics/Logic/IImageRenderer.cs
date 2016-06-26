﻿namespace AGS.API
{
    public interface IImageRenderer
	{
		void Prepare(IAnimationContainer obj, IDrawableInfo drawable, IInObjectTree tree, IViewport viewport, PointF areaScaling);
		void Render(IObject obj, IViewport viewport, PointF areaScaling);
	}
}

