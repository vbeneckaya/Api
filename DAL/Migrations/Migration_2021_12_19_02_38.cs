using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2021_12_19_02_38)]
    public class Migration_2021_12_19_02_38 : Migration
    {
        public override void Apply()
        {
            Database.AddColumn("MyDays", new Column("Volume", DbType.Int64)); // 0..100 % (10,50,100)
           
        }

    }
}
