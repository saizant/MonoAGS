﻿using AGS.API;
using System.Collections.Generic;
using System;

namespace AGS.Engine
{
	public class GLImageRenderer : IImageRenderer
	{
        private readonly Dictionary<string, ITexture> _textures;
        private static Lazy<ITexture> _emptyTexture;
		private readonly IGLColorBuilder _colorBuilder;
		private readonly IGLTextureRenderer _renderer;
        private readonly IGraphicsFactory _graphicsFactory;
        private readonly IGLUtils _glUtils;
        private readonly IBitmapLoader _bitmapLoader;
        private readonly GLMatrices _matrices = new GLMatrices();
        private readonly AGSBoundingBox _emptySquare = default;
        private readonly Func<string, ITexture> _createTextureFunc;
        private readonly IHasImage[] _colorAdjusters;

        public GLImageRenderer (Dictionary<string, ITexture> textures, 
			IGLColorBuilder colorBuilder, IGLTextureRenderer renderer,
            IGraphicsFactory graphicsFactory, IGLUtils glUtils, 
            IBitmapLoader bitmapLoader)
		{
            _graphicsFactory = graphicsFactory;
            _createTextureFunc = createNewTexture; //Creating a delegate in advance to avoid memory allocations on critical path
			_textures = textures;
			_colorBuilder = colorBuilder;
			_renderer = renderer;
            _glUtils = glUtils;
            _bitmapLoader = bitmapLoader;
            _emptyTexture = new Lazy<ITexture>(() => initEmptyTexture());
            _colorAdjusters = new IHasImage[2];
		}

        public static ITexture EmptyTexture => _emptyTexture.Value;

        public SizeF? CustomImageSize => null;
        public PointF? CustomImageResolutionFactor => null;

        public void Prepare(IObject obj, IDrawableInfoComponent drawable, IViewport viewport)
		{
		}

        public void Render(IObject obj, IViewport viewport)
		{
            ISprite sprite = obj.Animation?.Sprite;
            if (sprite == null || sprite.Image == null)
            {
                return;
            }
            var boundingBoxes = obj.GetBoundingBoxes(viewport);
            if (boundingBoxes == null || !boundingBoxes.RenderBox.IsValid)
            {
                return;
            }

			ITexture texture = _textures.GetOrAdd (sprite.Image.ID, _createTextureFunc);

            _colorAdjusters[0] = sprite;
            _colorAdjusters[1] = obj;
			IGLColor color = _colorBuilder.Build(_colorAdjusters);

            IBorderStyle border = obj.Border;
            AGSBoundingBox borderBox = _emptySquare;
            if (!obj.Visible) return;
			if (border != null)
			{
                if (boundingBoxes.RenderBox.BottomLeft.X > boundingBoxes.RenderBox.BottomRight.X) borderBox = boundingBoxes.RenderBox.FlipHorizontal();
                else borderBox = boundingBoxes.RenderBox;

				border.RenderBorderBack(borderBox);
			}
            _renderer.Render(texture.ID, boundingBoxes, color);

			border?.RenderBorderFront(borderBox);
			if (obj.DebugDrawPivot)
			{
                IObject parent = obj.TreeNode.Parent;
                float x = obj.X;
                float y = obj.Y;
				while (parent != null)
				{
                    x += (parent.X - parent.Width * parent.Pivot.X);
                    y += (parent.Y - parent.Height * parent.Pivot.Y);
					parent = parent.TreeNode.Parent;
				}
                _glUtils.DrawCross(x - viewport.X, y - viewport.Y, 10, 10, 1f, 1f, 1f, 1f);
			}
		}
			
        private ITexture createNewTexture(string path)
		{
            if (string.IsNullOrEmpty(path)) return _emptyTexture.Value;
            return _graphicsFactory.LoadImage(path).Texture;
		}

        private ITexture initEmptyTexture()
        {
            var bitmap = _bitmapLoader.Load(1, 1);
            bitmap.SetPixel(Colors.White, 0, 0);
            return _graphicsFactory.LoadImage(bitmap, new AGSLoadImageConfig(config: new AGSTextureConfig(scaleUp: ScaleUpFilters.Nearest))).Texture;
        }
	}
}

