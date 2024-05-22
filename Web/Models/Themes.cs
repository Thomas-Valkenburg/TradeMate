﻿namespace Web.Models;

public static class Theme
{
	public enum Value
	{
		auto,
		light,
		dark
	}

	public static Value? GetActiveTheme(HttpContext context)
	{
		return Enum.TryParse(context.Session.GetString("data-bs-theme"), out Value theme) ? theme : default(Value?);
	}

	public static bool IsActive(Value theme, HttpContext context)
	{
		if(!Enum.TryParse(context.Session.GetString("data-bs-theme") ?? "not_set", out Value themeResult)) return false;
		return theme == themeResult;
	}

	public static void SetActive(Value theme, HttpContext context)
	{
		context.Session.SetString("data-bs-theme", theme.ToString());
	}

	public static string GetThemeIconPath(Value theme)
	{
		return theme switch
		{
			Value.auto  => "#theme-auto-icon",
			Value.light => "#theme-light-icon",
			Value.dark  => "#theme-dark-icon",
			_           => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
		};
	}
}