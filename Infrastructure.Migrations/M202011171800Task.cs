using FluentMigrator;

namespace Infrastructure.Migrations
{
    [Migration(202011171800)]
    public class M202011171800Task : Migration
    {
        private const string TableTasks = "Tasks";

        public override void Up()
        {
            Create
                .Table(TableTasks)
                .WithColumn("ID").AsGuid().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("IsDeleted").AsBoolean();

            Create.PrimaryKey("PK.Tasks")
                .OnTable(TableTasks)
                .Column("ID");
        }

        public override void Down()
        {
            Delete.PrimaryKey("PK.Tasks").FromTable(TableTasks);
            Delete.Table(TableTasks);
        }
    }
}
