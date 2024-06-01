﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataNormalizer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DataNormalizer.Models.CityModel", b =>
                {
                    b.Property<int>("cityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("cityID"));

                    b.Property<string>("cityName")
                        .HasColumnType("longtext");

                    b.HasKey("cityID");

                    b.ToTable("ciudades_norm");
                });

            modelBuilder.Entity("DataNormalizer.Models.FamousModel", b =>
                {
                    b.Property<int>("famousID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("famousID"));

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("famousName")
                        .HasColumnType("longtext");

                    b.Property<string>("formattedBirthDate")
                        .HasColumnType("longtext");

                    b.Property<bool>("isBirthday")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("famousID");

                    b.ToTable("fnac_famosos_norm");
                });

            modelBuilder.Entity("DataNormalizer.Models.UnformattedModel", b =>
                {
                    b.Property<int>("unformFamousID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("unformFamousID"));

                    b.Property<string>("unformBirthDate")
                        .HasColumnType("longtext");

                    b.Property<string>("unformName")
                        .HasColumnType("longtext");

                    b.HasKey("unformFamousID");

                    b.ToTable("fnac_famosos");
                });
#pragma warning restore 612, 618
        }
    }
}
