using System.Data;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2021_12_19_02_34)]
    public class InitDatabaseScheme : Migration
    {
        public override void Apply()
        {
            Database.AddTable("Users",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("NicName", DbType.String.WithSize(100)),
                new Column("Role", DbType.Int16, defaultValue: 1),
                new Column("Level", DbType.Int64, defaultValue: 0),
                new Column("Score", DbType.Int64, defaultValue: 0),
                new Column("DeviceId", DbType.String.WithSize(300)),
                new Column("PasswordHash", DbType.String));
            Database.AddIndex("Users_pk", true, "Users", "Id");
            
        }
    }
}
