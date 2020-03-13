﻿
// Engine.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eamon.Framework;
using Eamon.Framework.Primitive.Classes;
using Eamon.Framework.Primitive.Enums;
using Eamon.Game.Attributes;
using static TheVileGrimoireOfJaldial.Game.Plugin.PluginContext;

namespace TheVileGrimoireOfJaldial.Game
{
	[ClassMappings(typeof(IEngine))]
	public class Engine : EamonRT.Game.Engine, Framework.IEngine
	{
		protected override void PlayerSpellCastBrainOverload(Spell s, ISpell spell)
		{
			Debug.Assert(Enum.IsDefined(typeof(Spell), s));

			Debug.Assert(spell != null);

			gOut.Print("The strain of attempting to cast {0} overloads your brain and you forget it completely.", spell.Name);

			gGameState.SetSa(s, 0);

			gCharacter.SetSpellAbilities(s, 0);
		}

		public override void PrintMonsterAlive(IArtifact artifact)
		{
			Debug.Assert(artifact != null);

			// Cutlass

			if (artifact.Uid != 34)
			{
				base.PrintMonsterAlive(artifact);
			}
		}

		public override void PrintLightOut(IArtifact artifact)
		{
			Debug.Assert(artifact != null);

			if (artifact.Uid == 1)
			{
				gOut.Print("{0} suddenly flickers and then goes out.", artifact.GetTheName(true));
			}
			else
			{
				base.PrintLightOut(artifact);
			}
		}

		public override void InitArtifacts()
		{
			base.InitArtifacts();

			MacroFuncs.Add(1, () =>
			{
				var largeFountainArtifact = gADB[24];

				Debug.Assert(largeFountainArtifact != null);

				return largeFountainArtifact?.DoorGate != null ? "A small staircase leads down into darkness, and a passage leads back southward.  From below, many different noises can be discerned." : "A passage leads back southward.";
			});

			var synonyms = new Dictionary<long, string[]>()
			{
				{ 1, new string[] { "magi-torch", "instruction label", "instruction tag", "instructions", "instruction", "label", "tag" } },
				{ 3, new string[] { "small crypt door", "crypt door", "crypt", "door" } },
				{ 4, new string[] { "small tomb door", "tomb door", "tomb", "door" } },
				{ 5, new string[] { "crypt door", "door" } },
				{ 6, new string[] { "bucket" } },
				{ 8, new string[] { "stone coffin", "coffin" } },
				{ 9, new string[] { "leather bound book", "leather book", "bound book", "book" } },
				{ 10, new string[] { "gravestone", "stone" } },
				{ 11, new string[] { "dragon's treasure", "dragon's hoard", "treasure hoard", "treasure", "hoard" } },
				{ 12, new string[] { "goblet", "cup" } },
				{ 13, new string[] { "chest" } },
				{ 14, new string[] { "old map", "map" } },
				{ 15, new string[] { "bottle" } },
				{ 16, new string[] { "gauntlets", "gauntlet" } },
				{ 17, new string[] { "wine", "cask" } },
				{ 18, new string[] { "beholder's treasure", "beholder's hoard", "treasure hoard", "treasure", "hoard" } },
				{ 19, new string[] { "cloak" } },
				{ 20, new string[] { "pieces" } },
				{ 21, new string[] { "pouch with stones", "pouch", "stones" } },
				{ 22, new string[] { "egg" } },
				{ 23, new string[] { "nest" } },
				{ 24, new string[] { "fountain", "basin", "grotesque face", "face" } },
				{ 25, new string[] { "gold pile", "gold coins", "gold", "coins" } },
				{ 26, new string[] { "wood throne", "throne" } },
				{ 27, new string[] { "rune book", "book" } },
				{ 28, new string[] { "rod" } },
				{ 29, new string[] { "mace" } },
				{ 31, new string[] { "sword" } },
				{ 32, new string[] { "dagger" } },
				{ 33, new string[] { "parchment" } },
				{ 34, new string[] { "sword" } },
				{ 35, new string[] { "buried coffin", "casket", "coffin" } },
				{ 36, new string[] { "skeleton", "bones" } },
				{ 37, new string[] { "cross" } },
				{ 38, new string[] { "coil" } },
				{ 41, new string[] { "dead tree" } },
				{ 42, new string[] { "dead weed" } },
				{ 44, new string[] { "dead dragon" } },
				{ 45, new string[] { "dead crimson amoeba", "dead amoeba", "wine" } },
				{ 47, new string[] { "dead crayfish" } },
				{ 48, new string[] { "dead scorpion" } },
				{ 50, new string[] { "Jaldial the lich's remains", "Jaldi'al's remains", "Jaldial's remains", "lich's remains", "lich remains", "remains", "dead lich" } },
				{ 51, new string[] { "dead Reginald" } },
				{ 52, new string[] { "dead Dubro" } },
				{ 53, new string[] { "dead Joque" } },
				{ 54, new string[] { "dead Trevor" } },
			};

			foreach (var synonym in synonyms)
			{
				CreateArtifactSynonyms(synonym.Key, synonym.Value);
			}

			var torchArtifact = gADB[1];

			Debug.Assert(torchArtifact != null);

			gGameState.TorchRounds = RollDice(1, 81, 399);

			torchArtifact.Value = (long)Math.Round(5.0 * ((double)gGameState.TorchRounds / 50.0));

			torchArtifact.LightSource.Field1 = gGameState.TorchRounds;
		}

		public override void InitMonsters()
		{
			base.InitMonsters();

			// TODO: complete when monster names locked down

			var synonyms = new Dictionary<long, string[]>()
			{
				{ 1, new string[] { "rat" } },
				{ 10, new string[] { "will-o'-wisp", "will o' the wisp", "will o' wisp", "wisp" } },
				{ 11, new string[] { "crawler" } },
				{ 12, new string[] { "mound" } },
				{ 13, new string[] { "jelly" } },
				{ 20, new string[] { "bloodnettle" } },
				{ 21, new string[] { "hood" } },
				{ 22, new string[] { "weed" } },
				{ 23, new string[] { "animated armor", "suit of armor", "armor" } },
				{ 24, new string[] { "dragon" } },
				{ 25, new string[] { "amoeba" } },
				{ 31, new string[] { "possessed sword", "cutlass", "sword" } },
				{ 32, new string[] { "statues" } },
				{ 37, new string[] { "crayfish" } },
				{ 38, new string[] { "weird" } },
				{ 39, new string[] { "scorpion" } },
				{ 41, new string[] { "griffins" } },
				{ 43, new string[] { "jaldial", "lich" } },
				{ 44, new string[] { "jungle cats", "bekkahs", "cats" } },
				{ 50, new string[] { "genie" } },
			};

			foreach (var synonym in synonyms)
			{
				CreateMonsterSynonyms(synonym.Key, synonym.Value);
			}
		}

		public override void MonsterSmiles(IMonster monster)
		{
			Debug.Assert(monster != null);

			var rl = RollDice(1, 100, 0);

			// Giant rat

			if (monster.Uid == 1)
			{
				Globals.Out.Write("{0}{1} {2} at you.", Environment.NewLine, monster.GetTheName(true), rl > 80 ? "squeals" : rl > 50 ? "squeaks" : "hisses");
			}

			// Skeleton/Gargoyle

			else if ((monster.Uid == 3 || monster.Uid == 8) && rl > 50)
			{
				Globals.Out.Write("{0}{1} hisses at you.", Environment.NewLine, monster.GetTheName(true));
			}

			// Zombie

			else if (monster.Uid == 4 && rl > 50)
			{
				Globals.Out.Write("{0}{1} snarls at you.", Environment.NewLine, monster.GetTheName(true));
			}

			// Ghoul/Ghast

			else if (monster.Uid == 6 || monster.Uid == 7)
			{
				Globals.Out.Write("{0}{1} {2} at you.", Environment.NewLine, monster.GetTheName(true), rl > 80 ? "hisses" : rl > 50 ? "snarls" : "growls");
			}

			// Shadow/Specter/Wraith/Dark Hood/Animated suit of armor

			else if (monster.Uid == 9 || monster.Uid == 14 || monster.Uid == 16 || monster.Uid == 21 || monster.Uid == 23)
			{
				Globals.Out.Write("{0}{1} gestures at you.", Environment.NewLine, monster.GetTheName(true));
			}

			// Pocket dragon/Giant crayfish/Giant scorpion

			else if ((monster.Uid == 24 && monster.Friendliness != Friendliness.Neutral) || monster.Uid == 37 || monster.Uid == 39)
			{
				Globals.Out.Write("{0}{1} hisses at you.", Environment.NewLine, monster.GetTheName(true));
			}

			// Griffin/Small griffin

			else if (monster.Uid == 40 || (monster.Uid == 41 && monster.Friendliness != Friendliness.Neutral))
			{
				Globals.Out.Write("{0}{1} {2} at you.", Environment.NewLine, monster.GetTheName(true), rl > 80 ? monster.EvalPlural("screeches", "screech") : rl > 50 ? monster.EvalPlural("squawks", "squawk") : monster.EvalPlural("hisses", "hiss"));
			}

			// Jaldi'al the lich

			else if (monster.Uid == 43)
			{
				Globals.Out.Write("{0}{1} {2} at you.", Environment.NewLine, monster.GetTheName(true), rl > 80 ? "hollowly chuckles" : rl > 50 ? "gestures" : "glares");
			}

			// Jungle bekkah

			else if (monster.Uid == 44)
			{
				Globals.Out.Write("{0}{1} {2} at you.", Environment.NewLine, monster.GetTheName(true), rl > 80 ? monster.EvalPlural("roars", "roar") : rl > 50 ? monster.EvalPlural("snarls", "snarl") : monster.EvalPlural("hisses", "hiss"));
			}
			else
			{
				base.MonsterSmiles(monster);
			}
		}

		public override void MonsterDies(IMonster OfMonster, IMonster DfMonster)	// Note: much more to implement here
		{
			Debug.Assert(DfMonster != null);

			// Possessed cutlass

			if (DfMonster.Uid == 31)
			{
				var cutlassArtifact = gADB[34];

				Debug.Assert(cutlassArtifact != null);

				cutlassArtifact.Type = ArtifactType.Weapon;

				cutlassArtifact.Field1 = 25;

				cutlassArtifact.Field2 = 5;

				cutlassArtifact.Field3 = 2;

				cutlassArtifact.Field4 = 6;

				cutlassArtifact.Field5 = 1;
			}

			// Water weird

			else if (DfMonster.Uid == 38)
			{
				gGameState.WaterWeirdKilled = true;
			}

			// Efreeti

			else if (DfMonster.Uid == 50)
			{
				gGameState.EfreetiKilled = true;
			}

			base.MonsterDies(OfMonster, DfMonster);
		}

		public override IList<IMonster> GetSmilingMonsterList(IRoom room, IMonster monster)
		{
			var monsterUids = new long[] { 13, 18, 19, 20, 22, 25, 31, 32, 38 };

			// Some monsters don't emote

			return base.GetSmilingMonsterList(room, monster).Where(m => !monsterUids.Contains(m.Uid)).ToList();
		}

		public override void CheckToExtinguishLightSource()
		{
			// do nothing
		}

		public virtual bool SaveThrow(Stat stat, long bonus = 0)
		{
			var characterMonster = gMDB[gGameState.Cm];

			Debug.Assert(characterMonster != null);

			var value = 0L;

			switch (stat)
			{
				case Stat.Hardiness:

					// This is the saving throw vs. opening doors, etc

					value = characterMonster.Hardiness + bonus;

					break;

				case Stat.Agility:

					// This is the saving throw vs. avoiding traps, etc

					value = characterMonster.Agility + bonus;

					break;

				case Stat.Intellect:

					// This is the saving throw vs. searching, etc

					value = gCharacter.GetStats(Stat.Intellect) + bonus;

					break;

				default:

					// This is the saving throw vs. death or magic

					value = (long)Math.Round((double)(characterMonster.Agility + gCharacter.GetStats(Stat.Charisma) + characterMonster.Hardiness) / 3.0) + bonus;

					break;
			}

			var rl = RollDice(1, 22, 2);

			return rl <= value;
		}
	}
}
