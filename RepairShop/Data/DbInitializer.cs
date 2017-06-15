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
					Price = 29.99M
				},
				new Service
				{
					Title = "OS Reinstall",
					Description = "Reinstallation of an operating system, installation of drivers, Microsoft Office (or similar), and other basic software.",
					Price = 39.99M
				},
				new Service
				{
					Title = "Dead Disk Recovery Attempt",
					Description = "Attempt will be made to recover data from a non-working device. Cost includes up to 5 GB of recovery data if successful.",
					Price = 39.99M
				},
				new Service
				{
					Title = "Additional Data Recovery",
					Description = "Charge per 10GB of additional data past the initial 5Gb of data recovered from a non-working device.",
					Price = 19.99M
				}
			};
			foreach (var service in services)
			{
				context.Services.Add(service);
			}
			context.SaveChanges();

			var customers = new Customer[]
			{
				new Customer
				{
					Name = "Michael Scott",
					Address = "1344 Village Dr. Ogden, UT 84405",
					EmailAddress = "michael.is.super.awesome12345@hotmail.com",
					Phone = "801-555-1234"
				},
				new Customer
				{
					Name = "Dwight Schrute",
					Address = "The large field off highway 193",
					EmailAddress = "thedarknightrises@schrutefarms.net",
					Phone = "801-555-8911"
				},
				new Customer
				{
					Name = "Jim Halpert",
					Address = "243 Old Farm Rd Hooper, UT 84040",
					EmailAddress = "jhalpert@dmifflin.com",
					Phone = "801-555-1597"
				}
			};
			foreach (var customer in customers)
			{
				context.Customers.Add(customer);
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
					CustomerId = 1
				},
				new Device
				{
					Make = "LG",
					Model = "G4",
					Type = "Phone",
					SerialNbr = "1A5868DHR2537893AP",
					CustomerId = 1
				},
				new Device
				{
					Make = "Dell",
					Model = "Alienware 17",
					Type = "Laptop",
					SerialNbr = "8DU72AS",
					CustomerId = 2
				},
				new Device
				{
					Make = "Apple",
					Model = "Ipad 3",
					Type = "Tablet",
					SerialNbr = "SOBPMWIAPLJ6PUZ4GNMS",
					CustomerId = 3
				},
				new Device
				{
					Make = "HP",
					Model = "Slimline 260",
					Type = "Desktop",
					SerialNbr = "ZPQIACTUR2HYW3TI54JL",
					CustomerId = 3
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
					DateStarted = new DateTime(2017,5,20),
					DateCompleted = new DateTime(2017,5,23),
					Comments = "Somebody moved all the dlls to one folder and the .exes to another!!!"
				},
				new DeviceService
				{
					DeviceId = 1,
					ServiceId = 3,
					DateStarted = new DateTime(2017,5,20),
					DateCompleted = new DateTime(2017,5,23),
					Comments = "Drive still worked. Found a bunch of cat videos in a folder labeled \"Super Important Files.\""
				},
				new DeviceService
				{
					DeviceId = 1,
					ServiceId = 4,
					DateStarted = new DateTime(2017,5,20),
					DateCompleted = new DateTime(2017,5,21),
					Comments = "How many cat videos do you have!?!?"
				},
				new DeviceService
				{
					DeviceId = 2,
					ServiceId = 1,
					DateStarted = new DateTime(2017,4,2),
					DateCompleted = new DateTime(2017,4,2),
					Comments = "Couldn't find any evidence that somebody is \"spying\" on you. No viruses found."
				},
				new DeviceService
				{
					DeviceId = 3,
					ServiceId = 2,
					DateStarted = new DateTime(2017,6,2),
					DateCompleted = new DateTime(2017,6,3),
					Comments = "Windows 10 installed on SSD. Game recordings moved to HDD."
				},
				new DeviceService
				{
					DeviceId = 4,
					ServiceId = 1,
					DateStarted = new DateTime(2017,6,2),
					DateCompleted = new DateTime(2017,6,2),
					Comments = "Removed junk apps and their data to free up space."
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateStarted = new DateTime(2017,4,2),
					DateCompleted = new DateTime(2017,4,3),
					Comments = "Removed adware. Case was packed with dust!"
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateStarted = new DateTime(2017,4,18),
					DateCompleted = new DateTime(2017,4,19),
					Comments = "Removed adware. System feels unstable. Recommended RAM + OS reinstall or new PC."
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 2,
					DateStarted = new DateTime(2017,5,27),
					DateCompleted = new DateTime(2017,5,18),
					Comments = "Fixed Registry errors. This computer is on its last leg."
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 2,
					DateStarted = new DateTime(2017,5,18),
					DateCompleted = new DateTime(2017,5,19),
					Comments = "Installed Windows 7. This PC does not have enough Memory to run Windows 10."
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateStarted = new DateTime(2017,6,8),
					DateCompleted = new DateTime(2017,6,9),
					Comments = "Removed spyware: Vosteran. We recommend adding RAM to this computer."
				},
				new DeviceService
				{
					DeviceId = 5,
					ServiceId = 1,
					DateStarted = new DateTime(2017,6,18),
					DateCompleted = new DateTime(2017,6,19),
					Comments = "Removed junkware: Power PC Pro. This computer desprately needs more RAM."
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
