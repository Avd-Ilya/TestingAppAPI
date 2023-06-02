﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestingAppApi.Data;

#nullable disable

namespace TestingAppAPI.Migrations
{
    [DbContext(typeof(TestingAppDb))]
    partial class TestingAppDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LeftOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("PassedTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Result")
                        .HasColumnType("REAL");

                    b.Property<int?>("TestId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("PassedTests");
                });

            modelBuilder.Entity("Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Task")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TestId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuestionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("SelectedOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnswerOtionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Position")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserAnswerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AnswerOtionId");

                    b.HasIndex("UserAnswerId");

                    b.ToTable("SelectedOptions");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ChapterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("TestingAppApi.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrackedTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TestId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.HasIndex("UserId");

                    b.ToTable("TrackedTests");
                });

            modelBuilder.Entity("UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnswerOptionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PassedTestId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AnswerOptionId");

                    b.HasIndex("PassedTestId");

                    b.HasIndex("QuestionId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("AnswerOption", b =>
                {
                    b.HasOne("Question", null)
                        .WithMany("AnswerOptions")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Chapter", b =>
                {
                    b.HasOne("Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("PassedTest", b =>
                {
                    b.HasOne("Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("Question", b =>
                {
                    b.HasOne("QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId");

                    b.HasOne("Test", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestId");

                    b.Navigation("QuestionType");
                });

            modelBuilder.Entity("SelectedOption", b =>
                {
                    b.HasOne("AnswerOption", "AnswerOtion")
                        .WithMany()
                        .HasForeignKey("AnswerOtionId");

                    b.HasOne("UserAnswer", null)
                        .WithMany("SelectedOptions")
                        .HasForeignKey("UserAnswerId");

                    b.Navigation("AnswerOtion");
                });

            modelBuilder.Entity("Subject", b =>
                {
                    b.HasOne("SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("Test", b =>
                {
                    b.HasOne("Chapter", "Chapter")
                        .WithMany()
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("TrackedTest", b =>
                {
                    b.HasOne("Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");

                    b.HasOne("TestingAppApi.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserAnswer", b =>
                {
                    b.HasOne("AnswerOption", null)
                        .WithMany("UserAnswers")
                        .HasForeignKey("AnswerOptionId");

                    b.HasOne("PassedTest", null)
                        .WithMany("UserAnswers")
                        .HasForeignKey("PassedTestId");

                    b.HasOne("Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("AnswerOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("PassedTest", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("Question", b =>
                {
                    b.Navigation("AnswerOptions");
                });

            modelBuilder.Entity("Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("UserAnswer", b =>
                {
                    b.Navigation("SelectedOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
