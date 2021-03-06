﻿
// HintsCommand.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Eamon;
using Eamon.Framework;
using Eamon.Framework.Primitive.Enums;
using Eamon.Game.Attributes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;
using static EamonRT.Game.Plugin.PluginContext;

namespace EamonRT.Game.Commands
{
	[ClassMappings]
	public class HintsCommand : Command, IHintsCommand
	{
		public virtual void PrintHintsQuestion(IList<IHint> hints, int i)
		{
			Debug.Assert(hints != null);

			gOut.Write("{0}{1,3}. {2}", Environment.NewLine, i + 1, hints[i].Question);
		}

		public virtual void PrintHintsQuestion01(IList<IHint> hints, int i)
		{
			Debug.Assert(hints != null);

			gOut.Print("{0}", hints[i].Question);
		}

		public virtual void PrintHintsAnswer(IList<IHint> hints, int i, int j)
		{
			Debug.Assert(hints != null);

			gEngine.PrintMacroReplacedPagedString(hints[i].GetAnswers(j), Globals.Buf);
		}

		public override void PlayerExecute()
		{
			RetCode rc;
			int i, j;

			if (Globals.Database.GetHintsCount() > 0)
			{
				var hints = Globals.Database.HintTable.Records.Where(h => h.Active).OrderBy(h => h.Uid).ToList();

				if (hints.Count > 0)
				{
					gOut.Print("Your question?");

					for (i = 0; i < hints.Count; i++)
					{
						PrintHintsQuestion(hints, i);
					}

					gOut.Write("{0}{0}Enter your choice: ", Environment.NewLine);

					Globals.Buf.Clear();

					rc = Globals.In.ReadField(Globals.Buf, Constants.BufSize01, null, ' ', '\0', false, null, gEngine.ModifyCharToUpper, gEngine.IsCharDigit, null);

					Debug.Assert(gEngine.IsSuccess(rc));

					i = Convert.ToInt32(Globals.Buf.Trim().ToString());

					i--;

					if (i >= 0 && i < hints.Count)
					{
						PrintHintsQuestion01(hints, i);

						for (j = 0; j < hints[i].NumAnswers; j++)
						{
							PrintHintsAnswer(hints, i, j);

							if (j + 1 < hints[i].NumAnswers)
							{
								gOut.Write("{0}Another (Y/N): ", Environment.NewLine);

								Globals.Buf.Clear();

								rc = Globals.In.ReadField(Globals.Buf, Constants.BufSize02, null, ' ', '\0', false, null, gEngine.ModifyCharToUpper, gEngine.IsCharYOrN, null);

								Debug.Assert(gEngine.IsSuccess(rc));

								if (Globals.Buf.Length == 0 || Globals.Buf[0] == 'N')
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					gOut.Print("There are no hints available at this point in the adventure.");
				}
			}
			else
			{
				gOut.Print("There are no hints available for this adventure.");
			}

			if (NextState == null)
			{
				NextState = Globals.CreateInstance<IStartState>();
			}
		}

		public override bool ShouldPreTurnProcess()
		{
			return false;
		}

		public HintsCommand()
		{
			SortOrder = 390;

			IsDarkEnabled = true;

			IsMonsterEnabled = false;

			Name = "HintsCommand";

			Verb = "hints";

			Type = CommandType.Miscellaneous;
		}
	}
}
