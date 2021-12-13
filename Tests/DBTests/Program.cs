// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;

Console.WriteLine("Hello, World!");
using (MySqlConnection cnn = new("server=localhost;database=shelter;uid=root;pwd=zxc123;"))
{
	cnn.Open();
	MySqlCommand cmd = new("SELECT * FROM Genus", cnn);
	using (var r = cmd.ExecuteReader())
	{
		while (r.Read())
		{
			Console.WriteLine();
		}
	}
	
}

