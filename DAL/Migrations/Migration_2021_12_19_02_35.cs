using System.Data;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2021_12_19_02_35)]
    public class Migration_2021_12_19_02_35 : Migration
    {
        public override void Apply()
        {
            Database.AddTable("Files",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("FileName", DbType.String.WithSize(100)),
                new Column("Ext", DbType.String.WithSize(4)),
                new Column("Data", DbType.Binary)
                );
            Database.AddIndex("Files_pk", true, "Files", "Id");
        }
    }
}
