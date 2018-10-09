﻿
// AddMonsterRecordManualMenu.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using Eamon.Framework;
using Eamon.Framework.Helpers;
using Eamon.Game.Attributes;
using EamonDD.Framework.Menus.ActionMenus;
using static EamonDD.Game.Plugin.PluginContext;

namespace EamonDD.Game.Menus.ActionMenus
{
	[ClassMappings]
	public class AddMonsterRecordManualMenu : AddRecordManualMenu<IMonster, IMonsterHelper>, IAddMonsterRecordManualMenu
	{
		public override void UpdateGlobals()
		{
			Globals.MonstersModified = true;

			if (Globals.Module != null)
			{
				Globals.Module.NumMonsters++;

				Globals.ModulesModified = true;
			}
		}

		public AddMonsterRecordManualMenu()
		{
			Title = "ADD MONSTER RECORD";

			RecordTable = Globals.Database.MonsterTable;

			RecordTypeName = "monster";
		}
	}
}
