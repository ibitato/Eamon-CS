﻿
// ReadCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System;
using System.Diagnostics;
using Eamon.Framework;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static TheSubAquanLaboratory.Game.Plugin.PluginContext;

namespace TheSubAquanLaboratory.Game.Commands
{
	[ClassMappings]
	public class ReadCommand : EamonRT.Game.Commands.ReadCommand, IReadCommand
	{
		public override void PrintCantVerbObj(IGameBase obj)
		{
			Debug.Assert(obj != null);

			Globals.Out.Print("You stare at {0}, but you don't see any secret messages forming.", obj.GetDecoratedName03(false, true, false, false, Globals.Buf));
		}

		public override void PlayerExecute()
		{
			var rl = 0L;

			Debug.Assert(DobjArtifact != null);

			var gameState = Globals.GameState as Framework.IGameState;

			Debug.Assert(gameState != null);

			switch (DobjArtifact.Uid)
			{
				case 9:

					// Bronze plaque

					if (!gameState.ReadPlaque)
					{
						gameState.QuestValue += 250;

						gameState.ReadPlaque = true;
					}

					base.PlayerExecute();

					break;

				case 48:

					// Display screen

					rl = Globals.Engine.RollDice(1, 100, 0);

					if (rl < 34)
					{
						var ls = new string[]
						{
							"",
							"SINCLAIR INLET     ",
							"VLADIVOSTOK     ",
							"BAJA PENINSULA     ",
							"YUKATAN PENINSULA     ",
							"GOLD COAST     ",
							"UPPER RHINE VALLEY     ",
							"LHASA     ",
							"VANCOUVER     ",
							"TRIPOLI     ",
							"HANOI     "
						};

						var d = new long[]
						{
							0L,
							Globals.Engine.RollDice(1, 9, 0),
							Globals.Engine.RollDice(1, 99, 0),
							Globals.Engine.RollDice(1, 30, 0),
							Globals.Engine.RollDice(1, 100, 0),
							Globals.Engine.RollDice(1, 2, 0),
							Globals.Engine.RollDice(1, 2, -1) + 3,
							Globals.Engine.RollDice(1, 90, 0),
							Globals.Engine.RollDice(1, 180, 0),
							Globals.Engine.RollDice(1, 59, 0),
							Globals.Engine.RollDice(1, 59, 0),
							Globals.Engine.RollDice(1, 59, 0),
							Globals.Engine.RollDice(1, 59, 0),
							Globals.Engine.RollDice(1, 10, 0),
							Globals.Engine.RollDice(1, 20, 0),
							Globals.Engine.RollDice(1, 100, 0)
						};

						var nsd = d[5] == 1 ? "North" : "South";

						var ewd = d[6] == 3 ? "East" : "West";

						Globals.Engine.PrintEffectDesc(51);

						Globals.Out.Print("{0}{1}.{2} GMT", ls[d[13]], d[14], d[15]);

						Globals.Out.Write("{0}Magnitude.....{1}.{2}", Environment.NewLine, d[1], d[2]);

						Globals.Out.Write("{0}Duration......{1}.{2} seconds", Environment.NewLine, d[3], d[4]);

						Globals.Out.Write("{0}Epicenter.....{1} Latitude {2} degrees {3} minutes {4} seconds", Environment.NewLine, nsd, d[7], d[9], d[11]);

						Globals.Out.Print("{0,14}{1} Longitude {2} degrees {3} minutes {4} seconds", "", ewd, d[8], d[10], d[12]);
						
						if (!gameState.ReadDisplayScreen)
						{
							gameState.QuestValue += 300;

							gameState.ReadDisplayScreen = true;
						}
					}
					else
					{
						Globals.Out.Print("The monitor screen remains blank.");
					}

					NextState = Globals.CreateInstance<IMonsterStartState>();

					break;

				case 50:

					// Terminals

					rl = Globals.Engine.RollDice(1, 100, 0);

					if (rl < 51)
					{
						Globals.Engine.PrintEffectDesc(52);

						if (!gameState.ReadTerminals)
						{
							gameState.QuestValue += 350;

							gameState.ReadTerminals = true;
						}
					}
					else
					{
						rl = Globals.Engine.RollDice(1, 100, 0);

						Globals.Out.Print("As you watch, the terminal screen prints:");

						Globals.Out.Print("  Error #{0}", rl);

						Globals.Out.Print("Uploading execution impossible - attempting to abort!");
					}

					NextState = Globals.CreateInstance<IMonsterStartState>();

					break;

				default:

					base.PlayerExecute();

					break;
			}
		}
	}
}
