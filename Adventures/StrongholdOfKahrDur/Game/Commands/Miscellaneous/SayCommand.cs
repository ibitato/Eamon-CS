﻿
// SayCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System;
using System.Diagnostics;
using Eamon.Framework.Primitive.Enums;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static StrongholdOfKahrDur.Game.Plugin.PluginContext;

namespace StrongholdOfKahrDur.Game.Commands
{
	[ClassMappings]
	public class SayCommand : EamonRT.Game.Commands.SayCommand, ISayCommand
	{
		public override void PlayerProcessEvents(long eventType)
		{
			if (eventType == PpeAfterPlayerSay)
			{
				// Restore monster stats to average for testing/debugging

				if (string.Equals(ProcessedPhrase, "*brutis", StringComparison.OrdinalIgnoreCase))
				{
					var artUid = gActorMonster.Weapon;

					gActorMonster.Weapon = -1;

					gEngine.InitMonsterScaledHardinessValues();

					gActorMonster.Weapon = artUid;

					gOut.Print("Monster stats reduced.");

					NextState = Globals.CreateInstance<IStartState>();

					goto Cleanup;
				}

				var cauldronArtifact = gADB[24];

				Debug.Assert(cauldronArtifact != null);

				// If the cauldron is present and the spell components (see effect #50) are in it then begin the spell casting process

				if (string.Equals(ProcessedPhrase, "knock nikto mellon", StringComparison.OrdinalIgnoreCase) && (cauldronArtifact.IsCarriedByCharacter() || cauldronArtifact.IsInRoom(gActorRoom)) && gEngine.SpellReagentsInCauldron(cauldronArtifact))
				{
					gEngine.PrintEffectDesc(51);

					gGameState.UsedCauldron = true;
				}

				var lichMonster = gMDB[15];

				Debug.Assert(lichMonster != null);

				// Player will agree to free the Lich

				if (string.Equals(ProcessedPhrase, "i will free you", StringComparison.OrdinalIgnoreCase) && gActorRoom.Uid == 109 && lichMonster.IsInRoom(gActorRoom) && lichMonster.Friendliness > Friendliness.Enemy && gGameState.LichState < 2)
				{
					gEngine.PrintEffectDesc(54);

					gGameState.LichState = 1;
				}

				// Player actually frees the Lich

				if (string.Equals(ProcessedPhrase, "barada lhain", StringComparison.OrdinalIgnoreCase) && gActorRoom.Uid == 109 && lichMonster.IsInRoom(gActorRoom) && gGameState.LichState == 1)
				{
					var helmArtifact = gADB[25];

					Debug.Assert(helmArtifact != null);

					gEngine.PrintEffectDesc(55);

					// Set freed Lich flag and give Wizard's Helm (25) to player (carried but not worn)

					gGameState.LichState = 2;

					helmArtifact.SetInRoom(gActorRoom);
				}
			}

			base.PlayerProcessEvents(eventType);

		Cleanup:

			;
		}
	}
}
