﻿using System;

namespace Dtat
{
	public static class String
	{
		static String()
		{
		}

		public static string? Fix(string? text)
		{
			string? value = null;

			if (text == null)
			{
				return value;
			}

			text = text.Trim();

			if (text == string.Empty)
			{
				return value;
			}

			while (text.Contains("  "))
			{
				value = text.Replace("  ", " ");
			}

			return value;
		}

		public static string? RemoveSpaces(string? text)
		{
			string? value = null;

			if (text == null)
			{
				return value;
			}

			value = text.Replace(oldValue: " ", newValue: string.Empty);

			return value;
		}
	}
}
