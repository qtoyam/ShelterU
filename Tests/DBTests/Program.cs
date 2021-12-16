// See https://aka.ms/new-console-template for more information
using System.Text;

using DBTests;

using(var t = new ShelterContext())
{
	foreach(var b in t.Breeds)
	{
		Console.WriteLine(b);
		var g = b.Gender;
	}
}

