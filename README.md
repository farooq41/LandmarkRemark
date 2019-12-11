# LandmarkRemark
Add notes on a landmark you are at on the map.

.NET CORE/Angular App to add notes against a location on the map.
Overview 
App Features
1.	User can register to be able to access the app
2.	User can login with their Username & Password
3.	On the landing page User can add notes on their current location by clicking/touching on the icon
4.	Can look at their own notes and see others notes on the map by clicking/touching
5.	User can search by note or user name 
6.	From the filtered list, user can click/touch on an item on the list and the note is highlighted on the map.
7.	User can logout

Application structure & Tech Stack
1.	LandmarkRemark - It is an Angular 5 .NET Core 2.1 app
2.	EF Core is used as ORM
3.	SQL DB on Azure
4.	LandmarkRemarkModel – It is a project for all the entities of the app
5.	LandmarkRemarkService – It is a project for the business logic of the app
6.	LandmarkRemarkData – It is a project that has EF context

Running the app:
There are two ways to test this one.
1)	Locally cloning the app and setup.
2)	Accessing the deployed site on Azure app service [here](https://landmarkremark20191209123439.azurewebsites.net)

Local Setup:
This project was generated with Angular CLI version 1.7.0.
Prerequisites:
1)	.NET Core 2.1
2)	Node JS 
3)	VS 2017 or higher
git clone https://github.com/farooq41/LandmarkRemark.git 
Running Angular:
Go to this location LandmarkRemark/LandmarkRemark/ClientApp and run
1)	Npm install
2)	Ng serve

Locally running the app:
1)	Open Solution file in VS & Set LandmarkRemark Project app as start up and run the solution.

Using the app:
	User can start with registering themselves with username, password & email(fake is fine). If already registered then proceed to the app by logging in. On the landing page User will be asked to allow their location to be identified. User can see their notes against locations that they have added previously and can see others who have added notes. User can add a note at their current location. If there is a note user already has added at their current location then that will be shown. User can also search for a note or by username and the items are filtered below. User can click on an item and the note will be highlighted on the map, so that user can locate the note geographically on the map. User can logout of the application.
Explicit requirements:
1.	As a user (of the application) I can see my current location on a map.
2.	As a user I can save a short note at my current location.
3.	As a user I can see notes that I have saved at the location they were saved on the map.
4.	As a user I can see the location, text, and user-name of notes other users have saved.
5.	As a user I have the ability to search for a note based on contained text or user-name.
Implicit requirements:
1.	For persistence, Azure SQL DB has been used. 
2.	Users can register, login & logout of the app.
3.	User can search and click on a note and the note is geographically highlighted.
4.	Unauthenticated users are not allowed into the app.
5.	For forced unknown urls, user are forced back to login page.
6.	JWT Token authentication is used for validating requests post login.
 
Esimations:
1.	As a user (of the application) I can see my current location on a map. – 3h
2.	As a user I can save a short note at my current location. – 1.5h
3.	As a user I can see notes that I have saved at the location they were saved on the map. 1.5h
4.	As a user I can see the location, text, and user-name of notes other users have saved. – 1h
5.	As a user I have the ability to search for a note based on contained text or user-name. – 2h
6.	Azure(app services, SQL db creation & deployments)  - 2h
7.	JWT Token & security implementation – 1.5h

Known Limitations:
1.	Logging – There is no logging mechanism in the app to identify potential errors and threats
2.	OWASP Top 10 Vulnerabiity – App can be made more robust by adding additional backend validation to get rid of cross site scripting and the likes of it.
3.	Front end Unit testing isn’t there yet.
4.	Search functionality is done on the frontend with the help of angular filters. However, this can be made more efficient by lazy loading based on user input from a redis/rabbit MQ store from the backend.


