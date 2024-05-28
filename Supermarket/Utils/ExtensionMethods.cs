using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Supermarket.Utils
{
	public static class ExtensionMethods
	{
		public static void RepopulateFrom<T>(this ObservableCollection<T> destination, ObservableCollection<T> source)
		{
			if (destination == null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			destination.Clear();
			foreach (T item in source)
			{
				destination.Add(item);
			}
		}

		public static void RepopulateFrom<T>(this List<T> destination, ObservableCollection<T> source)
		{
			if (destination == null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			for (int i = 0; i < Math.Min(destination.Count, source.Count); i++)
			{
				destination[i] = source[i];
			}

			if (destination.Count > source.Count)
			{
				int diff = destination.Count - source.Count;
				for (int i = 0; i < diff; i++)
				{
					destination.Remove(destination.Last());
				}
			}
			else if (destination.Count < source.Count)
			{
				int diff = source.Count - destination.Count;
				int start = destination.Count;
				for (int i = 0; i < diff; i++)
				{
					destination.Add(source[start + i]);
				}
			}
		}

		public static void Print<T>(this List<T> list, string separator = "\n")
		{
			foreach (var item in list)
			{
				Console.Write(item.ToString() + separator);
			}
		}
	}
}
