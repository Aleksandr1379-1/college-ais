# CollegeAis — АИС колледжа (модуль “Приёмная комиссия”)

Проект: ASP.NET Core (Razor Pages) + PostgreSQL + Entity Framework Core.  
Сейчас реализованы:
- Абитуриенты (`/Applicants`)
- Справочник специальностей (`/Programs`)

---

## 1) Требования

### Установить на ПК:
- **.NET SDK 8**
  - Проверка:
    ```powershell
    dotnet --version
    ```
- **PostgreSQL** (локально или на сервере)
- **Git**
  - Проверка:
    ```powershell
    git --version
    ```
- **VS Code** + расширение **C#**

---

## 2) Как скачать проект с GitHub на новый ПК

В папке, где будут проекты (например `C:\Projects`):

```powershell
cd C:\Projects
git clone https://github.com/<YOUR_USERNAME>/<REPO_NAME>.git
cd <REPO_NAME>
code .
