namespace mti_tech_interview_examination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712230839 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mti_Answer", "IsRight", c => c.Boolean());
            AddColumn("dbo.Mti_Candidate_Question", "IsText", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mti_Candidate_Question", "IsRight", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mti_Candidate_Question", "IsRight");
            DropColumn("dbo.Mti_Candidate_Question", "IsText");
            DropColumn("dbo.Mti_Answer", "IsRight");
        }
    }
}
