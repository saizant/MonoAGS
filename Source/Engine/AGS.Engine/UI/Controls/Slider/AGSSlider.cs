﻿using AGS.API;

namespace AGS.Engine
{
	public partial class AGSSlider
	{
		partial void afterInitComponents(Resolver resolver, IImage image)
		{
			RenderLayer = AGSLayers.UI;
			IgnoreScalingArea = true;
			IgnoreViewport = true;
			Pivot = new PointF ();
			Image = image;
			Enabled = true;
		}        
	}
}

