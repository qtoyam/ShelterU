// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;

Console.WriteLine("Hello, World!");
using (MySqlConnection cnn = new("server=localhost;database=testDB;uid=root;pwd=abc123;"))
{
	cnn.Open();
}

