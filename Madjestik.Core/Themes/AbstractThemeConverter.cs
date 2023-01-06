using System;
using System.Linq;
using System.Reflection;

namespace Madjestik.Core.Themes.Console
{
	/// <summary>
	/// The theme converter for console
	/// </summary>
	public abstract partial class AbstractThemeConverter<T> : IThemeConverter<T>
	{
		#region Static

		/// <summary>
		/// The program arg log prefixes
		/// </summary>
		public static readonly string[] INIT_ARG_THEME_PREFIXES = new string[] { "/t", "/theme" };

		/// <summary>
		/// Returns the theme converter from arguments
		/// </summary>
		/// <param name="args">The arguments</param>
		/// <returns>Returns the theme converter from arguments</returns>
		public static IThemeConverter<T> GetThemeConverterFromArguments(string[] args)
		{
			// Check from args
			if (args == null || args.Length == 0)
				return null;

			// Browse all args
			foreach (var arg in args)
			{
				// Check from arg
				if (string.IsNullOrEmpty(arg))
					continue;

				// Set as lowered
				var compared = arg.ToLower();

				// Check for theme
				var themePrefixes = AbstractThemeConverter<T>.INIT_ARG_THEME_PREFIXES
					.Where(p => compared.StartsWith(p?.ToLower() + "="))
					.ToArray();
				foreach (var prefix in themePrefixes)
				{
					// Check states
					if (string.IsNullOrEmpty(prefix))
						continue;

					// Check for parse value
					var value = compared[(prefix.Length + 1)..].ToString();
					if (!string.IsNullOrEmpty(value))
					{
						// Try to find the converter
						var result = AbstractThemeConverter<T>.GetThemeConverterFromName(value);
						if (result == null)
							throw new InvalidOperationException($"Can not find the expected theme for: {value}");

						// Return the result
						return result;
					}
				}
			}

			// Default return null
			return null;
		}

		/// <summary>
		/// Returns the theme converter
		/// </summary>
		/// <param name="themeName">The converter theme name</param>
		/// <returns>Returns the theme converter</returns>
		public static IThemeConverter<T> GetThemeConverterFromName(string themeName)
		{
			// Manage null or empty case
			if (!string.IsNullOrEmpty(themeName))
			{
				// Searcg from assemblies
				var searchedConverterName = themeName.ToLower();
				var allAssemblies = AppDomain.CurrentDomain?.GetAssemblies() ?? new Assembly[0];
				if (allAssemblies != null && allAssemblies.Any())
				{
					// Search converters compliant to the argument
					var converterTypes = allAssemblies.SelectMany(a => a?.GetTypes()?.Select(t => t != null ? new
					{
						Type = t,
						ThemeName = ThemeAttribute.GetThemeName(t)
					} : null)
					.Where(t => t?.Type != null && !string.IsNullOrEmpty(t.ThemeName) && t.ThemeName?.ToLower() == themeName))
					.Select(t => t.Type)
					.ToArray();

					// Check results found
					if (converterTypes != null && converterTypes.Any())
					{
						// Check for too many results
						if (converterTypes.Length > 1)
							throw new InvalidOperationException("Can not find the theme converter: too many types with the same name: " + string.Join(", ", converterTypes.Select(c => c.FullName)));
					
						// Dynamic invoke
						try
						{
							// Activate from the converter result
							return (IThemeConverter<T>)Activator.CreateInstance(converterTypes.First());
						}
						catch (Exception ex)
						{
							// Throw wrapped exception
							throw new InvalidOperationException($"Can not dynamically create the theme converter instance for '{themeName}'", ex);
						}
					}
				}
			}

			// Default
			return null;
		}

		#endregion

		#region Implementation

		/// <summary>
		/// The theme value type
		/// </summary>
		public virtual Type ThemeValueType
		{
			get
			{
				// Return type of the generic type argument
				return typeof(T);
			}
		}

		/// <summary>
		/// Transform the given input into another
		/// </summary>
		/// <param name="value">The input value</param>
		/// <param name="contextObjects">The context objects</param>
		/// <returns>Returns the translated value</returns>
		public abstract T Transform(T value, params object[] contextObjects);

		#endregion

		#region Public

		/// <summary>
		/// Return the current converter name
		/// </summary>
		/// <returns>Returns the converter name</returns>
		public virtual string GetConverterName()
		{
			// Extract attributes
			var currentAttributes = this.GetType().GetCustomAttributes(typeof(ThemeAttribute), true)
				.ToArray();

			// Return if any
			if (currentAttributes.Any())
				return (currentAttributes.First() as ThemeAttribute).Name;

			// Default
			return null;
		}

		#endregion
	}
}