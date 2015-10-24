# Technology stack
This is an app that I threw together in order to tinker with Unity DI and Entity Framework.  I opted to use webforms over mvc simply because I had heard that it has continued to evolve and I was curious how.
All commands within the webforms are handled through ajax via update panels.
Some of the .css is based on the default Site.css that loads with a new webforms project but in a lot of cases, I rogued content from Twitter, Outlook and Facebook.  Basically, I used whatever I found particularly appealing and fit with the overall look and feel I was going for.
I used webapi for the ajax autocomplete feature.  I couldn't believe how well webforms and webapi can co-habitate.
#DB
I took a code-first approach with Entity Framework.  Consequently, the MSSQL local database will get created in the app's /App_Data folder the first time the site runs.  It will be seeded with User, Genre, and Movie data.
#Installation
You should be able to pull this repo down and run it as-is.  *EXCEPT* do note that you'll more than likely need to perform a .NUGET package restore against the solution first.
#Initial Login
  The initial login will be:
  	UserName: MedMoviesAdmin
  	Password: M3dAdm1n
#Other
I had fun putting this together and learned a hell of a lot.  On to the next challenge!
-md
