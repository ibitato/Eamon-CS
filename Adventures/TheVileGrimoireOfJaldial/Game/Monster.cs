
// Monster.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System.Diagnostics;
using Eamon.Framework;
using Eamon.Game.Attributes;
using static TheVileGrimoireOfJaldial.Game.Plugin.PluginContext;

namespace TheVileGrimoireOfJaldial.Game
{
	[ClassMappings(typeof(IMonster))]
	public class Monster : Eamon.Game.Monster, Framework.IMonster
	{
		public override bool CanMoveToRoom(bool fleeing)
		{
			// Parent griffin will never abandon the griffin cubs

			if (Uid == 40)
			{
				var smallGriffinMonster = Globals.MDB[41];

				Debug.Assert(smallGriffinMonster != null);

				return GetInRoomUid() != smallGriffinMonster.GetInRoomUid() ? base.CanMoveToRoom(fleeing) : false;
			}

			// Griffin cubs will only flee, never follow

			else if (Uid == 41)
			{
				return fleeing ? base.CanMoveToRoom(fleeing) : false;
			}
			else
			{
				// Flora monsters and water weird can't flee or follow

				return Uid != 18 && Uid != 19 && Uid != 20 && Uid != 22 && Uid != 38 ? base.CanMoveToRoom(fleeing) : false;
			}
		}

		public override string[] GetNaturalAttackDescs()      // Note: much more to implement here
		{
			var attackDescs = base.GetNaturalAttackDescs();

			if (Uid == 50)
			{
				attackDescs = new string[] { "touches" };
			}

			return attackDescs;
		}

		public override string GetArmorDescString()
		{
			var armorDesc = base.GetArmorDescString();

			if (IsInRoomLit())
			{
				if (Uid == 1 || Uid == 15)
				{
					armorDesc = "its course fur";
				}
				else if (Uid == 2)
				{
					armorDesc = "his leather armor";
				}
				else if (Uid == 3)
				{
					armorDesc = "its brittle bones";
				}
				else if (Uid == 4 || Uid == 12)
				{
					armorDesc = "its rotting flesh";
				}
				else if (Uid == 5 || Uid == 6 || Uid == 7 || Uid == 8)
				{
					armorDesc = "its tough hide";
				}
				else if (Uid == 9 || Uid == 14 || Uid == 16 || Uid == 21 || Uid == 38)
				{
					armorDesc = "its transparent form";
				}
				else if (Uid == 10)
				{
					armorDesc = "its glowing forcefield-like aura";
				}
				else if (Uid == 11 || Uid == 36 || Uid == 37 || Uid == 39)
				{
					armorDesc = "its chitinous carapace";
				}
				else if (Uid == 13 || Uid == 25)
				{
					armorDesc = "its jelly-like form";
				}
				else if (Uid == 19 || Uid == 20 || Uid == 22)
				{
					armorDesc = "its plant-skin armor";
				}
				else if (Uid == 23 || Uid == 24)
				{
					armorDesc = "its armor-like plating";
				}
				else if (Uid == 26)
				{
					armorDesc = "its greasy skin";
				}
				else if (Uid == 31)
				{
					armorDesc = "the weapon itself";
				}
				else if (Uid == 32)
				{
					armorDesc = "its jade-stone skin";
				}
				else if (Uid == 40 || Uid == 41 || Uid == 43)
				{
					armorDesc = "its armor-like hide";
				}
				else if (Uid == 44)
				{
					armorDesc = "its smooth fur";
				}
				else if (Uid == 50)
				{
					armorDesc = "its fiery skin";
				}
			}

			return armorDesc;
		}
	}
}
