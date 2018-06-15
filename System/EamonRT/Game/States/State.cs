﻿
// State.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved

using System;
using Eamon.Framework;
using Eamon.Framework.States;
using EamonRT.Framework.States;
using Enums = Eamon.Framework.Primitive.Enums;
using static EamonRT.Game.Plugin.PluginContext;

namespace EamonRT.Game.States
{
	public abstract class State : IState
	{
		#region Private Properties

		private IStateImpl StateImpl { get; set; }

		#endregion

		#region Public Properties

		#region Interface IState

		public virtual bool GotoCleanup
		{
			get
			{
				return StateImpl.GotoCleanup;
			}

			set
			{
				StateImpl.GotoCleanup = value;
			}
		}

		public virtual string Name
		{
			get
			{
				return StateImpl.Name;
			}

			set
			{
				StateImpl.Name = value;
			}
		}

		public virtual IState NextState
		{
			get
			{
				return StateImpl.NextState;
			}

			set
			{
				StateImpl.NextState = value;
			}
		}

		public virtual bool PreserveNextState
		{
			get
			{
				return StateImpl.PreserveNextState;
			}

			set
			{
				StateImpl.PreserveNextState = value;
			}
		}

		public virtual bool Discarded
		{
			get
			{
				return StateImpl.Discarded;
			}

			set
			{
				StateImpl.Discarded = value;
			}
		}

		#endregion

		#endregion

		#region Public Methods

		#region Interface IDisposable

		public virtual void Dispose(bool disposing)
		{
			StateImpl.Dispose(disposing);
		}

		public void Dispose()      // virtual intentionally omitted
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		#endregion

		#region Interface IState

		public virtual void PrintObjBlocksTheWay(IArtifact artifact)
		{
			StateImpl.PrintObjBlocksTheWay(artifact);
		}

		public virtual void PrintCantGoThatWay()
		{
			StateImpl.PrintCantGoThatWay();
		}

		public virtual void PrintCantVerbThere(string verb)
		{
			StateImpl.PrintCantVerbThere(verb);
		}

		public virtual void PrintRideOffIntoSunset()
		{
			StateImpl.PrintRideOffIntoSunset();
		}

		public virtual void PrintEnemiesNearby()
		{
			StateImpl.PrintEnemiesNearby();
		}

		public virtual void ProcessEvents(long eventType)
		{
			StateImpl.ProcessEvents(eventType);
		}

		public virtual string GetDarkName(IGameBase target, Enums.ArticleType articleType, string nameType, bool upshift, bool groupCountOne)
		{
			return StateImpl.GetDarkName(target, articleType, nameType, upshift, groupCountOne);
		}

		public virtual bool ShouldPreTurnProcess()
		{
			return StateImpl.ShouldPreTurnProcess();
		}

		public abstract void Execute();

		#endregion

		#region Class State

		public State()
		{
			StateImpl = Globals.CreateInstance<IStateImpl>(x =>
			{
				x.State = this;
			});
		}

		#endregion

		#endregion
	}
}
