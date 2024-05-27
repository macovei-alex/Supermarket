using System.Collections.ObjectModel;
using System.Text;

namespace Supermarket.ViewModel
{
	internal class Input
	{
		public string Label { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return $"{Label}: {Value}";
		}

		public static string CollectionToString(Collection<Input> inputCollection, bool printLabels = false)
		{
			StringBuilder sb = new StringBuilder();

			if (printLabels)
			{
				foreach (var input in inputCollection)
				{
					sb.Append(input.ToString()).Append("; ");
				}
			}

			else
			{
				foreach (var input in inputCollection)
				{
					sb.Append(input.Value).Append("; ");
				}
			}

			return sb.Remove(sb.Length - 2, 2).ToString();
		}
	}
}
