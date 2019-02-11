namespace Vidly.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddMembershipTypeNameColumn : DbMigration
    {
        public override void Up() {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String());
            Sql("UPDATE mt " +
                "SET mt.Name = 'Pay as You Go' " +
                "FROM dbo.MembershipTypes AS mt " +
                "WHERE mt.Id = 1");
            Sql("UPDATE mt " +
               "SET mt.Name = 'Monthly' " +
               "FROM dbo.MembershipTypes AS mt " +
               "WHERE mt.Id = 2");
            Sql("UPDATE mt " +
               "SET mt.Name = 'Quarterly' " +
               "FROM dbo.MembershipTypes AS mt " +
               "WHERE mt.Id = 3");
            Sql("UPDATE mt " +
               "SET mt.Name = 'Yearly' " +
               "FROM dbo.MembershipTypes AS mt " +
               "WHERE mt.Id = 4");
        }

        public override void Down() {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
