using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models;

public partial class AllHomeEaseContext : DbContext
{
    public AllHomeEaseContext()
    {
    }

    public AllHomeEaseContext(DbContextOptions<AllHomeEaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeTb> EmployeeTbs { get; set; }

    public virtual DbSet<FeedbackTb> FeedbackTbs { get; set; }

    public virtual DbSet<ImgTb> ImgTbs { get; set; }

    public virtual DbSet<OrdersTb> OrdersTbs { get; set; }

    public virtual DbSet<ServiceImg> ServiceImgs { get; set; }

    public virtual DbSet<ServiceTb> ServiceTbs { get; set; }

    public virtual DbSet<UserTb> UserTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=AllHomeEase;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F003DB2B8");

            entity.ToTable("Employee_tb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dept_name");
            entity.Property(e => e.EmpStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("FREE")
                .HasColumnName("emp_status");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNum).HasColumnName("phone_num");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");

            entity.HasOne(d => d.Service).WithMany(p => p.EmployeeTbs)
                .HasForeignKey(d => d.Serviceid)
                .HasConstraintName("FK__Employee___servi__286302EC");
        });

        modelBuilder.Entity<FeedbackTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3213E83F87FEEAC9");

            entity.ToTable("feedback_tb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Message)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ImgTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__img_tb__3213E83F0EE18129");

            entity.ToTable("img_tb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImpSize).HasColumnName("imp_size");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<OrdersTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders_t__3213E83F598D0B65");

            entity.ToTable("orders_tb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bookingtime)
                .HasPrecision(6)
                .HasColumnName("bookingtime");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Status)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.OrdersTbs)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__orders_tb__emplo__31EC6D26");

            entity.HasOne(d => d.Service).WithMany(p => p.OrdersTbs)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__orders_tb__servi__32E0915F");

            entity.HasOne(d => d.User).WithMany(p => p.OrdersTbs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders_tb__user___33D4B598");
        });

        modelBuilder.Entity<ServiceImg>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__service___3E0DB8AFFEA4A221");

            entity.ToTable("service_img");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("service_id");
            entity.Property(e => e.ImgId).HasColumnName("img_id");

            entity.HasOne(d => d.Img).WithMany(p => p.ServiceImgs)
                .HasForeignKey(d => d.ImgId)
                .HasConstraintName("FK__service_i__img_i__3A81B327");

            entity.HasOne(d => d.Service).WithOne(p => p.ServiceImg)
                .HasForeignKey<ServiceImg>(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__service_i__servi__3B75D760");
        });

        modelBuilder.Entity<ServiceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service___3213E83F06B8F89F");

            entity.ToTable("Service_tb");

            entity.HasIndex(e => e.ServiceName, "UQ__Service___4A8EDF398BBAE3E2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LongDesc)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("long_desc");
            entity.Property(e => e.ServiceCharge).HasColumnName("service_charge");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceTax).HasColumnName("service_tax");
            entity.Property(e => e.ShortDesc)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("short_desc");
        });

        modelBuilder.Entity<UserTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_tb__3213E83FEEFBE79E");

            entity.ToTable("user_tb");

            entity.HasIndex(e => e.Email, "UQ__user_tb__AB6E616496C07B52").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__user_tb__B43B145FDCD16EC3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Dob)
                .HasPrecision(6)
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.HouseNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("house_no");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pincode");
            entity.Property(e => e.Role)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("USER")
                .HasColumnName("role");
            entity.Property(e => e.State)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("street");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
