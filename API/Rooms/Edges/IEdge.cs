﻿using System;

namespace AGS.API
{
	public interface IEdge
	{
		float Value { get; set; }
		IEvent<AGSEventArgs> OnEdgeCrossed { get; }
	}
}

