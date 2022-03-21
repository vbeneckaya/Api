
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ThinkingHome.Migrator.Framework;
using ThinkingHome.Migrator.Framework.Extensions;

namespace DAL.Migrations
{
    [Migration(2022_21_03_23_25)]
    public class Migration_2022_21_03_23_25 : Migration
    {
        public override void Apply()
        {
            Database.AddColumn("Users", new Column("FcmToken", DbType.String)); 
           
        }

    }
}
