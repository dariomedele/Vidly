namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNameIntoMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("update membershiptypes set name='Pay as You Go' where id =1 ");

            Sql("update membershiptypes set name='Monthly' where id  =2");

            Sql("update membershiptypes set name='Quarterly' where id =3 ");

            Sql("update membershiptypes set name='Annually' where id =4 ");
        }
        
        public override void Down()
        {
        }
    }
}
