﻿
// DeleteCustomAdventureClassesMenu.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eamon;
using Eamon.Game.Attributes;
using EamonDD.Framework.Menus.ActionMenus;
using static EamonDD.Game.Plugin.PluginContext;

namespace EamonDD.Game.Menus.ActionMenus
{
	[ClassMappings]
	public class DeleteCustomAdventureClassesMenu : AdventureSupportMenu01, IDeleteCustomAdventureClassesMenu
	{
		protected virtual void CheckForUnusedClasses()
		{
			RetCode rc;

			var generatedClasses = new string[] { "Artifact", "Effect", "Engine", "GameState", "Hint", "Module", "Monster", "Room" };

			SelectedClasses = new List<string>();

			foreach (var genClass in generatedClasses)
			{
				var origFileName = AdvTemplateDir + @"\Adventures\YourAdventureName\Game\" + genClass + ".cs";

				var gameFileName = Constants.AdventuresDir + @"\" + AdventureName + @"\Game\" + genClass + ".cs";

				var equalContent = Globals.File.Exists(origFileName) && Globals.File.Exists(gameFileName) && ReplaceMacros(Globals.File.ReadAllText(origFileName)).SequenceEqual(Globals.File.ReadAllText(gameFileName));

				if (equalContent)
				{
					Globals.Out.Print("{0}", Globals.LineSep);

					Globals.Out.Print("The {0} class appears to be empty.", AdventureName + ".Game." + genClass);

					Globals.Out.Write("{0}Would you like to delete it (Y/N): ", Environment.NewLine);

					Buf.Clear();

					rc = Globals.In.ReadField(Buf, Constants.BufSize02, null, ' ', '\0', false, null, Globals.Engine.ModifyCharToUpper, Globals.Engine.IsCharYOrN, null);

					Debug.Assert(Globals.Engine.IsSuccess(rc));

					if (Buf.Length > 0 && Buf[0] == 'Y')
					{
						SelectedClasses.Add(genClass);
					}
				}
			}

			if (SelectedClasses.Count == 0)
			{
				Globals.Out.Print("{0}", Globals.LineSep);

				Globals.Out.Print("The custom adventure library (.dll) has no unused generated classes.");

				GotoCleanup = true;
			}
		}

		protected virtual void DeleteSelectedClasses()
		{
			Globals.Out.Print("{0}", Globals.LineSep);

			Globals.Out.WriteLine();

			foreach (var selectedClass in SelectedClasses)
			{
				var fileName = Constants.AdventuresDir + @"\" + AdventureName + @"\Game\" + selectedClass + ".cs";

				if (Globals.File.Exists(fileName))
				{
					Globals.File.Delete(fileName);
				}

				fileName = Constants.AdventuresDir + @"\" + AdventureName + @"\Framework\I" + selectedClass + ".cs";

				if (Globals.File.Exists(fileName))
				{
					Globals.File.Delete(fileName);
				}
			}
		}

		public override void Execute()
		{
			Globals.Out.WriteLine();

			Globals.Engine.PrintTitle("DELETE CUSTOM ADVENTURE CLASSES", true);

			Debug.Assert(!Globals.Engine.IsAdventureFilesetLoaded());

			GotoCleanup = false;

			var workDir = Globals.Directory.GetCurrentDirectory();

			CheckForPrerequisites();

			if (GotoCleanup)
			{
				goto Cleanup;
			}

			GetAdventureName();

			if (GotoCleanup)
			{
				goto Cleanup;
			}

			GetAuthorName();

			GetAuthorInitials();

			CheckForUnusedClasses();

			if (GotoCleanup)
			{
				goto Cleanup;
			}

			QueryToProcessAdventure();

			if (GotoCleanup)
			{
				goto Cleanup;
			}

			DeleteSelectedClasses();

			UpdateXmlFileClasses();

			DeleteAdvBinaryFiles();

			RebuildSolution();

			if (GotoCleanup)
			{
				goto Cleanup;
			}

			PrintAdventureProcessed();

		Cleanup:

			if (GotoCleanup)
			{
				// TODO: rollback classes delete if possible
			}

			Globals.Directory.SetCurrentDirectory(workDir);
		}

		public DeleteCustomAdventureClassesMenu()
		{

		}
	}
}
