using System;
using System.IO;
using System.Text;
using Npgsql;
using NpgsqlTypes;

namespace UploadApkOnServerDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var connectionString =
                "Host=217.29.62.245;port=5432;database=BiBiGameDev;User ID=BiBiGame;Password=BiBiGame";
            using (FileStream pgFileStream = new FileStream("/Users/valeria/RiderProjects/Api/UploadApkOnServerDB/i.apk", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader pgReader = new BinaryReader(new BufferedStream(pgFileStream)))
                {
                    using var con = new NpgsqlConnection(connectionString);
                    con.Open();

                    var sql = "SELECT version()";

                    using var cmd = new NpgsqlCommand(sql, con);

                    var version = cmd.ExecuteScalar().ToString();

                    Console.WriteLine($"PostgreSQL version: {version}");

                    var Id = Guid.NewGuid();
                    var FileName = "BiBiGame.apk";
                    var Ext = "apk";

                    byte[] Data = pgReader.ReadBytes(Convert.ToInt32(pgFileStream.Length));
                    cmd.CommandText =
                        "INSERT INTO \"Files\" (\"Id\", \"FileName\", \"Ext\", \"Data\") VALUES(@Id, @FileName, @Ext, @Data)";
                    cmd.Parameters.Add("@Id", NpgsqlDbType.Uuid).Value = Id;
                    cmd.Parameters.Add("@FileName", NpgsqlDbType.Varchar).Value = FileName;
                    cmd.Parameters.Add("@Ext", NpgsqlDbType.Varchar).Value = Ext;
                    cmd.Parameters.Add("@Data", NpgsqlDbType.Bytea).Value = Data;

                    cmd.ExecuteNonQuery();
                    
                    con.Close();
                }
            }
        }
    }
}