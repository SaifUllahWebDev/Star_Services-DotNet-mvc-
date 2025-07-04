# 🛡️ Star Security Services – Management System

A comprehensive web-based security agency management system built using **ASP.NET Core MVC** and **Entity Framework Core**, designed for streamlined operations of a security service company.

## 🚀 Features

### 🔐 Authentication & Roles
- Role-based login for **Admin** and **Employee**
- Secure login using **BCrypt** password hashing
- Session-based user handling

### 🧑‍💼 Employee Panel
- Book **Security Guards**, request **Cash Transport Services**, and make **Electronic Product Installations**
- Apply for **Recruitment/Training Announcements**
- View all personal bookings and their statuses with **tab-based UI**
- Cancel bookings directly from the dashboard

### 🧑‍💻 Admin Panel
- View all incoming service requests
- Approve or reject guard, cash, and electronic service bookings
- Post, update, or delete **Recruitment Announcements**
- Post and manage **Job Vacancies** (with image upload)
- Mark vacancies as filled

### 🛠️ Tech Stack
- **Frontend**: HTML, Bootstrap 5, Razor Views
- **Backend**: ASP.NET Core MVC
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: Session-based login with password hashing (BCrypt)
- **File Uploads**: Supports image and document uploads

## 📁 Project Structure

StarSecurityServices/
│
├── Controllers/
│ ├── AdminController.cs
│ ├── EmployeeController.cs
│ ├── GuardBookingController.cs
│ ├── CashServiceController.cs
│ ├── ElectronicServiceRequestController.cs
│ ├── RecruitmentAnnouncementController.cs
│ ├── VacancyController.cs
│
├── Models/
│ ├── GuardBookings.cs
│ ├── CashServiceBooking.cs
│ ├── ElectronicServiceRequest.cs
│ ├── RecruitmentApplication.cs
│ ├── Vacancy.cs
│ └── Employee.cs
│
├── Views/
│ ├── Shared/
│ ├── GuardBooking/
│ ├── CashService/
│ ├── ElectronicServiceRequest/
│ ├── RecruitmentAnnouncement/
│ ├── Vacancy/
│ └── Employee/
│
├── wwwroot/
│ └── uploads/
│ └── vacancies/
│
└── ApplicationDbContext/
└── ApplicationDb.cs

Clone this repository
   ```bash
   git clone https://github.com/yourusername/star-security-services.git
   cd star-security-services
Update the connection string in appsettings.json

Apply migrations and create the database

bash
Copy
Edit
dotnet ef database update
Run the project

bash
Copy
Edit
dotnet run
Visit: https://localhost:5001

✅ Future Improvements
Role management using ASP.NET Identity

Email/SMS notifications

PDF invoice/booking summary generation

Admin analytics dashboard

📜 License
This project is licensed under the MIT License.
