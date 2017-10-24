﻿using AGS.API;
using System.Threading.Tasks;

namespace AGS.Engine
{
    [PropertyFolder]
	public class AGSAnimationState : IAnimationState
	{
		public AGSAnimationState ()
		{
			OnAnimationCompleted = new TaskCompletionSource<AnimationCompletedEventArgs> ();
		}

		#region IAnimationState implementation

		public bool RunningBackwards { get; set; }

		public int CurrentFrame { get; set; }

		public int CurrentLoop { get; set; }

		public int TimeToNextFrame { get; set; }

		public bool IsPaused { get; set; }

        [Property(Browsable = false)]
		public TaskCompletionSource<AnimationCompletedEventArgs> OnAnimationCompleted { get; private set; }

		#endregion
	}
}

