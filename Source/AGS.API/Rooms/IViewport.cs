﻿using System.ComponentModel;

namespace AGS.API
{
    /// <summary>
    /// It might be that not all of the room is shown on the screen at once (for example, a scrolling room). 
    /// A viewport to the room instructs the engine on what parts of the room to show.
    /// </summary>
    /// <seealso cref="IRoom"/>
    public interface IViewport : INotifyPropertyChanged
	{
        /// <summary>
        /// The left location of the room from which to show the screen.
        /// </summary>
        /// <value>The x.</value>
		float X { get; set; }

        /// <summary>
        /// The bottom location for the room from which to show the screen.
        /// </summary>
        /// <value>The y.</value>
		float Y { get; set; }

        /// <summary>
        /// The horizontal zoom factor of the room.
        /// </summary>
        /// <value>The scale x.</value>
		float ScaleX { get; set; }

        /// <summary>
        /// The vertical zoom factor of the room.
        /// </summary>
        /// <value>The scale y.</value>
		float ScaleY { get; set; }

        /// <summary>
        /// The rotation angle (in degrees) of the viewport.
        /// </summary>
        /// <value>The angle.</value>
		float Angle { get; set; }

        /// <summary>
        /// Gets or sets the camera which automatically manipulates the viewport to follow a target (usually the player).
        /// </summary>
        /// <value>The camera.</value>
		ICamera Camera { get; set; }

        /// <summary>
        /// Gets or sets the box into which the viewport will project into. The measurement for the box is
        /// the overall factor to the window size. So, for example, a rectangle with (x,y) = (0,0) and 
        /// (width,height) = (1,1) will be projected onto the entire window (this is the default), 
        /// and a rectangle with (x,y) = (0.25,0.25) and (width,height) = (0.5,0.5) will be projected on
        /// half the window and will be centered.
        /// 
        /// Note: the projection box respects the "keep aspect ratio" setting (<see cref="IGameSettings.PreserveAspectRatio"/>),
        /// so if that option is enabled in the settings (on by default), the "window size" is without the black side-bars.
        /// </summary>
        /// <value>The projection box.</value>
        RectangleF ProjectionBox { get; set; }

        /// <summary>
        /// Gets or sets the room provider, which returns the room currently shown by the viewport.
        /// </summary>
        /// <value>The room provider.</value>
        IRoomProvider RoomProvider { get; set; }

        /// <summary>
        /// Can the user interact with objects in the viewport?
        /// </summary>
        /// <value><c>true</c> if interactive; otherwise, <c>false</c>.</value>
        bool Interactive { get; set; }

        /// <summary>
        /// Adds the ability to attach a viewport to an object. 
        /// This makes the <see cref="ProjectionBox"/> of the viewport be relative to that object.
        /// </summary>
        /// <value>The parent.</value>
        IObject Parent { get; set; }

        /// <summary>
        /// Allows to control what will be seen via the viewport.
        /// </summary>
        /// <value>The display list settings.</value>
        IDisplayListSettings DisplayListSettings { get; }

        /// <summary>
        /// Checks whether a given object should be visible in the viewport.
        /// </summary>
        /// <returns><c>true</c>, if object should be visible, <c>false</c> otherwise.</returns>
        /// <param name="obj">Object.</param>
        bool IsObjectVisible(IObject obj);
	}
}

