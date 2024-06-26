﻿using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

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

		public static bool AreNotNullOrEmpty(params string[] strings)
		{
			if (strings == null || strings?.Length == 0)
			{
				return false;
			}

			foreach (string item in strings)
			{
				if (item == null || item == string.Empty)
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
