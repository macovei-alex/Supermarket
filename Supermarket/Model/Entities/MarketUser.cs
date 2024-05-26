using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Entities
{
	public class MarketUser
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public int UserTypeID { get; set; }

		public MarketUser(int id, string name, string passwordHash, int userTypeID)
		{
			ID = id;
			Name = name;
			PasswordHash = passwordHash;
			UserTypeID = userTypeID;
		}

		public override string ToString()
		{
			return $"{nameof(MarketUser)}{{ ID: {ID}, Name: {Name}, PasswordHash: {PasswordHash}, UserTypeID: {UserTypeID} }}";
		}
	}
}
