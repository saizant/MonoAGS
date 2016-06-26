﻿using System;
using AGS.API;

namespace AGS.Engine
{
    public partial class AGSTextbox
    {
        partial void init(Resolver resolver)
        {
            RenderLayer = AGSLayers.UI;
            IgnoreScalingArea = true;
            IgnoreViewport = true;
            Anchor = new PointF();

            Enabled = true;
        }

        public void ApplySkin(ITextbox textbox)
        {
            throw new NotSupportedException();
        }
    }
}