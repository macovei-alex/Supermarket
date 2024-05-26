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

		public static string CollectionToString(Collection<Input> inputCollection)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var input in inputCollection)
			{
				sb.Append(input.ToString()).Append("; ");
			}
			return sb.Remove(sb.Length - 2, 2).ToString();
		}
	}
}
