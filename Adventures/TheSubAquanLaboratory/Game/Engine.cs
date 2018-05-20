﻿
// Engine.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System.Collections.Generic;
using System.Diagnostics;
using Eamon.Framework;
using Eamon.Game.Attributes;
using static TheSubAquanLaboratory.Game.Plugin.PluginContext;

namespace TheSubAquanLaboratory.Game
{
	[ClassMappings(typeof(IEngine))]
	public class Engine : EamonRT.Game.Engine, EamonRT.Framework.IEngine
	{
		public override void AddPoundCharsToArtifactNames()
		{
			base.AddPoundCharsToArtifactNames();

			var cardSlotArtifact = Globals.ADB[26];     // Card slot #2

			Debug.Assert(cardSlotArtifact != null);

			cardSlotArtifact.Name = cardSlotArtifact.Name.TrimEnd('#');

			var consoleArtifact = Globals.ADB[62];         // Console #2

			Debug.Assert(consoleArtifact != null);

			consoleArtifact.Name = consoleArtifact.Name.TrimEnd('#');
		}

		public override void InitArtifacts()
		{
			base.InitArtifacts();

			var synonyms = new Dictionary<long, string[]>()
			{
				{ 1, new string[] { "slot" } },
				{ 3, new string[] { "button" } },
				{ 4, new string[] { "button" } },
				{ 5, new string[] { "desk" } },
				{ 6, new string[] { "plate" } },
				{ 7, new string[] { "phone-like device", "device" } },
				{ 9, new string[] { "plaque" } },
				{ 10, new string[] { "spigot" } },
				{ 11, new string[] { "cabinet" } },
				{ 13, new string[] { "utensil" } },
				{ 15, new string[] { "vat" } },
				{ 16, new string[] { "door" } },
				{ 17, new string[] { "pipe" } },
				{ 18, new string[] { "massive apparatus", "spherical apparatus", "dish washer", "washer" } },
				{ 19, new string[] { "button" } },
				{ 20, new string[] { "button" } },
				{ 21, new string[] { "button" } },
				{ 22, new string[] { "food" } },
				{ 23, new string[] { "magnetic power plant", "fusion power plant", "fusion plant", "power plant", "plant" } },
				{ 24, new string[] { "tank" } },
				{ 25, new string[] { "pool" } },
				{ 26, new string[] { "slot" } },
				{ 27, new string[] { "button" } },
				{ 30, new string[] { "burner" } },
				{ 31, new string[] { "dish" } },
				{ 32, new string[] { "pedestal" } },
				{ 33, new string[] { "balance" } },
				{ 36, new string[] { "spectrograph", "chromatograph" } },
				{ 41, new string[] { "table" } },
				{ 42, new string[] { "box sized device", "device" } },
				{ 43, new string[] { "tool" } },
				{ 44, new string[] { "surgeomat xxi", "type xxi", "surgeomat", "xxi" } },
				{ 46, new string[] { "button" } },
				{ 47, new string[] { "drum" } },
				{ 48, new string[] { "display", "screen" } },
				{ 49, new string[] { "cabinet" } },
				{ 50, new string[] { "computer", "terminal" } },
				{ 52, new string[] { "gear" } },
				{ 53, new string[] { "rack" } },
				{ 54, new string[] { "gun" } },
				{ 55, new string[] { "button" } },
				{ 56, new string[] { "button" } },
				{ 57, new string[] { "Seven" } },
				{ 58, new string[] { "panel" } },
				{ 59, new string[] { "button" } },
				{ 60, new string[] { "button" } },
				{ 61, new string[] { "crystal column", "column" } },
				{ 63, new string[] { "shield panel", "control panel", "panel" } },
				{ 64, new string[] { "installation panel", "defense panel", "panel" } },
				{ 65, new string[] { "dial" } },
				{ 66, new string[] { "elevation button", "increase button", "button" } },
				{ 67, new string[] { "elevation button", "decrease button", "button" } },
				{ 68, new string[] { "rotate button", "turret button", "button" } },
				{ 69, new string[] { "button" } },
				{ 70, new string[] { "button" } },
				{ 73, new string[] { "blaster", "pistol", "gun" } },
				{ 74, new string[] { "blaster", "pistol", "gun" } },
				{ 75, new string[] { "pistol", "gun" } },
				{ 76, new string[] { "scalpel" } },
				{ 77, new string[] { "drill" } },
				{ 78, new string[] { "disruptor", "pistol", "gun" } },
				{ 79, new string[] { "axe" } },
				{ 80, new string[] { "mace" } },
				{ 81, new string[] { "crossbow" } },
				{ 82, new string[] { "card" } },
				{ 83, new string[] { "fake looking back wall", "fake back wall", "fake wall", "back wall", "wall" } },
				{ 84, new string[] { "wall" } },
				{ 85, new string[] { "electric floor", "floor", "tile", "trap" } },
				{ 86, new string[] { "horl choo", "choo" } },
				{ 87, new string[] { "dismantled android", "first android", "android" } },
				{ 88, new string[] { "dismantled android", "second android", "android" } },
				{ 89, new string[] { "dismantled android", "worker android", "android" } },
				{ 90, new string[] { "dismantled android", "thinker android", "android" } },
				{ 91, new string[] { "destroyed android", "thinker android", "android" } },
				{ 92, new string[] { "dead hammerhead", "large hammerhead", "dead shark", "large shark", "hammerhead", "shark" } },
				{ 93, new string[] { "dismembered hammerhead", "small hammerhead", "dismembered shark", "small shark", "hammerhead", "shark" } },
				{ 94, new string[] { "dismantled first android", "dismantled silver android", "first silver android", "silver android", "android" } },
				{ 95, new string[] { "dismantled second android", "dismantled silver android", "second silver android", "silver android", "android" } },
				{ 96, new string[] { "dismantled android", "thinker", "android" } },
				{ 97, new string[] { "dismantled android", "worker", "android" } },
				{ 98, new string[] { "dead humanoid", "dead fish man", "dead fish-man", "fen" } },
				{ 99, new string[] { "dismantled technician", "lab technician", "dismantled android", "lab android", "technician", "android" } },
				{ 100, new string[] { "dismantled android", "warrior android", "android" } },
				{ 101, new string[] { "jules' body", "body" } },
				{ 102, new string[] { "jemmas' body", "body" } },
				{ 103, new string[] { "mutilated body", "body" } },
				{ 104, new string[] { "body" } },
				{ 105, new string[] { "shattered wall", "glass wall", "wall" } },
				{ 106, new string[] { "pile of rubble", "pile", "rubble" } },
				{ 107, new string[] { "shorted out floor", "shorted out trap", "floor trap", "floor", "trap" } },
			};

			foreach (var synonym in synonyms)
			{
				CreateArtifactSynonyms(synonym.Key, synonym.Value);
			}
		}

		public override void InitMonsters()
		{
			base.InitMonsters();

			var synonyms = new Dictionary<long, string[]>()
			{
				{ 1, new string[] { "choo" } },
				{ 2, new string[] { "first android", "worker android", "worker", "android" } },
				{ 3, new string[] { "second android", "worker android", "worker", "android" } },
				{ 4, new string[] { "worker", "android" } },
				{ 5, new string[] { "thinker", "android" } },
				{ 6, new string[] { "thinker", "android" } },
				{ 7, new string[] { "large hammerhead", "mutated hammerhead", "large shark", "mutated shark", "hammerhead", "shark" } },
				{ 8, new string[] { "small hammerhead", "mutated hammerhead", "small shark", "mutated shark", "hammerhead", "shark" } },
				{ 9, new string[] { "first figure", "silver-clad figure", "first android", "silver-clad android", "silver figure", "silver android", "figure", "android" } },
				{ 10, new string[] { "second figure", "silver-clad figure", "second android", "silver-clad android", "silver figure", "silver android", "figure", "android" } },
				{ 11, new string[] { "thinker", "android" } },
				{ 12, new string[] { "worker", "android" } },
				{ 13, new string[] { "humanoid", "fish man", "fish-man" } },
				{ 14, new string[] { "thinker", "android" } },
				{ 15, new string[] { "warrior", "android" } },
				{ 16, new string[] { "jules" } },
				{ 17, new string[] { "jemmas" } },
				{ 18, new string[] { "red eye", "eye" } },
				{ 19, new string[] { "archie", "panther" } },
				{ 20, new string[] { "worker", "android" } },
				{ 21, new string[] { "thinker", "android" } },
				{ 22, new string[] { "warrior", "android" } },
			};

			foreach (var synonym in synonyms)
			{
				CreateMonsterSynonyms(synonym.Key, synonym.Value);
			}
		}

		public Engine()
		{
			StartRoom = 16;
		}
	}
}
