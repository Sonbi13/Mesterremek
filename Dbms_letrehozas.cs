using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
	internal class Dbms_letrehozas
	{
		string connString = "server=localhost;user=root;password=;";
		string connStringWithDatabase = "server=localhost;user=root;password=;database=Tanfolyam;";
		MySqlConnection mysqlConn;
		MySqlConnection mysqlConnWithDatabase;

		public bool openConnection()
		{
			try
			{
				mysqlConn = new MySqlConnection(connString);
				mysqlConn.Open();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool openConnectionWithDatabase()
		{
			try
			{
				mysqlConnWithDatabase = new MySqlConnection(connStringWithDatabase);
				mysqlConnWithDatabase.Open();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool closeConnection()
		{
			try
			{
				mysqlConn = new MySqlConnection(connString);
				mysqlConn.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool closeConnectionWithDatabase()
		{
			try
			{
				mysqlConnWithDatabase = new MySqlConnection(connStringWithDatabase);
				mysqlConnWithDatabase.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool runQuery(string query)
		{
			closeConnectionWithDatabase();
			if(openConnectionWithDatabase())
			{
				try
				{
					MySqlCommand parancs = new MySqlCommand(query, mysqlConnWithDatabase);
					parancs.ExecuteNonQuery();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
				return false;
		}

		public bool createDatabase(string dbName)
		{
			closeConnection();
			if(openConnection())
			{
				try
				{
					string query = "CREATE DATABASE " + dbName + " DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci";
					MySqlCommand command = new MySqlCommand();
					command.Connection = mysqlConn;
					command.CommandText = query;
					command.ExecuteNonQuery();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
				return false;
		}

		public bool createTable(string name, string[] values)
		{
			closeConnectionWithDatabase();
			if(openConnectionWithDatabase())
			{
				try
				{
					string query = "CREATE TABLE " + name + " (";
					for(int i = 0; i < values.Length; i++)
					{
						query += values[i];
					}
					query += ") ENGINE=InnoDb DEFAULT CHARSET=utf8;";
					MySqlCommand parancs = new MySqlCommand(query, mysqlConnWithDatabase);
					parancs.ExecuteNonQuery();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
				return false;
		}
	}
}
