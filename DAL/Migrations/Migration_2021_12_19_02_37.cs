using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2021_12_19_02_37)]
    public class Migration_2021_12_19_02_37 : Migration
    {
        public override void Apply()
        {
            Database.AddTable("MyDays",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("UserId", DbType.Guid),
                new Column("Date", DbType.Date),
                new Column("Year", DbType.Int16),
                new Column("Month", DbType.Byte),
                new Column("Day", DbType.Byte),
                new Column("Number", DbType.Byte), //0,1,2 ...5
                new Column("MyDaysGroupId", DbType.Guid)
            );
            Database.AddIndex("MyDays_pk", true, "MyDays", "Id");

            Database.AddTable("MyDaysGroups",
                new Column("Id", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("UserId", DbType.Guid),
                new Column("Size", DbType.Int16),
                new Column("SizeBefore", DbType.Int16),
                new Column("SizeTotal", DbType.Int16),
                new Column("Number", DbType.Byte)
            );
            Database.AddIndex("MyDaysGroups_pk", true, "MyDaysGroups", "Id");
        }

    }
}
