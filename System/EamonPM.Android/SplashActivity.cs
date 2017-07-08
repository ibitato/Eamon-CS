﻿
// SplashActivity.cs

// Copyright (c) 2014-2017 by Michael R. Penner.  All rights reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Eamon;
using Eamon.Framework.Plugin;
using Eamon.Framework.Portability;
using Eamon.Mobile;
using static Eamon.Game.Plugin.PluginContext;
using static Eamon.Game.Plugin.PluginContextStack;

namespace EamonPM
{
	[Activity(Theme = "@style/MyTheme.Splash", ScreenOrientation = ScreenOrientation.Locked, NoHistory = true, MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
			/*
				- leave the BuildGuid alone to upgrade only the binary .apk file

				- change the BuildGuid to upgrade the binary .apk file and the .XML datafiles in the filesystem (but not CHARACTERS.XML)
			*/

			static readonly string BuildGuid = "52E7EA4D-7375-4624-992C-242B5FEB2F39";

			static readonly string TAG = "X:" + typeof (SplashActivity).Name;

		protected virtual void MirrorAssets()
		{
			var path = Path.Combine(App.BasePath, Path.Combine("System", "Bin"));

			Directory.CreateDirectory(path);

			var copyFiles = true;

			var guidFile = Path.Combine(path, "BUILDGUID.TXT");

			if (File.Exists(guidFile))
			{
				var savedGuid = File.ReadAllText(guidFile);

				if (string.Equals(BuildGuid, savedGuid, StringComparison.OrdinalIgnoreCase))
				{
					copyFiles = false;
				}
			}

			File.WriteAllText(guidFile, BuildGuid);

			var binFiles = Assets.List(Path.Combine("System", "Bin"));

			foreach (var file in binFiles)
			{
				var fileName = Path.Combine(path, file);

				if (!File.Exists(fileName) || (copyFiles && !fileName.EndsWith("CHARACTERS.XML", StringComparison.OrdinalIgnoreCase)))
				{
					fileName = Path.Combine(Path.Combine("System", "Bin"), file);

					using (var streamReader = new StreamReader(Assets.Open(fileName)))
					{
						var fileBytes = default(byte[]);

						using (var memoryStream = new MemoryStream())
						{
							streamReader.BaseStream.CopyTo(memoryStream);

							fileBytes = memoryStream.ToArray();

							fileName = Path.Combine(path, file);

							File.WriteAllBytes(fileName, fileBytes);
						}
					}
				}
			}

			path = Path.Combine(App.BasePath, "Documentation");

			Directory.CreateDirectory(path);

			var docFiles = Assets.List("Documentation");

			foreach (var file in docFiles)
			{
				var fileName = Path.Combine(path, file);

				if (!File.Exists(fileName) || copyFiles)
				{
					fileName = Path.Combine("Documentation", file);

					using (var streamReader = new StreamReader(Assets.Open(fileName)))
					{
						var fileBytes = default(byte[]);

						using (var memoryStream = new MemoryStream())
						{
							streamReader.BaseStream.CopyTo(memoryStream);

							fileBytes = memoryStream.ToArray();

							fileName = Path.Combine(path, file);

							File.WriteAllBytes(fileName, fileBytes);
						}
					}
				}
			}

			var adventureDirs = Assets.List("Adventures");

			foreach (var dir in adventureDirs)
			{
				path = Path.Combine(App.BasePath, Path.Combine("Adventures", dir));

				Directory.CreateDirectory(path);

				var adventureFiles = Assets.List(Path.Combine("Adventures", dir));

				foreach (var file in adventureFiles)
				{
					var fileName = Path.Combine(path, file);

					if (!File.Exists(fileName) || copyFiles)
					{
						fileName = Path.Combine(Path.Combine("Adventures", dir), file);

						using (var streamReader = new StreamReader(Assets.Open(fileName)))
						{
							var fileBytes = default(byte[]);

							using (var memoryStream = new MemoryStream())
							{
								streamReader.BaseStream.CopyTo(memoryStream);

								fileBytes = memoryStream.ToArray();

								fileName = Path.Combine(path, file);

								File.WriteAllBytes(fileName, fileBytes);
							}
						}
					}
				}
			}
		}

		// Launches the startup task
		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
      {
         base.OnCreate(savedInstanceState, persistentState);
			Log.Debug(TAG, "SplashActivity.OnCreate");
       }

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();

			App.Rc = RetCode.Success;

			App.BasePath = Application.Context.FilesDir.Path;

			MirrorAssets();

			StartActivity(new Intent(this, typeof(MainActivity)));         // StartActivity(new Intent(Application.Context, typeof(MainActivity)));
		}

		// Prevent the back button from canceling the startup process
		public override void OnBackPressed() { }
   }
}