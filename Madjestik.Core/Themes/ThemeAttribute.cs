using System;
using System.Linq;

namespace Madjestik.Core.Themes
{
	/// <summary>
	/// The theme attribute class
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ThemeAttribute : Attribute
	{
		#region Constructor

		/// <summary>
		/// Simple constructor
		/// </summary>
		/// <param name="name">The theme name</param>
		public ThemeAttribute(string name)
		{
			// The theme name
			this.Name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The name
		/// </summary>
		public virtual string Name { get; }

		#endregion

		#region Static

		/// <summary>
		/// The name of the theme
		/// </summary>
		/// <param name="instance">The instance</param>
		/// <returns>Returns the name of the converter</returns>
		public static string GetThemeName(object instance)
		{
			// Check from entry
			if (instance == null)
				throw new InvalidOperationException("Can not get theme name from a null instance type");

			// Default
			return ThemeAttribute.GetThemeName(instance.GetType());
		}

		/// <summary>
		/// The name of the theme
		/// </summary>
		/// <param name="type">The instance type</param>
		/// <returns>Returns the name of the converter</returns>
		public static string GetThemeName(Type type)
		{
			// Check from entry
			if (type == null)
				throw new InvalidOperationException("Can not get theme name from a null instance type");

			// Extract attributes
			var currentAttributes = type.GetCustomAttributes(typeof(ThemeAttribute), true)
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