using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using biodata.Database.Tables;
using MySql.Data.Entity;

namespace biodata.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BiodataDb : DbContext
    {
        public BiodataDb()
            : base("name=biodataDbEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<BiodataDb>(new CreateDatabaseIfNotExists<BiodataDb>());
            System.Data.Entity.Database.SetInitializer<BiodataDb>(new DropCreateDatabaseIfModelChanges<BiodataDb>());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ContactInfo> Contactinfoes { get; set; }
        public DbSet<CulturalInfo> Culturalinfoes { get; set; }
        public DbSet<EducationInfo> Educationinfoes { get; set; }
        public DbSet<FamilyInfo> Familyinfoes { get; set; }
        public DbSet<PersonalInfo> Personalinfoes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SocialMedia> Socialmedias { get; set; }
        public DbSet<UserProfile> Userprofiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkExperienceInfo> Workexperienceinfoes { get; set; }
    }
}