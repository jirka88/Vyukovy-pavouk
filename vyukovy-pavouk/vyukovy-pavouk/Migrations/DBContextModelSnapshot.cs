﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vyukovy_pavouk.DBContexts;

#nullable disable

namespace vyukovypavouk.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("vyukovy_pavouk.Data.Kapitola", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdPredmetu")
                        .HasColumnType("int");

                    b.Property<string>("Kontent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Název")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Perex")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdPredmetu");

                    b.ToTable("Kapitoly");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.KapitolaPrerekvizita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdPrerekvizita")
                        .HasColumnType("int");

                    b.Property<int>("KapitolaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPrerekvizita");

                    b.HasIndex("KapitolaId");

                    b.ToTable("kapitolaPrerekvizita");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Predmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nazev")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Vytvoril")
                        .IsRequired()
                        .HasMaxLength(62)
                        .HasColumnType("nvarchar(62)");

                    b.HasKey("Id");

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Prerekvizity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdPrerekvizity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Prerekvizity");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Skupina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IDPredmetu")
                        .HasColumnType("int");

                    b.Property<string>("TmSkupina")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("IDPredmetu");

                    b.ToTable("Skupina");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.SkupinaStudent", b =>
                {
                    b.Property<int>("IdSkupina")
                        .HasColumnType("int");

                    b.Property<int>("IdStudent")
                        .HasColumnType("int");

                    b.HasKey("IdSkupina", "IdStudent");

                    b.HasIndex("IdStudent");

                    b.ToTable("SkupinaStudent");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Splneni", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdKapitoly")
                        .HasColumnType("int");

                    b.Property<int>("IdSkupiny")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Splneni");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmeno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prijmeni")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.StudentSplneni", b =>
                {
                    b.Property<int>("IdStudent")
                        .HasColumnType("int");

                    b.Property<int>("IdSplneni")
                        .HasColumnType("int");

                    b.HasKey("IdStudent", "IdSplneni");

                    b.HasIndex("IdSplneni");

                    b.ToTable("StudentSplneni");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Videa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdKapitoly")
                        .HasColumnType("int");

                    b.Property<string>("Nazev")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Odkaz")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("IdKapitoly");

                    b.ToTable("Videa");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Zadani", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdKapitoly")
                        .HasColumnType("int");

                    b.Property<string>("Odkaz")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("IdKapitoly");

                    b.ToTable("Zadani");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Kapitola", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Predmet", "predmet")
                        .WithMany("Kapitoly")
                        .HasForeignKey("IdPredmetu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("predmet");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.KapitolaPrerekvizita", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Prerekvizity", "prerekvizita")
                        .WithMany("KapitolaPrerekvizita")
                        .HasForeignKey("IdPrerekvizita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("KapitolaPrerekvizita")
                        .HasForeignKey("KapitolaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kapitola");

                    b.Navigation("prerekvizita");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Skupina", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Predmet", "predmet")
                        .WithMany("Skupiny")
                        .HasForeignKey("IDPredmetu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("predmet");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.SkupinaStudent", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Skupina", "Skupina")
                        .WithMany("Student")
                        .HasForeignKey("IdSkupina")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Student", "Student")
                        .WithMany("SkupinaStudent")
                        .HasForeignKey("IdStudent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skupina");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.StudentSplneni", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Splneni", "splneni")
                        .WithMany("StudentSplneni")
                        .HasForeignKey("IdSplneni")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Student", "student")
                        .WithMany("StudentSplneni")
                        .HasForeignKey("IdStudent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("splneni");

                    b.Navigation("student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Videa", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("Videa")
                        .HasForeignKey("IdKapitoly")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kapitola");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Zadani", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("Zadani")
                        .HasForeignKey("IdKapitoly")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kapitola");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Kapitola", b =>
                {
                    b.Navigation("KapitolaPrerekvizita");

                    b.Navigation("Videa");

                    b.Navigation("Zadani");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Predmet", b =>
                {
                    b.Navigation("Kapitoly");

                    b.Navigation("Skupiny");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Prerekvizity", b =>
                {
                    b.Navigation("KapitolaPrerekvizita");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Skupina", b =>
                {
                    b.Navigation("Student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Splneni", b =>
                {
                    b.Navigation("StudentSplneni");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Student", b =>
                {
                    b.Navigation("SkupinaStudent");

                    b.Navigation("StudentSplneni");
                });
#pragma warning restore 612, 618
        }
    }
}
