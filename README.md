# Credit-Consult
Project for my ASP.Net Core course in NBU

App for scheduling appointments with credit consultants.
Used - Asp.Net Core, Entity Framework Core, MsSql Server, JQuery.

3 roles - Administrator, Consultant, Client.
Administrator can create, edit and remove the offered services by the consultants.
The Clients can create and delete appointments. Every new registered user is assigned to role "Client". Scaffold of Register, Login, Logout of Identity options.

--------------------------------------------------------------------------------------------------------
On first start it will be a little slow because it is seeding a lot of sample data in the database.

There is ONE Administrator seeded on initialization:
	UserName = admin@creditConsult.bg,
        Email = admin@creditConsult.bg,
	Password = Pass@123

Seeded also sample consultants - the seeding can be found in CreditConsult.Data.Seeding

---------------------------------------------------------------------------------------------------------

Background hosted service creating the schedule for next month when the 15th date is passed.