﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RepairShop.Data;

namespace RepairShop.Migrations
{
    [DbContext(typeof(RepairShopContext))]
    partial class RepairShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepairShop.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepairShop.Models.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("UserId");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("SerialNbr")
                        .IsRequired();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("RepairShop.Models.DeviceService", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime?>("DateCompleted");

                    b.Property<DateTime?>("DateStarted");

                    b.Property<long>("DeviceId");

                    b.Property<long>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("DeviceServices");
                });

            modelBuilder.Entity("RepairShop.Models.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("RepairShop.Models.Device", b =>
                {
                    b.HasOne("RepairShop.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RepairShop.Models.DeviceService", b =>
                {
                    b.HasOne("RepairShop.Models.Device", "Device")
                        .WithMany("DeviceServices")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RepairShop.Models.Service", "Service")
                        .WithMany("DeviceServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
