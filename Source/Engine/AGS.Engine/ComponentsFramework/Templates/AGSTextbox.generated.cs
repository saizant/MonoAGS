﻿

//Generation Time: 8/13/2017 10:19:28 PM
//This class was automatically generated by a T4 template.
//Making manual changes in this class might be overridden if the template will be processed again.
//If you want to add functionality you might be able to do this via another partial class definition for this class.

using System;
using AGS.API;
using AGS.Engine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AGS.Engine
{
    public partial class AGSTextbox : AGSEntity, ITextBox
    {
        private IUIEvents _uIEvents;
        private ISkinComponent _skinComponent;
        private IHasRoom _hasRoom;
        private IAnimationContainer _animationContainer;
        private IInObjectTree _inObjectTree;
        private ICollider _collider;
        private IVisibleComponent _visibleComponent;
        private IEnabledComponent _enabledComponent;
        private ICustomPropertiesComponent _customPropertiesComponent;
        private IDrawableInfo _drawableInfo;
        private IHotspotComponent _hotspotComponent;
        private IShaderComponent _shaderComponent;
        private ITranslateComponent _translateComponent;
        private IImageComponent _imageComponent;
        private IScaleComponent _scaleComponent;
        private IRotateComponent _rotateComponent;
        private IPixelPerfectComponent _pixelPerfectComponent;
        private IModelMatrixComponent _modelMatrixComponent;
        private IBoundingBoxComponent _boundingBoxComponent;
        private ITextComponent _textComponent;
        private ITextBoxComponent _textBoxComponent;

        public AGSTextbox(string id, Resolver resolver) : base(id, resolver)
        {            
            _uIEvents = AddComponent<IUIEvents>();
            Bind<IUIEvents>(c => _uIEvents = c, _ => {});            
            _skinComponent = AddComponent<ISkinComponent>();
            Bind<ISkinComponent>(c => _skinComponent = c, _ => {});            
            _hasRoom = AddComponent<IHasRoom>();
            Bind<IHasRoom>(c => _hasRoom = c, _ => {});            
            _animationContainer = AddComponent<IAnimationContainer>();
            Bind<IAnimationContainer>(c => _animationContainer = c, _ => {});            
            _inObjectTree = AddComponent<IInObjectTree>();
            Bind<IInObjectTree>(c => _inObjectTree = c, _ => {});            
            _collider = AddComponent<ICollider>();
            Bind<ICollider>(c => _collider = c, _ => {});            
            _visibleComponent = AddComponent<IVisibleComponent>();
            Bind<IVisibleComponent>(c => _visibleComponent = c, _ => {});            
            _enabledComponent = AddComponent<IEnabledComponent>();
            Bind<IEnabledComponent>(c => _enabledComponent = c, _ => {});            
            _customPropertiesComponent = AddComponent<ICustomPropertiesComponent>();
            Bind<ICustomPropertiesComponent>(c => _customPropertiesComponent = c, _ => {});            
            _drawableInfo = AddComponent<IDrawableInfo>();
            Bind<IDrawableInfo>(c => _drawableInfo = c, _ => {});            
            _hotspotComponent = AddComponent<IHotspotComponent>();
            Bind<IHotspotComponent>(c => _hotspotComponent = c, _ => {});            
            _shaderComponent = AddComponent<IShaderComponent>();
            Bind<IShaderComponent>(c => _shaderComponent = c, _ => {});            
            _translateComponent = AddComponent<ITranslateComponent>();
            Bind<ITranslateComponent>(c => _translateComponent = c, _ => {});            
            _imageComponent = AddComponent<IImageComponent>();
            Bind<IImageComponent>(c => _imageComponent = c, _ => {});            
            _scaleComponent = AddComponent<IScaleComponent>();
            Bind<IScaleComponent>(c => _scaleComponent = c, _ => {});            
            _rotateComponent = AddComponent<IRotateComponent>();
            Bind<IRotateComponent>(c => _rotateComponent = c, _ => {});            
            _pixelPerfectComponent = AddComponent<IPixelPerfectComponent>();
            Bind<IPixelPerfectComponent>(c => _pixelPerfectComponent = c, _ => {});            
            _modelMatrixComponent = AddComponent<IModelMatrixComponent>();
            Bind<IModelMatrixComponent>(c => _modelMatrixComponent = c, _ => {});            
            _boundingBoxComponent = AddComponent<IBoundingBoxComponent>();
            Bind<IBoundingBoxComponent>(c => _boundingBoxComponent = c, _ => {});            
            _textComponent = AddComponent<ITextComponent>();
            Bind<ITextComponent>(c => _textComponent = c, _ => {});            
            _textBoxComponent = AddComponent<ITextBoxComponent>();
            Bind<ITextBoxComponent>(c => _textBoxComponent = c, _ => {});
			beforeInitComponents(resolver);
            InitComponents();
            afterInitComponents(resolver);
            }

        public string Name { get { return ID; } }
        public bool AllowMultiple { get { return false; } }
        public void Init(IEntity entity) {}

        public override string ToString()
        {
            return string.Format("{0} ({1})", ID ?? "", GetType().Name);
        }

        partial void beforeInitComponents(Resolver resolver);
		partial void afterInitComponents(Resolver resolver);

        #region IUIEvents implementation

        public IEvent<MousePositionEventArgs> MouseEnter 
        {  
            get { return _uIEvents.MouseEnter; } 
        }

        public IEvent<MousePositionEventArgs> MouseLeave 
        {  
            get { return _uIEvents.MouseLeave; } 
        }

        public IEvent<MousePositionEventArgs> MouseMove 
        {  
            get { return _uIEvents.MouseMove; } 
        }

        public IEvent<MouseButtonEventArgs> MouseClicked 
        {  
            get { return _uIEvents.MouseClicked; } 
        }

        public IEvent<MouseButtonEventArgs> MouseDoubleClicked 
        {  
            get { return _uIEvents.MouseDoubleClicked; } 
        }

        public IEvent<MouseButtonEventArgs> MouseDown 
        {  
            get { return _uIEvents.MouseDown; } 
        }

        public IEvent<MouseButtonEventArgs> MouseUp 
        {  
            get { return _uIEvents.MouseUp; } 
        }

        public IEvent<MouseButtonEventArgs> LostFocus 
        {  
            get { return _uIEvents.LostFocus; } 
        }

        public Boolean IsMouseIn 
        {  
            get { return _uIEvents.IsMouseIn; } 
        }

        #endregion

        #region ISkinComponent implementation

        public ISkin Skin 
        {  
            get { return _skinComponent.Skin; }  
            set { _skinComponent.Skin = value; } 
        }

        public IConcurrentHashSet<String> SkinTags 
        {  
            get { return _skinComponent.SkinTags; } 
        }

        #endregion

        #region IHasRoom implementation

        public IRoom Room 
        {  
            get { return _hasRoom.Room; } 
        }

        public IRoom PreviousRoom 
        {  
            get { return _hasRoom.PreviousRoom; } 
        }

        public IEvent OnRoomChanged 
        {  
            get { return _hasRoom.OnRoomChanged; } 
        }

        public Task ChangeRoomAsync(IRoom room, Nullable<Single> x, Nullable<Single> y)
        {
            return _hasRoom.ChangeRoomAsync(room, x, y);
        }

        #endregion

        #region IAnimationContainer implementation

        public IAnimation Animation 
        {  
            get { return _animationContainer.Animation; } 
        }

        public Boolean DebugDrawAnchor 
        {  
            get { return _animationContainer.DebugDrawAnchor; }  
            set { _animationContainer.DebugDrawAnchor = value; } 
        }

        public IEvent OnAnimationStarted 
        {  
            get { return _animationContainer.OnAnimationStarted; } 
        }

        public IBorderStyle Border 
        {  
            get { return _animationContainer.Border; }  
            set { _animationContainer.Border = value; } 
        }

        public void StartAnimation(IAnimation animation)
        {
            _animationContainer.StartAnimation(animation);
        }

        public AnimationCompletedEventArgs Animate(IAnimation animation)
        {
            return _animationContainer.Animate(animation);
        }

        public Task<AnimationCompletedEventArgs> AnimateAsync(IAnimation animation)
        {
            return _animationContainer.AnimateAsync(animation);
        }

        #endregion

        #region IInObjectTree implementation

        #endregion

        #region IInTree<IObject> implementation

        public ITreeNode<IObject> TreeNode 
        {  
            get { return _inObjectTree.TreeNode; } 
        }

        #endregion

        #region ICollider implementation

        public Nullable<PointF> CenterPoint 
        {  
            get { return _collider.CenterPoint; } 
        }

		public Boolean CollidesWith(Single x, Single y, IViewport viewport)
		{
			return _collider.CollidesWith(x, y, viewport);
		}

        #endregion

        #region IVisibleComponent implementation

        public Boolean Visible 
        {  
            get { return _visibleComponent.Visible; }  
            set { _visibleComponent.Visible = value; } 
        }

        public Boolean UnderlyingVisible 
        {  
            get { return _visibleComponent.UnderlyingVisible; } 
        }

        #endregion

        #region IEnabledComponent implementation

        public Boolean Enabled 
        {  
            get { return _enabledComponent.Enabled; }  
            set { _enabledComponent.Enabled = value; } 
        }

		public Boolean ClickThrough
		{
			get { return _enabledComponent.ClickThrough; }
			set { _enabledComponent.ClickThrough = value; }
		}

        public Boolean UnderlyingEnabled 
        {  
            get { return _enabledComponent.UnderlyingEnabled; } 
        }

		#endregion

		#region ICustomPropertiesComponent implementation

		public ICustomProperties Properties 
        {  
            get { return _customPropertiesComponent.Properties; } 
        }

        #endregion

        #region IDrawableInfo implementation

        public IRenderLayer RenderLayer 
        {  
            get { return _drawableInfo.RenderLayer; }  
            set { _drawableInfo.RenderLayer = value; } 
        }

        public Boolean IgnoreViewport 
        {  
            get { return _drawableInfo.IgnoreViewport; }  
            set { _drawableInfo.IgnoreViewport = value; } 
        }

        public Boolean IgnoreScalingArea 
        {  
            get { return _drawableInfo.IgnoreScalingArea; }  
            set { _drawableInfo.IgnoreScalingArea = value; } 
        }

        #endregion

        #region IHotspotComponent implementation

        public IInteractions Interactions 
        {  
            get { return _hotspotComponent.Interactions; } 
        }

        public Nullable<PointF> WalkPoint 
        {  
            get { return _hotspotComponent.WalkPoint; }  
            set { _hotspotComponent.WalkPoint = value; } 
        }

        public String Hotspot 
        {  
            get { return _hotspotComponent.Hotspot; }  
            set { _hotspotComponent.Hotspot = value; } 
        }

        #endregion

        #region IShaderComponent implementation

        public IShader Shader 
        {  
            get { return _shaderComponent.Shader; }  
            set { _shaderComponent.Shader = value; } 
        }

        #endregion

        #region ITranslateComponent implementation

        #endregion

        #region ITranslate implementation

        public ILocation Location 
        {  
            get { return _translateComponent.Location; }  
            set { _translateComponent.Location = value; } 
        }

        public Single X 
        {  
            get { return _translateComponent.X; }  
            set { _translateComponent.X = value; } 
        }

        public Single Y 
        {  
            get { return _translateComponent.Y; }  
            set { _translateComponent.Y = value; } 
        }

        public Single Z 
        {  
            get { return _translateComponent.Z; }  
            set { _translateComponent.Z = value; } 
        }

        #endregion

        #region IImageComponent implementation

        #endregion

        #region IHasImage implementation

        public Byte Opacity 
        {  
            get { return _imageComponent.Opacity; }  
            set { _imageComponent.Opacity = value; } 
        }

        public Color Tint 
        {  
            get { return _imageComponent.Tint; }  
            set { _imageComponent.Tint = value; } 
        }

        public PointF Anchor 
        {  
            get { return _imageComponent.Anchor; }  
            set { _imageComponent.Anchor = value; } 
        }

        public IImage Image 
        {  
            get { return _imageComponent.Image; }  
            set { _imageComponent.Image = value; } 
        }

        public IImageRenderer CustomRenderer 
        {  
            get { return _imageComponent.CustomRenderer; }  
            set { _imageComponent.CustomRenderer = value; } 
        }

        #endregion

        #region IScaleComponent implementation

        #endregion

        #region IScale implementation

        public Single Height 
        {  
            get { return _scaleComponent.Height; } 
        }

        public Single Width 
        {  
            get { return _scaleComponent.Width; } 
        }

        public Single ScaleX
        {
            get { return _scaleComponent.ScaleX; }
            set { _scaleComponent.ScaleX = value; }
        }

        public Single ScaleY
        {
            get { return _scaleComponent.ScaleY; }
            set { _scaleComponent.ScaleY = value; }
        }

        public PointF Scale
        {
            get { return _scaleComponent.Scale; }
            set { _scaleComponent.Scale = value; }
        }

        public SizeF BaseSize
        {
            get { return _scaleComponent.BaseSize; }
            set { _scaleComponent.BaseSize = value; }
        }

        public void ResetScale()
        {
            _scaleComponent.ResetScale();
        }

        public void ResetScale(Single initialWidth, Single initialHeight)
        {
            _scaleComponent.ResetScale(initialWidth, initialHeight);
        }

        public void ScaleTo(Single width, Single height)
        {
            _scaleComponent.ScaleTo(width, height);
        }

        public void FlipHorizontally()
        {
            _scaleComponent.FlipHorizontally();
        }

        public void FlipVertically()
        {
            _scaleComponent.FlipVertically();
        }

        #endregion

        #region IRotateComponent implementation

        #endregion

        #region IRotate implementation

        public Single Angle 
        {  
            get { return _rotateComponent.Angle; }  
            set { _rotateComponent.Angle = value; } 
        }

        #endregion

        #region IPixelPerfectComponent implementation

        #endregion

        #region IPixelPerfectCollidable implementation

        public IArea PixelPerfectHitTestArea 
        {  
            get { return _pixelPerfectComponent.PixelPerfectHitTestArea; } 
        }

        public void PixelPerfect(Boolean pixelPerfect)
        {
            _pixelPerfectComponent.PixelPerfect(pixelPerfect);
        }

        #endregion

        #region IModelMatrixComponent implementation

        public IEvent OnMatrixChanged 
        {  
            get { return _modelMatrixComponent.OnMatrixChanged; } 
        }

        public ModelMatrices GetModelMatrices()
        {
            return _modelMatrixComponent.GetModelMatrices();
        }

        public ILockStep ModelMatrixLockStep
        {
            get { return _modelMatrixComponent.ModelMatrixLockStep; }
        }

        #endregion

        #region IBoundingBoxComponent implementation

        public IEvent OnBoundingBoxesChanged
        {
            get { return _boundingBoxComponent.OnBoundingBoxesChanged; }
        }

        public AGSBoundingBoxes GetBoundingBoxes(IViewport viewport)
        {
            return _boundingBoxComponent.GetBoundingBoxes(viewport);
        }

        public ILockStep BoundingBoxLockStep
        {
            get { return _boundingBoxComponent.BoundingBoxLockStep; }
        }

        #endregion

        #region ITextComponent implementation

        public ITextConfig TextConfig 
        {  
            get { return _textComponent.TextConfig; }  
            set { _textComponent.TextConfig = value; } 
        }

        public String Text 
        {  
            get { return _textComponent.Text; }  
            set { _textComponent.Text = value; } 
        }

        public SizeF LabelRenderSize 
        {  
            get { return _textComponent.LabelRenderSize; }  
            set { _textComponent.LabelRenderSize = value; } 
        }

        public Boolean TextVisible 
        {  
            get { return _textComponent.TextVisible; }  
            set { _textComponent.TextVisible = value; } 
        }

        public Boolean TextBackgroundVisible
        {
            get { return _textComponent.TextBackgroundVisible; }
            set { _textComponent.TextBackgroundVisible = value; }
        }

        public Single TextHeight 
        {  
            get { return _textComponent.TextHeight; } 
        }

        public Single TextWidth 
        {  
            get { return _textComponent.TextWidth; } 
        }

        #endregion

        #region ITextBoxComponent implementation

        public IEvent<TextBoxKeyPressingEventArgs> OnPressingKey 
        {  
            get { return _textBoxComponent.OnPressingKey; } 
        }

        public IEvent OnFocusChanged 
        {  
            get { return _textBoxComponent.OnFocusChanged; } 
        }

        public Boolean IsFocused 
        {  
            get { return _textBoxComponent.IsFocused; }  
            set { _textBoxComponent.IsFocused = value; } 
        }

        public Int32 CaretPosition 
        {  
            get { return _textBoxComponent.CaretPosition; }  
            set { _textBoxComponent.CaretPosition = value; } 
        }

        public UInt32 CaretFlashDelay 
        {  
            get { return _textBoxComponent.CaretFlashDelay; }  
            set { _textBoxComponent.CaretFlashDelay = value; } 
        }

        #endregion
    }
}

