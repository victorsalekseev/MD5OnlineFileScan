using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Netcode.Preferences
{
    public class DB
    {
        public static bool use_db = false;
        public static string host = string.Empty;//"192.168.1.2";
        public static string user = string.Empty;//"root";
        public static string password = string.Empty;//"adminadmin";
        public static string db_name = string.Empty;//"aver";
        public static string tbl_fileinfo = "fileinfo";
    }

    public class LocalHash
    {
        public static string db_hash = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hashes.dbh");
    }

    public class OnlineServices
    {
        public static string server_url_hash_details = "http://info.1io.ru/h.php?hash=";
    }
}
