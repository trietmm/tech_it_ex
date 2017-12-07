namespace mti_tech_interview_examination.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Interview_Examination_Context : DbContext
    {
        // Your context has been configured to use a 'Interview_Examination_Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'mti_tech_interview_examination.Models.Entity.Interview_Examination_Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Interview_Examination_Model' 
        // connection string in the application configuration file.
        public Interview_Examination_Context()
            : base("name=Interview_Examination_Context")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Mti_Question> Mti_Question { get; set; }
        public virtual DbSet<Mti_Candidate> Mti_Candidate { get; set; }
        public virtual DbSet<Mti_Answer> Mti_Answer { get; set; }
        public virtual DbSet<Mti_Candidate_Question> Mti_Candidate_Question { get; set; }
        
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}