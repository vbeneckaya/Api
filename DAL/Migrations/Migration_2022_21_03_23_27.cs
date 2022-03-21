
using System.Data;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2022_21_03_23_27)]
    public class Migration_2022_21_03_23_27 : Migration
    {
        public override void Apply()
        {
            Database.AddTable("Logs",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("UserId", DbType.Guid),
                new Column("DateTime", DbType.Date),
                new Column("Message", DbType.String));
                Database.AddIndex("Logs_pk", true, "Logs", "Id");
            
        }
    }
}
