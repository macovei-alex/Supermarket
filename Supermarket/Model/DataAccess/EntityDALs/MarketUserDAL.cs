using Supermarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Utilities;
using Supermarket.ViewModel;
using System.Data;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class MarketUserDAL
	{
		public static List<MarketUser> GetAllUsers()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllUsers),
				Connection = connection,
			})
			{
				List<MarketUser> users = new List<MarketUser>();
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						users.Add(new MarketUser(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
					}
					return users;
				}
			}
		}

		public static MarketUser FindUser(string name, string passwordHash, bool isAdmin)
		{
			int roleID = isAdmin ? Cache.Instance.AdminID : Cache.Instance.CashierID;

			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(FindUser),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name),
					new SqlParameter("@PasswordHash", passwordHash),
					new SqlParameter("@UserType", roleID),
				}
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (!reader.HasRows)
					{
						return null;
					}
					reader.Read();
					return new MarketUser(reader.GetInt32(0), name, passwordHash, roleID);
				}
			}
		}

		public static int GetUserID(string name)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetUserID),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name)
				}
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (!reader.HasRows)
					{
						return -1;
					}
					reader.Read();
					return reader.GetInt32(0);
				}
			}
		}

		public static string CreateUser(string name, string passwordHash, int userType)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateUser),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name),
					new SqlParameter("@PasswordHash", passwordHash),
					new SqlParameter("@UserType", userType),
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteUser(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteUser),
				Connection = connection,
				Parameters = { new SqlParameter("@ID", id) }
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteUserByName(string name)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteUserByName),
				Connection = connection,
				Parameters = { new SqlParameter("@Name", name) }
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string EditUser(int id, string name, string oldPasswordHash, string newPasswordHash, int userType)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditUser),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id),
					new SqlParameter("@Name", name),
					new SqlParameter("@OldPasswordHash", oldPasswordHash),
					new SqlParameter("@NewPasswordHash", newPasswordHash),
					new SqlParameter("@UserType", userType),
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}
	}
}
