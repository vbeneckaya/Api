using System.Data;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2021_12_19_02_36)]
    public class Migration_2021_12_19_02_36 : Migration
    {
        public override void Apply()
        {
            Database.AddTable("Downloads",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("Date", DbType.Date),
                new Column("Time", DbType.Time),
                new Column("Version", DbType.String),
                new Column("IP", DbType.String)
                );
            Database.AddIndex("Downloads_pk", true, "Downloads", "Id");
        }
    }
}
