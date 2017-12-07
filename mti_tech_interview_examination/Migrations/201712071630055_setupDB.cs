namespace mti_tech_interview_examination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setupDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mti_Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerContent = c.String(maxLength: 500),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mti_Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Mti_Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(maxLength: 100),
                        QuestionContent = c.String(maxLength: 500),
                        QuestionType = c.Int(nullable: false),
                        QuestionLevel = c.Int(nullable: false),
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
                        level = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mti_Candidate_Question", "QuestionId", "dbo.Mti_Question");
            DropForeignKey("dbo.Mti_Candidate_Question", "CandidateId", "dbo.Mti_Candidate");
            DropForeignKey("dbo.Mti_Answer", "QuestionId", "dbo.Mti_Question");
            DropIndex("dbo.Mti_Candidate_Question", new[] { "QuestionId" });
            DropIndex("dbo.Mti_Candidate_Question", new[] { "CandidateId" });
            DropIndex("dbo.Mti_Answer", new[] { "QuestionId" });
            DropTable("dbo.Mti_Candidate_Question");
            DropTable("dbo.Mti_Candidate");
            DropTable("dbo.Mti_Question");
            DropTable("dbo.Mti_Answer");
        }
    }
}
