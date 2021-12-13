using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maintance.Models;

using MySql.Data.MySqlClient;

using Serilog;
using Serilog.Core;

namespace Maintance.Services
{
	public sealed class DBService : IDisposable
	{
		private readonly MySqlConnection _conn;

		public DBService()
		{
			_conn = new(@"server=localhost;database=shelter;uid=root;pwd=zxc123;");
			_conn.Open();

			_addGenus.Connection = _conn;
			_getGenuses.Connection = _conn;
			_addBreed.Connection = _conn;
			_getBreeds.Connection = _conn;
			_getAnimals.Connection = _conn;

			Log.Debug("DB connected");
		}

		private readonly MySqlCommand _addGenus = new("INSERT INTO genus(name) VALUES (@name);");
		public void AddGenus(Genus v)
		{
			_addGenus.Parameters.AddWithValue("@name", v.Name);
			_addGenus.ExecuteNonQuery();
		}

		private readonly MySqlCommand _addBreed = 
			new("INSERT INTO breed (name, genus_ID, gender, description) VALUES(@name, @genus_ID, @gender, @descr)");
		public void AddBreed(Breed v)
		{
			_addBreed.Parameters.AddWithValue("@name", v.Name);
			_addBreed.Parameters.AddWithValue("@genus_ID", v.GenusId);
			_addBreed.Parameters.AddWithValue("@gender", 1);
			_addBreed.Parameters.AddWithValue("@descr", v.Description);
			_addBreed.ExecuteNonQuery();
		}

		private readonly MySqlCommand _getBreeds =
			new("SELECT * FROM Breed;");
		public IEnumerable<Breed> GetBreeds()
		{
			List<Breed> res = new();
			using (var r = _getBreeds.ExecuteReader())
			{
				while (r.Read())
				{
					res.Add(new()
					{
						ID = r.GetInt32(0),
						Name = r.GetString(1),
						GenusId = r.GetInt32(2),
						Sex = r.GetString(3),
						Description = r.GetString(4)
					});
				}
			}
			return res;
		}

		private readonly MySqlCommand _getGenuses = new("SELECT * FROM Genus;");
		public IEnumerable<Genus> GetGenuses()
		{
			List<Genus> res = new List<Genus>();
			using (var r = _getGenuses.ExecuteReader())
			{
				while (r.Read())
				{
					res.Add(new()
					{
						ID = r.GetInt32(0),
						Name = r.GetString(1)
					});
				}
			}
			return res;
		}

		private readonly MySqlCommand _getAnimals = new("SELECT * FROM Animal;");
		public IEnumerable<Animal> GetAnimals()
		{
			List<Animal> res = new();
			using(var r = _getAnimals.ExecuteReader())
			{
				while (r.Read())
				{
					var anim = new Animal();
					anim.ID = r.GetInt32(0);
					anim.Name = r.GetString(1);
					anim.Age = r.GetSByte(2);
					anim.BreedName = r.GetValue(3).ToString();
					var bd = r.GetValue(4);
					anim.Birthday = bd is DateTime dt ? dt : DateTime.MinValue;
					anim.Height = (int)r.GetDecimal(5);
					anim.Weight = (int)r.GetDecimal(6);
					anim.Arrival = r.GetDateTime(7);
					res.Add(anim);
				}
			}
			return res;
		}

		public void Dispose()
		{
			_conn.Close();
			Log.Debug("DB closed");
		}
	}
}
