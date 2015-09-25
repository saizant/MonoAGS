﻿using System;
using System.Threading.Tasks;

namespace AGS.API
{
	public interface IAnimationContainer : ISprite
	{
		IAnimation Animation { get; }
		bool Visible { get; set; }
		bool DebugDrawAnchor { get; set; }

		IBorderStyle Border { get; set; }

		void StartAnimation(IAnimation animation);
		AnimationCompletedEventArgs Animate(IAnimation animation);
		Task<AnimationCompletedEventArgs> AnimateAsync(IAnimation animation);
	}
}
