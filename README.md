# ToDoList

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Backend Setup](#backend-setup)
3. [Frontend Setup](#frontend-setup)
4. [Database Setup with MySQL and HeidiSQL](#database-setup-with-mysql-and-heidisql)
5. [Running the Application](#running-the-application)

---

## Prerequisites
Pastikan Anda telah menginstal beberapa hal berikut ini sebelum memulai:

- [Node.js](https://nodejs.org/) (Versi terbaru LTS)
- [MySQL](https://dev.mysql.com/downloads/mysql/) (Untuk pengelolaan database)
- [HeidiSQL](https://www.heidisql.com/) atau tool lain yang Anda pilih untuk manajemen MySQL
- Visual Studio 2022 (untuk menjalankan backend .NET)
- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (Untuk membangun dan menjalankan backend)

---

## Backend Setup

1. **Buka File Solution:**
   - Buka file `.sln` pada folder `Backend/ToDoList` menggunakan **Visual Studio 2022**.

2. **Instalasi Dependencies dengan NuGet:**
   - Berikut adalah NuGet packages yang digunakan dalam proyek ini:
     - `Microsoft.Extensions.DependencyInjection.Abstractions`
     - `MySql.Data`
     - `MySqlConnector`
     - `MySqlConnector.DependencyInjection`
     - `Swashbuckle.AspNetCore` (untuk dokumentasi API dengan Swagger)

   - Pastikan semua NuGet packages sudah terpasang dengan benar melalui Visual Studio:
     - Klik kanan pada proyek di **Solution Explorer** > **Manage NuGet Packages**.
     - Periksa bahwa semua paket di atas telah terinstal, atau tambahkan jika perlu.

3. **Konfigurasi Environment:**
   - Sesuaikan `appsettings.json` untuk koneksi MySQL:
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "server=your_server;port=your_port;user=your_user;password=your_password;database=todolist"
   }
   jalankan dengan ToDoList Profile

---

## Frontend Setup

1. **Instalasi Dependencies:**
   - Pastikan Node.js sudah terpasang, kemudian jalankan:
     - `npm install`

2. **Konfigurasi Environment:**
   - Sesuaikan link URL dengan URL Backend
     - `jika backend tidak jalan pada https://localhost:7296, ubah semua port pada fetch, samakan port pada localhostnya.`

3. **Jalankan Frontend:**
   - Untuk memulai aplikasi frontend, jalankan perintah:
     - `npm run dev`

---

## Database Setup with MySQL and HeidiSQL

1. **Install MySQL:**
   - Install MYSQL dari browser

2. **Import Database Schema:**
   - Untuk mengimpor file .sql `saya menggunakan HeidiSQL`:
     - `Buka HeidiSQL dan sambungkan ke server MySQL Anda.`
     - `Klik kanan pada Database > Create New untuk membuat database baru (jika belum ada).`
     - `Pilih database yang telah dibuat, klik kanan > Load SQL file.`
     - `Pilih file .sql yang Anda miliki, lalu jalankan untuk membuat tabel dan mengisi data yang diperlukan.`

3. **Konfigurasi Koneksi Database:**
   - Pastikan koneksi database pada backend sudah benar di appsettings.json:
     - ```json 
       "ConnectionStrings": {
         "DefaultConnection": "server=your_server;port=your_port;user=your_user;password=your_password;database=todolist"
       }

---

## Running the Application

1. **Jalankan Backend:**
   - Jalankan backend dari Visual Studio 2022.

2. **Jalankan Frontend:**
   - Jalankan frontend dengan perintah npm run dev dari direktori frontend.

3. **Akses Aplikasi:**
   - Akses frontend pada alamat http://localhost:3000 (atau sesuai port yang diatur).

