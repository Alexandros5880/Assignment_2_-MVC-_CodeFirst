namespace Assignment_2__MVC__CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Trainers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Trainers", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "LastName", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Trainers", "FirstName", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Assignments", "Title", c => c.String(nullable: false, maxLength: 5));
        }
    }
}
