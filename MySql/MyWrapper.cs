using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MySql.Data.MySqlClient
{
    public class MyConnect
    {
        string _server = string.Empty;
        string _user_id = string.Empty;
        string _password = string.Empty;
        MySqlConnection conn;

        public delegate void OnMakeError(string Error);
        public event OnMakeError MakeError;

        public MyConnect(string server, string user_id, string password)
        {
            _server = server;
            _user_id = user_id;
            _password = password;
        }

        public bool Insert(Hashtable ht, string db, string tbl)
        {
            string field = string.Empty;
            string param = string.Empty;
            string content = string.Empty;
            if (conn != null)
                conn.Close();

            string connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
                _server, _user_id, _password, db);

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                foreach (string key in ht.Keys)
                {
                    field += key + ", ";
                    param += "?" + key + ", ";
                    cmd.Parameters.AddWithValue("?" + key, ht[key]);
                }

                field = field.Remove(field.Length - 2);
                param = param.Remove(param.Length - 2);

                cmd.CommandText ="INSERT INTO " + tbl + " (" + field + ") VALUES (" + param + ")";
                
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //cmd.Parameters[0].Value = 20;
                //cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (MakeError != null)
                {
                    MakeError.Invoke(ex.Message);
                }
            }
            return true;
        }
    }
}
