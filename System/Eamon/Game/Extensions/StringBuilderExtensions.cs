﻿
// StringBuilderExtensions.cs

// Copyright (c) 2014+ by Michael R. Penner.  All rights reserved.

using System;
using System.Text;

namespace Eamon.Game.Extensions
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder Replace(this StringBuilder buf, int startIndex, int length, string replacement)
		{
			if (buf != null && startIndex >= 0 && length > 0 && replacement != null)
			{
				buf.Remove(startIndex, length).Insert(startIndex, replacement);
			}

			return buf;
		}

		public static StringBuilder SetFormat(this StringBuilder buf, string format, params object[] args)
		{
			if (buf != null)
			{
				buf.Clear();

				buf.AppendFormat(format, args);
			}

			return buf;
		}

		public static StringBuilder SetPrint(this StringBuilder buf, string format, params object[] args)
		{
			if (buf != null && format != null)
			{
				buf.Clear();

				buf.AppendFormat(Environment.NewLine + format + Environment.NewLine, args);
			}

			return buf;
		}

		public static StringBuilder AppendPrint(this StringBuilder buf, string format, params object[] args)
		{
			if (buf != null && format != null)
			{
				buf.AppendFormat(Environment.NewLine + format + Environment.NewLine, args);
			}

			return buf;
		}

		public static StringBuilder TrimEndPunctuationMinusPound(this StringBuilder buf)
		{
			if (buf != null)
			{
				while (buf.Length > 0 && char.IsPunctuation(buf[buf.Length - 1]) && buf[buf.Length - 1] != '#')
				{
					buf.Length--;
				}
			}

			return buf;
		}
	}
}
