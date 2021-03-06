﻿namespace AGS.API
{
    /// <summary>
    /// The model matrix is used for rendering and hit-testing the object.
    /// </summary>
    public struct ModelMatrices
    {
        /// <summary>
        /// The object's matrix calculated in the object's resolution (used for rendering).
        /// </summary>
        public Matrix4 InObjResolutionMatrix;
        /// <summary>
        /// The object's matrix calculated in the game's resoution (used for hit-testing).
        /// </summary>
        public Matrix4 InVirtualResolutionMatrix;
    }

    /// <summary>
    /// A component to calculate the matrix used to render/hit-test the object.
    /// The matrix includes transormations, rotations and scale.
    /// </summary>
    [RequiredComponent(typeof(IScaleComponent))]
    [RequiredComponent(typeof(ITranslateComponent))]
    [RequiredComponent(typeof(IAnimationComponent))]
    [RequiredComponent(typeof(IRotateComponent))]
    [RequiredComponent(typeof(IImageComponent))]
    [RequiredComponent(typeof(IHasRoomComponent))]
    [RequiredComponent(typeof(IDrawableInfoComponent))]
    [RequiredComponent(typeof(IInObjectTreeComponent))]
    public interface IModelMatrixComponent : IComponent
    {
        /// <summary>
        /// Gets the model matrices.
        /// </summary>
        /// <returns>The model matrices.</returns>
        ref ModelMatrices GetModelMatrices();

        /// <summary>
        /// An event that fires whenever the matrix changes.
        /// </summary>
        /// <value>The on matrix changed.</value>
        IBlockingEvent OnMatrixChanged { get; }

        /// <summary>
        /// Allows locking the component from changing (to allow for changing multiple components "at once").
        /// </summary>
        /// <value>The lock step.</value>
        ILockStep ModelMatrixLockStep { get; }
    }
}
