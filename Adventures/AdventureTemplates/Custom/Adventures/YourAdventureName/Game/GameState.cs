﻿
// GameState.cs

// Copyright (c) 2014+ by YourAuthorName.  All rights reserved

using Eamon.Framework;
using Eamon.Game.Attributes;

namespace YourAdventureName.Game
{
	[ClassMappings(typeof(IGameState))]
	public class GameState : Eamon.Game.GameState, Framework.IGameState
	{

	}
}