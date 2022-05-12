# Habit_Tracker
 ##Console Habit Tracker
 
 Project Outline: https://www.thecsharpacademy.com/habit-tracker/

 The project makes a SQLite database connection, creates a database if one doesn't exist. 

 The user is shown a menu of options in the console where they can choose to Create, Read, Update and Delete entries as well as exit the console. 
 
 This app is to record the number of galsses of water drunk in a day. It records the date in dd MMMM yyyy format and quantity as an interger. 
 
 The input from the user is checked for the correct format. If incorrect, user is prompted to try again.
 
 When creating an entry, a check is done to stop duplication. Once created, the user is taken back to main menu.
 
 When deleting and updating, a check is done to see if the entry exists. If it doesn't exist or once the function is complete, the user is taken to main menu.
 




##Journal on how I've tackled the project, challenges and resources I've used for each challenge. 

Recording my journey to complete this first project! 

 > I had already installed Visual Studio, GitDesktop, made a github profile. 

 > Created a new project in VS and comitted it. 

  > Install SQLite and created a db. Got confused between .db and .sqppro files. This was because I was clicking on save project after creating and editing the database. Saving a project creates a .sqppro. This took me a while to figure out. Just press ctrl+s to save and close database, don't click save project.
 
  >For the porject a database is created from VS so no need to create separate .db file. 


 > Pasted the code from csharacademy. Got lots of errors! Got very confused. 
 
 > Followed youtube tutorial on how to make a SQLite connection. https://www.youtube.com/watch?v=APVit-pynwQ&ab_channel=DevNami
 
 > Managed to install a Nuget Package. Realised that I have to pull in the SQLite library by adding using at the top. 
 
 > Still got the errors. 

 > Copied code from the tutorial realised the spelling of the class was different. 

 > Realised I had downloaded a different Nuget package to the Microsoft one. Went back and installed that. So now I am using the intended nuget package for this project. 

 
 >getting error that connection is not opening so going to just follow the youtube tutorial to make the connection instead. Using a System.Data.SQLite nuget package
 
 >Managed to add entry to  the database
 
 >getting errors when trying to delete a specific entry

 
 >Shows CRUD 
 
 >https://www.youtube.com/watch?v=_JJwHXZcE4Y

 > Had installed the professional version of Visual Studio, trial ran out so had to uninstall and install the community version. 
 
 >Came back from a break from coding; getting distracted by the fact that I can't find documentation for System.Data.SQLite, can't really understand microsoft documentation so trying to focus on actually doing what I need to do for the porject

 
 >Deleting specific entry worked. Make sure the user input is set properly. 
 
 >Managed to get all the CRUD fucntions working. 
 
 > Need to put them in methods so I can link it to the main menu.
 
 >Methods tutorial: https://www.youtube.com/watch?v=MkDroqxS8LY
 
 >Then just used the errors to guide me. Have to open and close connections in each method.

>Create a menu using switch statement. https://www.w3schools.com/cs/cs_switch.php 

>Updated the show all method so it displays the column headers as well.

>Updated the columns to be named date and quantity

>Use a while loop in the menu to make sure the menu comes back after selecting each method, so you can do multiple things in the app without having to restart it. 

>Not sure where to start with data validation

>Searched for videos on data validation

> Used tryparse and if function to validate quantity input

>Looking into validating the date input using TryParseExact. 
https://www.youtube.com/watch?v=hyXynrnxR-8&ab_channel=CPlus%2B

> To ensure there aren't any duplicate entries, you can use SQL Unique constraint. It stops the duplicate entry but it does crash the app. 
https://www.youtube.com/watch?v=JMlBU06QcI4&ab_channel=BIwithMina

> To stop the crash and deal with the exception use Try {} catch{}. If there is a duplicate entry it gives you a message to tell you what the error is and then takes you to the beginning to try again instaed of crashing the app.
https://www.youtube.com/watch?v=ZJRg8nrNeeA&ab_channel=MikeDane (video on exception handling)

> To check entry exists before updating or deleting, used a while function to go through all the dates and match it with date entered. Use the Try {} catch {} to go back to main menu if entry doesn't exist.

>Good video on how to use try parse! https://www.youtube.com/watch?v=w7IGs7mh0d4&ab_channel=CoffeeNCode

https://zetcode.com/csharp/sqlite/ good SQLite explanation