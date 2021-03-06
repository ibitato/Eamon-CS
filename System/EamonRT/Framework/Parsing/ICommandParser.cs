﻿
// ICommandParser.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System.Text;
using Eamon.Framework;
using Eamon.Framework.Primitive.Classes;
using EamonRT.Framework.Commands;
using EamonRT.Framework.States;

namespace EamonRT.Framework.Parsing
{
	/// <summary></summary>
	public interface ICommandParser
	{
		/// <summary></summary>
		StringBuilder InputBuf { get; set; }

		/// <summary></summary>
		string LastInputStr { get; set; }

		/// <summary></summary>
		string[] Tokens { get; set; }

		/// <summary></summary>
		long CurrToken { get; set; }

		/// <summary></summary>
		long PrepTokenIndex { get; set; }

		/// <summary></summary>
		IPrep Prep { get; set; }

		/// <summary></summary>
		IMonster ActorMonster { get; set; }

		/// <summary></summary>
		IRoom ActorRoom { get; set; }

		/// <summary></summary>
		IParserData DobjData { get; set; }

		/// <summary></summary>
		IParserData IobjData { get; set; }

		/// <summary></summary>
		IParserData ObjData { get; set; }

		/// <summary></summary>
		IState NextState { get; set; }

		/// <summary></summary>
		ICommand NextCommand { get; }

		/// <summary></summary>
		/// <returns></returns>
		string GetActiveObjData();

		/// <summary></summary>
		/// <param name="artifact"></param>
		void SetArtifact(IArtifact artifact);

		/// <summary></summary>
		/// <param name="monster"></param>
		void SetMonster(IMonster monster);

		/// <summary></summary>
		/// <returns></returns>
		IArtifact GetArtifact();

		/// <summary></summary>
		/// <returns></returns>
		IMonster GetMonster();

		/// <summary></summary>
		/// <returns></returns>
		StringBuilder ReplacePrepositions(StringBuilder buf);

		/// <summary></summary>
		void Clear();

		/// <summary></summary>
		void ParseName();

		/// <summary></summary>
		/// <param name="command"></param>
		/// <param name="afterFinishParsing"></param>
		void CheckPlayerCommand(ICommand command, bool afterFinishParsing);

		/// <summary></summary>
		void Execute();
	}
}
