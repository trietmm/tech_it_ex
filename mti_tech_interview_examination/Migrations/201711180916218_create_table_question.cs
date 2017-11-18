namespace mti_tech_interview_examination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_question : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mti_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(maxLength: 100),
                        QuestionContent = c.String(maxLength: 500),
                        QuestionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mti_Question");
        }
    }
}
