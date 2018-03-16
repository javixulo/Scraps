using Microsoft.EntityFrameworkCore;

namespace Scraps.Model
{
    public partial class PicManagerContext : DbContext
    {
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<EventTyped> EventTyped { get; set; }
        public virtual DbSet<IncludedFolder> IncludedFolder { get; set; }
        public virtual DbSet<ExcludedFolder> ExcludedFolder { get; set; }
		public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonInPicture> PersonInPicture { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<PictureEvent> PictureEvent { get; set; }
        public virtual DbSet<PictureGroup> PictureGroup { get; set; }
        public virtual DbSet<PictureTag> PictureTag { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

		// Unable to generate entity type for table 'Settings'. Please see the warning messages.

		private string _connectionString;

		public PicManagerContext(string connectionString)
		{
			_connectionString = connectionString;
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//optionsBuilder.UseSqlite(@"Datasource=E:\Dropbox\Proyectos\VisualStudio\Scraps\DB\picManagarDB_test.db");
			optionsBuilder.UseSqlite(_connectionString);
	        SQLitePCL.Batteries_V2.Init();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("DATE");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("DATE");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("sqlite_autoindex_EventType_1");
            });

            modelBuilder.Entity<EventTyped>(entity =>
            {
				entity.HasKey(e => new { e.Event, e.Type });

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.EventTyped)
                    .HasForeignKey(d => d.Event)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.EventTyped)
                    .HasForeignKey(d => d.Type);
            });

			modelBuilder.Entity<ExcludedFolder>(entity =>
			{
				entity.HasKey(e => e.Path)
					.HasName("sqlite_autoindex_ExcludedFolder_1");
			});

			modelBuilder.Entity<IncludedFolder>(entity =>
			{
				entity.HasKey(e => e.Path)
					.HasName("sqlite_autoindex_IncludedFolder_1");
			});

			modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<PersonInPicture>(entity =>
            {
                entity.HasKey(e => new { e.Person, e.Picture })
                    .HasName("sqlite_autoindex_PersonInPicture_1");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.PersonInPicture)
                    .HasForeignKey(d => d.Person);

                entity.HasOne(d => d.PictureNavigation)
                    .WithMany(p => p.PersonInPicture)
                    .HasForeignKey(d => d.Picture);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.FileName).IsRequired();

                entity.Property(e => e.Folder).IsRequired();
            });

            modelBuilder.Entity<PictureEvent>(entity =>
            {
                entity.HasKey(e => new { e.Picture, e.Event })
                    .HasName("sqlite_autoindex_PictureEvent_1");

                entity.HasOne(d => d.EventNavigation)
                    .WithMany(p => p.PictureEvent)
                    .HasForeignKey(d => d.Event);

                entity.HasOne(d => d.PictureNavigation)
                    .WithMany(p => p.PictureEvent)
                    .HasForeignKey(d => d.Picture);
            });

            modelBuilder.Entity<PictureGroup>(entity =>
            {
                entity.HasKey(e => new { e.Picture, e.Group })
                    .HasName("sqlite_autoindex_PictureGroup_1");

                entity.HasOne(d => d.GroupNavigation)
                    .WithMany(p => p.PictureGroup)
                    .HasForeignKey(d => d.Group);

                entity.HasOne(d => d.PictureNavigation)
                    .WithMany(p => p.PictureGroup)
                    .HasForeignKey(d => d.Picture);
            });

            modelBuilder.Entity<PictureTag>(entity =>
            {
                entity.HasKey(e => new { e.Picture, e.Tag })
                    .HasName("sqlite_autoindex_PictureTag_1");

                entity.HasOne(d => d.PictureNavigation)
                    .WithMany(p => p.PictureTag)
                    .HasForeignKey(d => d.Picture);

                entity.HasOne(d => d.TagNavigation)
                    .WithMany(p => p.PictureTag)
                    .HasForeignKey(d => d.Tag);
            });

	        modelBuilder.Entity<Settings>(entity =>
	        {
		        entity.HasKey(e => e.Key)
			        .HasName("sqlite_autoindex_Settings_1");
	        });

			modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("sqlite_autoindex_Tag_1");
            });
        }
    }
}