# Acute Pediatrics Orientation
A web application to do online orientation and training for employees

Video Demo: https://www.youtube.com/watch?v=EKetdJxTkq0&feature=youtu.be

Website Demo: https://acutepediatricsorientation.azurewebsites.net/Account/Login?ReturnUrl=%2F

### Login credentials for the website demo:
Educator:
* **email**: admin@email.com
* **password**: admin

Staff:
* **email**: christian@email.com
* **password**: christian

### Functionalities:

As an Educator:
* Manage training package(Edit materials like adding YouTube videos or PDF file)
* Track staff progress

As a Staff:
* View training package
* Sign materials
* Print training package

### Development set up (This project only works for Microsoft SQL Server or Azure SQL Database):
* Publish the Database project to SQL Express. (This will also insert login credentials from above for testing)
* If you wish to publish the database to a different server, make sure to change the connection string in appsettings.json




