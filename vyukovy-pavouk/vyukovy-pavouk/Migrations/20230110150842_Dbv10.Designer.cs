﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vyukovy_pavouk.DBContexts;

#nullable disable

namespace vyukovypavouk.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230110150842_Dbv10")]
    partial class Dbv10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("PredmetID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PredmetID");

                    b.ToTable("Kapitoly");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.KapitolaPrerekvizita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("KapitolaID")
                        .HasColumnType("int");

                    b.Property<int>("PrerekvizitaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KapitolaID");

                    b.HasIndex("PrerekvizitaID");

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

                    b.Property<bool>("Privatni")
                        .HasColumnType("bit");

                    b.Property<int>("SkupinaID")
                        .HasColumnType("int");

                    b.Property<string>("Vytvoril")
                        .IsRequired()
                        .HasMaxLength(62)
                        .HasColumnType("nvarchar(62)");

                    b.HasKey("Id");

                    b.HasIndex("SkupinaID")
                        .IsUnique();

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Prerekvizity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PrerekvizityID")
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

                    b.Property<string>("TmSkupina")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.ToTable("Skupina");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.SkupinaStudent", b =>
                {
                    b.Property<int>("SkupinaID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("SkupinaID", "StudentID");

                    b.HasIndex("StudentID");

                    b.ToTable("SkupinaStudent");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Splneni", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("KapitolaID")
                        .HasColumnType("int");

                    b.Property<int>("SkupinaID")
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
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("SplneniID")
                        .HasColumnType("int");

                    b.Property<bool>("Uspech")
                        .HasColumnType("bit");

                    b.HasKey("StudentID", "SplneniID");

                    b.HasIndex("SplneniID");

                    b.ToTable("StudentSplneni");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Videa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("KapitolaID")
                        .HasColumnType("int");

                    b.Property<string>("Nazev")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Odkaz")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("KapitolaID");

                    b.ToTable("Videa");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Zadani", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("KapitolaID")
                        .HasColumnType("int");

                    b.Property<string>("Odkaz")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("KapitolaID");

                    b.ToTable("Zadani");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Kapitola", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Predmet", "predmet")
                        .WithMany("Kapitoly")
                        .HasForeignKey("PredmetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("predmet");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.KapitolaPrerekvizita", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("KapitolaPrerekvizita")
                        .HasForeignKey("KapitolaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Prerekvizity", "prerekvizita")
                        .WithMany("KapitolaPrerekvizita")
                        .HasForeignKey("PrerekvizitaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kapitola");

                    b.Navigation("prerekvizita");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Predmet", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Skupina", "Skupina")
                        .WithOne("predmet")
                        .HasForeignKey("vyukovy_pavouk.Data.Predmet", "SkupinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skupina");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.SkupinaStudent", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Skupina", "Skupina")
                        .WithMany("Student")
                        .HasForeignKey("SkupinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Student", "Student")
                        .WithMany("SkupinaStudent")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skupina");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.StudentSplneni", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Splneni", "splneni")
                        .WithMany("StudentSplneni")
                        .HasForeignKey("SplneniID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("vyukovy_pavouk.Data.Student", "student")
                        .WithMany("StudentSplneni")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("splneni");

                    b.Navigation("student");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Videa", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("Videa")
                        .HasForeignKey("KapitolaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("kapitola");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Zadani", b =>
                {
                    b.HasOne("vyukovy_pavouk.Data.Kapitola", "kapitola")
                        .WithMany("Zadani")
                        .HasForeignKey("KapitolaID")
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
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Prerekvizity", b =>
                {
                    b.Navigation("KapitolaPrerekvizita");
                });

            modelBuilder.Entity("vyukovy_pavouk.Data.Skupina", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("predmet");
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