using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FootballSchool.Kernel.Extensions
{
	/// <summary>
	/// ext fo UC
	/// </summary>
	public static class UserControlExtensions
	{
		/// <summary>
		/// Tries the find parent.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="child">The child.</param>
		/// <returns></returns>
		public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
		{
			DependencyObject parentObject = VisualTreeHelper.GetParent(child);
			if (parentObject == null) return null;
			T parent = parentObject as T;
			if (parent != null)
				return parent;
			return TryFindParent<T>(parentObject);
		}
	}
}