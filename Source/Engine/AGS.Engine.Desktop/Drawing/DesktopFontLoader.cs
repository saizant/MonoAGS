﻿using System;
using AGS.API;
using System.Drawing;

namespace AGS.Engine.Desktop
{
	public class DesktopFontLoader : IFontLoader
	{
		private DesktopFontFamilyLoader _familyLoader;

		public DesktopFontLoader(DesktopFontFamilyLoader familyLoader)
		{
			_familyLoader = familyLoader;
		}

		#region IFontLoader implementation

		public void InstallFonts(params string[] paths)
		{
			_familyLoader.InstallFonts(paths);
		}

		public IFont LoadFont(string fontFamily, float sizeInPoints, AGS.API.FontStyle style)
		{
			return new DesktopFont (new Font (fontFamily ?? SystemFonts.DefaultFont.FontFamily.Name, sizeInPoints, style.Convert()));
		}

		public IFont LoadFontFromPath(string path, float sizeInPoints, AGS.API.FontStyle style)
		{
			return new DesktopFont(new Font(_familyLoader.LoadFontFamily(path), sizeInPoints, style.Convert()));
		}

		#endregion
	}
}

