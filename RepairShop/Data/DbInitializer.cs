using RepairShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Data
{
	public static class DbInitializer
	{
		public static void Initialize(RepairShopContext context)
		{
			context.Database.EnsureCreated();

			if (context.Services.Any())
			{
				return;
			}

			var services = new Service[]
			{
				new Service
				{
					Title = "Device Checkup",
					Description = "Includes virus scan, quick hardware diagnostics, dust removal from case (on PCs), and a courtesy screen and keyboard cleaning on laptops and phones.",
					Price = 29.99M,
					ImagePath = @"../images/products/pc-checkup.jpg",
					Type = "Other"
				},
				new Service
				{
					Title = "OS Reinstall",
					Description = "Reinstallation of an operating system, installation of drivers, Microsoft Office (or similar), and other basic software.",
					Price = 39.99M,
					ImagePath = @"../images/products/os_reinstall.jpg",
					Type = "Software"
				},
				new Service
				{
					Title = "Dead Disk Recovery Attempt",
					Description = "Attempt will be made to recover data from a non-working device. Cost includes up to 100 GB of recovery data if successful.",
					Price = 39.99M,
					ImagePath = @"../images/products/DATA_RECOVERY.jpg",
					Type = "Other"
				},
				new Service
				{
					Title = "Install RAM",
					Description = "Install additional RAM for a faster computer.",
					Price = 34.99M,
					ImagePath = @"../images/products/ram.jpg",
					Type = "Hardware"
				},
				new Service
				{
					Title = "Virus Scan",
					Description = "Scan for and remove viruses.",
					Price = 19.99M,
					ImagePath = @"../images/products/virus_scan.jpg",
					Type = "Software"
				},
				new Service
				{
					Title = "Cracked Screen Replacement",
					Description = "Replace cracked or damaged screen",
					Price = 19.99M,
					ImagePath = @"../images/products/cracked_screen.png",
					Type = "Hardware"
				},
				new Service
				{
					Title = "SSD Upgrade",
					Description = "Upgrade your slow hard drive for a blazing fast Solid State Drive.",
					Price = 30.00M,
					ImagePath = @"../images/products/ssd.jpg",
					Type = "Hardware"
				}
			};
			foreach (var service in services)
			{
				context.Services.Add(service);
			}
			context.SaveChanges();

			var users = new User[]
			{

				new User
				{
					UserName = "michaelscott",
					Name = "Michael Scott",
					Address = "1344 Village Dr. Ogden, UT 84405",
					EmailAddress = "michael@hotmail.com",
					Phone = "801-555-1234",
					Password = "password",
					UserType = "customer"
				},
				new User
				{
					UserName = "dschrute",
					Name = "Dwight Schrute",
					Address = "The large field off highway 193",
					EmailAddress = "thedarknightrises@schrutefarms.net",
					Phone = "801-555-8911",
					Password = "password",
					UserType = "customer"
				},
				new User
				{
					UserName = "jhalpert",
					Name = "Jim Halpert",
					Address = "243 Old Farm Rd Hooper, UT 84040",
					EmailAddress = "jhalpert@dmifflin.com",
					Phone = "801-555-1597",
					Password = "password",
					UserType = "customer"
				},
				new User
				{
					UserName = "jaredwilliams",
					Name = "Jared Williams",
					Address = "2153 W. 1145 N. Clinton, UT 84015",
					EmailAddress = "jaredwilliams@mail.weber.edu",
					Phone = "801-555-1234",
					Password = "admin123",
					UserType = "admin"
				}
			};
			foreach (var user in users)
			{
				context.Users.Add(user);
			}
			context.SaveChanges();

			var devices = new Device[]
			{
				new Device
				{
					Make = "Dell",
					Model = "Precision 920",
					Type = "Laptop",
					SerialNbr = "1D5G25S",
					UserId = 1
				},
				new Device
				{
					Make = "LG",
					Model = "G4",
					Type = "Phone",
					SerialNbr = "1A5868DHR2537893AP",
					UserId = 1
				},
				new Device
				{
					Make = "Dell",
					Model = "Alienware 17",
					Type = "Laptop",
					SerialNbr = "8DU72AS",
					UserId = 2
				},
				new Device
				{
					Make = "Apple",
					Model = "Ipad 3",
					Type = "Tablet",
					SerialNbr = "SOBPMWIAPLJ6PUZ4GNMS",
					UserId = 3
				},
				new Device
				{
					Make = "HP",
					Model = "Slimline 260",
					Type = "Desktop",
					SerialNbr = "ZPQIACTUR2HYW3TI54JL",
					UserId = 3
				}
			};
			foreach (var device in devices)
			{
				context.Devices.Add(device);
			}
			context.SaveChanges();

			var dServices = new DeviceService[]
			{
				new DeviceService
				{
					DeviceId = 1,
					ServiceId = 2,
					DateOrdered = new DateTime(2017,5,20),
					Comments = "Somebody moved all the dlls to one folder and the .exes to another!!!",
					Quantity = 3
				},
				new DeviceService
				{
					DeviceId = 1,
					ServiceId = 3,
					DateOrdered = new DateTime(2017,5,20),
					Comments = "Drive still worked. Found a bunch of cat videos in a folder labeled \"Super Important Files.\"",
					Quantity = 2
				},
				new DeviceService
				{
					DeviceId = 1,
					ServiceId = 4,
					DateOrdered = new DateTime(2017,5,20),
					Comments = "How many cat videos do you have!?!?",
					Quantity = 1
				},
				new DeviceService
				{
					DeviceId = 2,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,4,2),
					Comments = "Couldn't find any evidence that somebody is \"spying\" on you. No viruses found.",
					Quantity = 5
				},
				new DeviceService
				{
					DeviceId = 3,
					ServiceId = 2,
					DateOrdered = new DateTime(2017,6,2),
					Comments = "Windows 10 installed on SSD. Game recordings moved to HDD.",
					Quantity = 2
				},
				new DeviceService
				{
					DeviceId = 4,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,6,2),
					Comments = "Removed junk apps and their data to free up space.",
					Quantity = 1
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,4,2),
					Comments = "Removed adware. Case was packed with dust!",
					Quantity = 1
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,4,18),
					Comments = "Removed adware. System feels unstable. Recommended RAM + OS reinstall or new PC.",
					Quantity = 3
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 2,
					DateOrdered = new DateTime(2017,5,27),
					Comments = "Fixed Registry errors. This computer is on its last leg.",
					Quantity = 3
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 2,
					DateOrdered = new DateTime(2017,5,18),
					Comments = "Installed Windows 7. This PC does not have enough Memory to run Windows 10.",
					Quantity = 1
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,6,8),
					Comments = "Removed spyware: Vosteran. We recommend adding RAM to this computer.",
					Quantity = 2
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateOrdered = new DateTime(2017,6,18),
					Comments = "Removed junkware: Power PC Pro. This computer desprately needs more RAM.",
					Quantity = 3
				}

			};
			foreach(var dService in dServices)
			{
				context.DeviceServices.Add(dService);
			}
			context.SaveChanges();
		}
	}
}
