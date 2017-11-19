namespace mti_tech_interview_examination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mti_Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerContent = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mti_Candidate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CanidateName = c.String(maxLength: 100),
                        CandidateBirthYear = c.Int(nullable: false),
                        DateMakeExam = c.DateTime(nullable: false),
                        ImgURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mti_Candidate_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        CandidateAnswer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mti_Candidate", t => t.CandidateId, cascadeDelete: true)
                .ForeignKey("dbo.Mti_Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.CandidateId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Mti_Question_Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        IsRight = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mti_Answer", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.Mti_Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mti_Question_Answer", "QuestionId", "dbo.Mti_Question");
            DropForeignKey("dbo.Mti_Question_Answer", "AnswerId", "dbo.Mti_Answer");
            DropForeignKey("dbo.Mti_Candidate_Question", "QuestionId", "dbo.Mti_Question");
            DropForeignKey("dbo.Mti_Candidate_Question", "CandidateId", "dbo.Mti_Candidate");
            DropIndex("dbo.Mti_Question_Answer", new[] { "AnswerId" });
            DropIndex("dbo.Mti_Question_Answer", new[] { "QuestionId" });
            DropIndex("dbo.Mti_Candidate_Question", new[] { "QuestionId" });
            DropIndex("dbo.Mti_Candidate_Question", new[] { "CandidateId" });
            DropTable("dbo.Mti_Question_Answer");
            DropTable("dbo.Mti_Candidate_Question");
            DropTable("dbo.Mti_Candidate");
            DropTable("dbo.Mti_Answer");
        }
    }
}
