using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System.Windows;
using Supermarket.Utils;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace Supermarket.Utilities
{
	internal static class Functions
	{
		public static string HashString(string input)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(input);
				byte[] hash = sha256.ComputeHash(bytes);
				return BitConverter.ToString(hash).Replace("-", "").ToLower();
			}
		}

		public static bool AreNotNullOrEmpty(params string[] passwords)
		{
			if (passwords == null || passwords?.Length == 0)
			{
				return false;
			}

			foreach (string password in passwords)
			{
				if (password == null || password == string.Empty)
				{
					return false;
				}
			}

			return true;
		}

		public static void LogError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButton.OK);
		}

		public static void LogInfo(string message)
		{
			MessageBox.Show(message, "Info", MessageBoxButton.OK);
		}

		public static string SqlCallWrapper(Func<object> p)
		{
			try
			{
				return p() as string;
			}
			catch (SqlException e)
			{
				return e.Message;
			}
		}
	}
}
