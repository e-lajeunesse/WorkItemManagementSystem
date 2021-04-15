# WorkItemManagementSystem
---
The Work Item Management System is a web application which allows organizations to track development related tasks among teams and individual team members. 
This is a .NET Core MVC project built primarily with C# as well as HTML/CSS and Javascript.

##Getting Started 

To use the application you can visit the live deployment on azure or download the code from github and run it locally.

### Live Deployment

The live deployment of the application can be found here: [title](https://wimsmvc.azurewebsites.net). To use the application simply register a new account and log in with
your username and password. Note that due to the implentation of user roles some actions will be restricted.

### Running the Application Locally

To run the application locally you will need to create a folder for the project on your local machine, then using the command line navigate to that directory. In github on the repo's 
main page click on the green code dropdown located towards the upper right and copy the link found there into your clipboard. Then back in the command making sure you are still
in your desired directory type "git clone" followed by a single space and then paste in the copied link. It should look something like this but will vary depending on your setup:

> C:\Users\{Your User Name}\{your directory}> git clone https://github.com/e-lajeunesse/WorkItemManagementSystem.git

Once you've cloned the directory you should be able to open it in Visual Studio or your desired IDE however note that this guide assumes the use of Visual Studio and the steps to
get the application working in another IDE may differ. 

#### Nuget Packages
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

The following package may be optionally installed
-Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (this package allows you to see the changes made when editing a cshtml file by refreshing the browser
without needing to restart the application, if desired install version 3.1.11 under WIMS.MVC)
