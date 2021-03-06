﻿
// ReadCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System.Diagnostics;
using Eamon.Framework.Primitive.Enums;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static TheBeginnersCave.Game.Plugin.PluginContext;

namespace TheBeginnersCave.Game.Commands
{
	[ClassMappings]
	public class ReadCommand : EamonRT.Game.Commands.ReadCommand, IReadCommand
	{
		public override void PlayerProcessEvents(long eventType)
		{
			if (eventType == PpeBeforeArtifactReadTextPrint)
			{
				// saving throw vs. intellect for book trap warning

				if (gDobjArtifact.Uid == 9)
				{
					if (gGameState.BookWarning == 0)
					{
						var rl = gEngine.RollDice(1, 22, 2);

						if (rl <= gCharacter.GetStats(Stat.Intellect))
						{
							gEngine.PrintEffectDesc(14);

							gGameState.BookWarning = 1;

							GotoCleanup = true;
						}
					}
					else
					{
						gEngine.PrintEffectDesc(15);
					}
				}
				else
				{
					base.PlayerProcessEvents(eventType);
				}
			}
			else if (eventType == PpeAfterArtifactRead)
			{
				// book trap

				if (gDobjArtifact.Uid == 9)
				{
					gOut.Print(gActorRoom.Uid == 26 ? "You fall into the sea and are eaten by a big fish." : "You flop three times and die.");

					gGameState.Die = 1;

					NextState = Globals.CreateInstance<IPlayerDeadState>(x =>
					{
						x.PrintLineSep = true;
					});

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

		public override void PlayerExecute()
		{
			Debug.Assert(gDobjArtifact != null);

			// change name of bottle

			if (gDobjArtifact.Uid == 3)
			{
				gDobjArtifact.Name = "healing potion";

				gOut.Print("It says, \"HEALING POTION\".");

				NextState = Globals.CreateInstance<IMonsterStartState>();
			}
			else
			{
				base.PlayerExecute();
			}
		}
	}
}
