# WorkItemManagementSystem
---
The Work Item Management System is a web application which allows organizations to track development related tasks among teams and individual team members. 
This is a .NET Core MVC project built primarily with C# as well as HTML/CSS and Javascript.

### Getting Started

To use the application you can visit the live deployment on azure or download the code from github and run it locally.

### Live Deployment

The live deployment of the application can be found here: [Live Deployment](https://wimsmvc.azurewebsites.net). To use the application simply register a new account and log in with
your username and password. Note that due to the implentation of user roles some actions will be restricted.

### Running the Application Locally

To run the application locally you will need to create a folder for the project on your local machine, then using the command line navigate to that directory. In github on the repo's 
main page click on the green code dropdown located towards the upper right and copy the link found there into your clipboard. Then back in the command making sure you are still
in your desired directory type "git clone" followed by a single space and then paste in the copied link. It should look something like this but will vary depending on your setup:

> C:\Users\{Your User Name}\{your directory}> git clone https://github.com/e-lajeunesse/WorkItemManagementSystem.git

Once you've cloned the directory you should be able to open it in Visual Studio or your desired IDE however note that this guide assumes the use of Visual Studio and the steps to
get the application working in another IDE may differ. 

### Nuget Packages
The final step before you are able to run the application will be the installation of the required Nuget Packages. In Visual Studio click on the tools tab located on the top of the scree
and select Nuget Package Manger, click on the Installed tab on the top of the page to view the currently installed packages. To find any packages that still need to be installed
click on the Browse tab and type the package name into the search bar. From there select the desired package and then in the window on the right use the checkboxes to select which 
projects to install the package on, use the dropdown to select the desired version and then click the install button to finish the installation. Note that this application was built
for .NET Core 3.1, therefore it is very important to ensure you select the correct package versions as some newer versions require .NET 5.0 and will be incompatible.

The following packages will need to be installed:
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (version 3.1.11 should be installed on the WIMS.MVC project)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (version 3.1.13 should be installed on all projects)
- Microsoft.AspNetCore.Identity.UI (version 3.1.13 should be installed on the WIMS.MVC project)
- Microsoft.EntityFrameworkCore.Proxies (version 3.1.11 should be installed on all projects)
- Microsoft.EntityFrameworkCore.SqlServer (version 3.1.13 should be installed on all projects)
- Microsoft.EntityFrameworkCore.Tools (version 3.1.13 should be installed on WIMS.MVC)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (version 3.1.5 should be installed on WIMS.MVC)
- X.PagedList.Mvc.Core (version 8.0.7 should be installed on WIMS.MVC)

The following package may be optionally installed. It allows you to see changes in the browser made from editing the projects cshtml files without needing to restart the application.
-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (if desired install version 3.1.11 under WIMS.MVC)

### Using the Application
The application has three main areas "Admin", "Teams", and "Work Items".

**Admin**
The admin section allows users to manage the user roles for the application. You are able to create new user roles, edit or delete existing roles, and assign or remove users to roles. Note that normal users do not have access to this section. Users in Manager or Admin roles are able to access the admin section but only Admin users are allowed to make 
changes.

**Teams**
The Teams section allows users to create, edit or delete teams, and reassign employees among teams. The main index page of the Teams section displays a list of all current teams
with the team's Id, Name and Manager. You can create a new team by clicking on the Create New Team link on the top of the table. 
The action dropdown on the right contains additional options to edit or delete teams or view the Team Details page which includes the list of all employees assigned to that team. All users are able to view the main team index and team details pages, however, only Managers are allowed to make any changes to Teams.  

**Work Items**
This is the main section of the application. The main index page displays a list of all current Bugs or Features assigned to a regular user and for a Manager displays all the 
items that are assigned to their team. Above the main a window displays some current data for the given user or team and there are options to add new Bugs/Features or to view 
all currently pending items for the organization or to view all items that have been completed. In the table you are able sort the items based on the Priority, Status, Size, Type, Assignee, or Days Pending in either ascending or descending order by clicking on the arrow icons to the right of the Table Headers. There is also a search bar you can use to filter results which allows you to search based on the description or the user it's assigned to. For each item there is an option dropdown on the right of the table with options to edit the Item, vierw the item's details page or to mark the item complete. Managers will also see options to delete or reassign items.

The Pending Items view shows all currently pending items for the entire organization. Regular users may only view the item details from the pending items pages, while managers can edit, delete or reassign the item. 

The Completed Items page shows all items that have been marked as complete. Regular users may only view the item details from this page, while Managers can delete the item or reassign it which will change it's status to Reopened.

The Item's details page displays all data associated with an item including an optional detailed description that can be included when creating an item. The details page also feature a notes section where users can leave notes for that item. 


