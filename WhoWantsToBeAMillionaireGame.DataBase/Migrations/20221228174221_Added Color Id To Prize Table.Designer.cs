﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhoWantsToBeAMillionaireGame.DataBase;

#nullable disable

namespace WhoWantsToBeAMillionaireGame.DataBase.Migrations
{
    [DbContext(typeof(WhoWantsToBeAMillionaireGameDbContext))]
    [Migration("20221228174221_Added Color Id To Prize Table")]
    partial class AddedColorIdToPrizeTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("Text", "QuestionId")
                        .IsUnique();

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.ColorPrize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ColorValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("PrizeColors");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.GameQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("GameId", "QuestionId");

                    b.ToTable("GameQuestions");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Prize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isEnable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Prize");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Text")
                        .IsUnique();

                    b.ToTable("Question");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Answer", b =>
                {
                    b.HasOne("WhoWantsToBeAMillionaireGame.DataBase.Entities.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.GameQuestion", b =>
                {
                    b.HasOne("WhoWantsToBeAMillionaireGame.DataBase.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("WhoWantsToBeAMillionaireGame.DataBase.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}