﻿
// SayCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System;
using System.Diagnostics;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static BeginnersForest.Game.Plugin.PluginContext;

namespace BeginnersForest.Game.Commands
{
	[ClassMappings]
	public class SayCommand : EamonRT.Game.Commands.SayCommand, ISayCommand
	{
		protected override void PlayerProcessEvents01()
		{
			var gameState = Globals.GameState as Framework.IGameState;

			Debug.Assert(gameState != null);

			//          Spook Reducer 2.0
			//  (c) 2012 Frank Black Productions

			if (string.Equals(ProcessedPhrase, "less spooks", StringComparison.OrdinalIgnoreCase) && gameState.SpookCounter < 8)
			{
				var monster = Globals.MDB[9];

				Debug.Assert(monster != null);

				monster.GroupCount = monster.GroupCount > 0 ? 1 : 0;

				monster.InitGroupCount = monster.GroupCount;

				monster.OrigGroupCount = monster.GroupCount;

				Globals.Engine.CheckEnemies();

				gameState.SpookCounter = 8;

				Globals.Out.Print("Less spooks it is!");

				NextState = Globals.CreateInstance<IStartState>();

				goto Cleanup;
			}

			base.PlayerProcessEvents01();

		Cleanup:

			;
		}
	}
}
