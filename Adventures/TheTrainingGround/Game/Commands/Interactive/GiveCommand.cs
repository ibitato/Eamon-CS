﻿
// GiveCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System.Diagnostics;
using Eamon.Framework;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static TheTrainingGround.Game.Plugin.PluginContext;

namespace TheTrainingGround.Game.Commands
{
	[ClassMappings]
	public class GiveCommand : EamonRT.Game.Commands.GiveCommand, IGiveCommand
	{
		public override void PrintGiveObjToActor(IArtifact artifact, IMonster monster)
		{
			Debug.Assert(artifact != null && monster != null);

			base.PrintGiveObjToActor(artifact, monster);

			// Give rapier to Jacques

			if (monster.Uid == 5 && artifact.Uid == 8 && !gGameState.JacquesRecoversRapier)
			{
				gEngine.PrintEffectDesc(22);

				gGameState.JacquesRecoversRapier = true;
			}
		}

		public override void PlayerProcessEvents(long eventType)
		{
			if (eventType == PpeAfterEnforceMonsterWeightLimitsCheck)
			{
				// Give obsidian scroll case to Emerald Warrior

				if (gIobjMonster.Uid == 14 && gDobjArtifact.Uid == 51)
				{
					gDobjArtifact.SetInLimbo();

					gIobjMonster.SetInLimbo();

					gEngine.PrintEffectDesc(14);

					GotoCleanup = true;
				}
				else
				{
					base.PlayerProcessEvents(eventType);
				}
			}
			else if (eventType == PpeBeforeMonsterTakesGold)
			{
				// Buy potion from gnome

				if (gIobjMonster.Uid == 20)
				{
					if (GoldAmount >= 100)
					{
						var redPotionArtifact = gADB[40];

						Debug.Assert(redPotionArtifact != null);

						var bluePotionArtifact = gADB[41];

						Debug.Assert(bluePotionArtifact != null);

						if (redPotionArtifact.IsCarriedByMonsterUid(20) || bluePotionArtifact.IsCarriedByMonsterUid(20))
						{
							gCharacter.HeldGold -= GoldAmount;

							if (GoldAmount > 100)
							{
								gEngine.PrintEffectDesc(30);
							}

							var potionArtifact = redPotionArtifact.IsCarriedByMonsterUid(20) ? redPotionArtifact : bluePotionArtifact;

							potionArtifact.SetInRoomUid(gGameState.Ro);

							gEngine.PrintEffectDesc(31);

							NextState = Globals.CreateInstance<IStartState>();
						}
						else
						{
							gEngine.PrintEffectDesc(29);
						}
					}
					else
					{
						gEngine.PrintEffectDesc(28);
					}

					GotoCleanup = true;
				}
				else
				{
					base.PlayerProcessEvents(eventType);
				}
			}
			else
			{
				base.PlayerProcessEvents(eventType);
			}
		}
	}
}
