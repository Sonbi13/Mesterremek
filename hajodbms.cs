using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace dbms
{
    class adatbazis
    {
        string kapcs_string;
        MySqlConnection kapcs_mysql;
        MySqlDataAdapter adapter_mysql = new MySqlDataAdapter();
        public adatbazis(string ks) //a hívó oldalról egyben kapja a kapcsolatot
        {
            kapcs_string = ks; // :))
        }
        public adatbazis(string server, string db, string uid, string pwd)
        //részekben kapja a kapcsolati stringet
        {
            kapcs_string = "datasource=" + server + ";database=" + db + ";username=" 
                + uid + ";pwd=" + pwd + ";";
        }
		public bool megnyitas()
		{
			try
			{
				kapcs_mysql = new MySqlConnection(kapcs_string);
				kapcs_mysql.Open();
				return true;
			}
			catch(Exception ex)
			{
				// Show the exception message in a message box
				MessageBox.Show("Connection error: " + ex.Message);
				return false;
			}
		}


		public bool bezaras()
		{
			try
			{
				if(kapcs_mysql != null)
				{
					kapcs_mysql.Close();
				}
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public string szamot_ad(string LKS) // LKS - lekérdezés
        {
            bezaras();
            if (megnyitas())
            {                
                try
                {
                    MySqlCommand parancs = new MySqlCommand(LKS, kapcs_mysql);
                    return parancs.ExecuteScalar().ToString();
                }
                catch (Exception H)
                {
                    return H.ToString(); //visszaküldöm a MySql hiba szövegét
                }
            }
            return "hiba";
        }
		public List<string> listat_ad(string LKS)
		{
			bezaras();
			List<string> valaszlista = new List<string>();
			if(megnyitas())
			{
				try
				{
					MySqlCommand parancs = new MySqlCommand(LKS, kapcs_mysql);
					MySqlDataReader olvaso = parancs.ExecuteReader();
					while(olvaso.Read())
					{
						valaszlista.Add(olvaso[0].ToString());
					}
				}
				catch(Exception Ex)
				{
					valaszlista.Add(Ex.ToString());
				}
				return valaszlista;
			}
			// Log when a "kapcsolati hiba" is added to the list
			Console.WriteLine("Adding 'kapcsolati hiba' to the response list");
			valaszlista.Add("kapcsolati hiba");
			return valaszlista;
		}

		public DataSet tablazatot_ad(string LKS) {
            bezaras();
            DataSet adatok = new DataSet();
            if (megnyitas()) {
                try {
                    adapter_mysql.SelectCommand = new MySqlCommand(LKS, kapcs_mysql);
                    adapter_mysql.Fill(adatok);
                    return adatok;
                }
                catch (Exception) {
                    return adatok;
                }
            }
            return adatok;
        }
        public void ddl_dml(string LKS) {
            bezaras();
            if (megnyitas()) {                
                try {
                    MySqlCommand parancs = new MySqlCommand();
                    parancs.Connection = kapcs_mysql;
                    parancs.CommandText = LKS;
                    parancs.ExecuteNonQuery();
                }
                catch {}
            }
        }
        public List<byte[]> media_lista(string LKS) 
        {
            bezaras();
            List<byte[]> K = new List<byte[]>();
            if (megnyitas()) 
            {
                try {
                    MySqlCommand parancs = new MySqlCommand(LKS, kapcs_mysql);
                    MySqlDataReader olvaso = parancs.ExecuteReader();
                    while (olvaso.Read()) {
                        if (olvaso[0] is byte[])
                        //vizsgálom a típust, ha nem média, akkor nem csinálok semmit
                        {
                            byte[] Uj_media = (byte[])olvaso[0];
                            K.Add(Uj_media);
                        }
                    }
                }
                catch {
                }
            }
            return K;
        }
    }
}
