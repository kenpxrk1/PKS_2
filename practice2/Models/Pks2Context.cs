using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace practice2.Models;

public partial class Pks2Context : DbContext
{
    public Pks2Context()
    {
    }

    public Pks2Context(DbContextOptions<Pks2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BuildingMaterial> BuildingMaterials { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<EstateObject> EstateObjects { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeParameter> GradeParameters { get; set; }

    public virtual DbSet<Realtor> Realtors { get; set; }

    public virtual DbSet<Selling> Sellings { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=pks2;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuildingMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("building_materials_pkey");

            entity.ToTable("building_materials");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedNever()
                .HasColumnName("material_id");
            entity.Property(e => e.MaterialName).HasColumnName("material_name");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("districts_pkey");

            entity.ToTable("districts");

            entity.Property(e => e.DistrictId)
                .ValueGeneratedNever()
                .HasColumnName("district_id");
            entity.Property(e => e.DistrictName).HasColumnName("district_name");
        });

        modelBuilder.Entity<EstateObject>(entity =>
        {
            entity.HasKey(e => e.ObjectId).HasName("estate_objects_pkey");

            entity.ToTable("estate_objects");

            entity.Property(e => e.ObjectId)
                .ValueGeneratedNever()
                .HasColumnName("object_id");
            entity.Property(e => e.AdDate).HasColumnName("ad_date");
            entity.Property(e => e.Adress).HasColumnName("adress");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.EstateObjectsDescription).HasColumnName("estate_objects_description");
            entity.Property(e => e.EstateObjectsMaterial).HasColumnName("estate_objects_material");
            entity.Property(e => e.EstateObjectsSquare).HasColumnName("estate_objects_square");
            entity.Property(e => e.Floorr).HasColumnName("floorr");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.QuantityOfRooms).HasColumnName("quantity_of_rooms");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Types).HasColumnName("types");

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.EstateObjects)
                .HasForeignKey(d => d.District)
                .HasConstraintName("estate_objects_district_fkey");

            entity.HasOne(d => d.EstateObjectsMaterialNavigation).WithMany(p => p.EstateObjects)
                .HasForeignKey(d => d.EstateObjectsMaterial)
                .HasConstraintName("estate_objects_estate_objects_material_fkey");

            entity.HasOne(d => d.TypesNavigation).WithMany(p => p.EstateObjects)
                .HasForeignKey(d => d.Types)
                .HasConstraintName("estate_objects_types_fkey");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("grades_pkey");

            entity.ToTable("grades");

            entity.Property(e => e.GradeId)
                .ValueGeneratedNever()
                .HasColumnName("grade_id");
            entity.Property(e => e.DateOfGrade).HasColumnName("date_of_grade");
            entity.Property(e => e.Grade1).HasColumnName("grade");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.ParamId).HasColumnName("param_id");

            entity.HasOne(d => d.Object).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ObjectId)
                .HasConstraintName("grades_object_id_fkey");

            entity.HasOne(d => d.Param).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ParamId)
                .HasConstraintName("grades_param_id_fkey");
        });

        modelBuilder.Entity<GradeParameter>(entity =>
        {
            entity.HasKey(e => e.ParamId).HasName("grade_parameters_pkey");

            entity.ToTable("grade_parameters");

            entity.Property(e => e.ParamId)
                .ValueGeneratedNever()
                .HasColumnName("param_id");
            entity.Property(e => e.ParamName).HasColumnName("param_name");
        });

        modelBuilder.Entity<Realtor>(entity =>
        {
            entity.HasKey(e => e.RealtorId).HasName("realtor_pkey");

            entity.ToTable("realtor");

            entity.Property(e => e.RealtorId)
                .ValueGeneratedNever()
                .HasColumnName("realtor_id");
            entity.Property(e => e.RealtorLastname).HasColumnName("realtor_lastname");
            entity.Property(e => e.RealtorName).HasColumnName("realtor_name");
            entity.Property(e => e.RealtorPhone).HasColumnName("realtor_phone");
            entity.Property(e => e.RealtorSurname).HasColumnName("realtor_surname");
        });

        modelBuilder.Entity<Selling>(entity =>
        {
            entity.HasKey(e => e.SellingId).HasName("selling_pkey");

            entity.ToTable("selling");

            entity.Property(e => e.SellingId)
                .ValueGeneratedNever()
                .HasColumnName("selling_id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RealtorId).HasColumnName("realtor_id");
            entity.Property(e => e.SellingDate).HasColumnName("selling_date");

            entity.HasOne(d => d.Object).WithMany(p => p.Sellings)
                .HasForeignKey(d => d.ObjectId)
                .HasConstraintName("selling_object_id_fkey");

            entity.HasOne(d => d.Realtor).WithMany(p => p.Sellings)
                .HasForeignKey(d => d.RealtorId)
                .HasConstraintName("selling_realtor_id_fkey");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("types_pkey");

            entity.ToTable("types");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(1)
                .HasColumnName("type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
