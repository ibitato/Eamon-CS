﻿
// SayCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System;
using System.Diagnostics;
using Eamon.Game.Attributes;
using static TheTrainingGround.Game.Plugin.PluginContext;

namespace TheTrainingGround.Game.Commands
{
	[ClassMappings]
	public class SayCommand : EamonRT.Game.Commands.SayCommand, EamonRT.Framework.Commands.ISayCommand
	{
		protected override void PlayerProcessEvents01()
		{
			var hammerArtifact = Globals.ADB[24];

			Debug.Assert(hammerArtifact != null);

			var magicWordsSpoken = string.Equals(ProcessedPhrase, "thor", StringComparison.OrdinalIgnoreCase) || string.Equals(ProcessedPhrase, "thor's hammer", StringComparison.OrdinalIgnoreCase);

			var hammerPresent = hammerArtifact.IsCarriedByCharacter() || hammerArtifact.IsInRoom(ActorRoom);

			// Hammer of Thor

			if (magicWordsSpoken && hammerPresent)
			{
				var command = Globals.CreateInstance<EamonRT.Framework.Commands.IUseCommand>();

				CopyCommandData(command);

				command.Dobj = hammerArtifact;

				NextState = command;

				GotoCleanup = true;
			}
			else
			{
				base.PlayerProcessEvents01();
			}
		}
	}
}
