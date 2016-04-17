﻿namespace AGS.API
{
    public interface IObjectFactory
	{
		IObject GetObject(string id, string[] sayWhenLook = null, string[] sayWhenInteract = null);

		ICharacter GetCharacter(string id, IOutfit outfit, string[] sayWhenLook = null, string[] sayWhenInteract = null);
		ICharacter GetCharacter(string id, IOutfit outfit, IAnimationContainer innerContainer);

		//If ID is null, the hotspot is used as the id
		IObject GetHotspot(string maskPath, string hotspot, string[] sayWhenLook = null, string[] sayWhenInteract = null, string id = null);
	}
}
