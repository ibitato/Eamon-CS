﻿
// QuickLaunchViewModel.cs

// Copyright (c) 2014-2017 by Michael R. Penner.  All rights reserved

using System.Collections.Generic;

namespace Eamon.Mobile.ViewModels
{
	public class QuickLaunchViewModel : BaseViewModel
	{
		public List<string> Folders { get; set; }

		public QuickLaunchViewModel()
		{
			Title = "QuickLaunch";

			Folders = new List<string>()
			{
				"EamonDD",
				"EamonMH",
				"EamonRT"
			};
		}

	}
}