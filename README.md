
# Cursor Pagination in .NET 🚀

Efficient server-side cursor pagination implemented in **.NET** for handling large datasets. Ideal for APIs, web apps, and services requiring high performance with real-time data consistency.

## 📖 Overview
This project demonstrates cursor-based pagination in a **.NET backend**, avoiding traditional `OFFSET`-based pagination for better scalability. The cursor mechanism uses a unique identifier (e.g., sequential ID or timestamp) to paginate records efficiently.

## ✨ Features
- **.NET 6/7/8 API**: Robust backend with optimized cursor logic.
- **Entity Framework Core**: Database queries with cursor-based filtering.
- **Scalable Architecture**: Designed for high-throughput applications.
- **Consistent Pagination**: Resilient to concurrent data changes.
- **Clean API Responses**: Returns `nextCursor` and `prevCursor` for seamless navigation.

## 🛠️ Installation
1. Clone the repository:
   ```bash
   git clone [your-repository-url]
   ```
2. Install dependencies (NuGet packages):
   ```bash
   dotnet restore
   ```
3. Configure your database connection in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Your_Database_Connection_String"
   }
   ```
4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

## 🚀 Usage
1. Run the .NET API:
   ```bash
   dotnet run --project YourProject.Api
   ```
2. Use a client (e.g., Postman, React, Angular) to call the paginated endpoint:
   ```http
   GET /api/items?pageSize=20&cursor=abc123
   ```
3. Navigate using the `nextCursor` or `prevCursor` in the API response.

---

### 🔍 How It Works
#### Server-Side (.NET API)
- **Cursor Logic**:  
  The API uses a column (e.g., `Id` or `CreatedAt`) as the cursor. Example query with Entity Framework:
  ```csharp
  var query = _context.Items
      .Where(i => i.Id > cursor)
      .OrderBy(i => i.Id)
      .Take(pageSize);
  ```

- **API Endpoint**:  
  Returns paginated data with cursors:
  ```csharp
  [HttpGet]
  public ActionResult<PagedResponse<Item>> GetItems([FromQuery] int pageSize, [FromQuery] string cursor)
  {
      var items = _service.GetItems(pageSize, cursor);
      return Ok(new PagedResponse<Item>
      {
          Data = items,
          NextCursor = items.LastOrDefault()?.Id.ToString(),
          HasNext = items.Count >= pageSize
      });
  }
  ```

#### Client-Side
- Use the `nextCursor`/`prevCursor` from the API to request subsequent pages.
- Example client implementation (React/Angular/Blazor) included in the repo.

---

## 💡 Why Cursor Pagination?
- **Performance**: Avoids `OFFSET` overhead in SQL queries.
- **Scalability**: Handles millions of records efficiently.
- **Consistency**: No duplicates/skipped data during pagination.

## 📂 Repository Structure
```
├── YourProject.Api/         # .NET Web API
│   ├── Controllers/         # API endpoints
│   ├── Models/              # Database models
│   ├── Services/            # Pagination logic
│   ├── Migrations/          # Database migrations
│   └── appsettings.json     # Configuration
├── YourProject.Client/      # Frontend (optional)
│   ├── Pages/               # UI components
│   └── ...                 
└── README.md
```

## 🤝 Contributing
Open to contributions! Fork the repo, create a branch, and submit a PR.

## 📄 License
MIT

---

🌟 **Star the repo** if this helps you build faster, scalable APIs!  
🔗 [[GitHub Repository Link]([your-repo-url-here])](https://github.com/Tayyab94/cursor-pagination)
```


Let me know if you want to expand any section! 😊
