
public class Employee 
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class EmployeeDatabase 
{
    private readonly IDbConnection _db;

    public EmployeeDatabase(IDbConnection dbConnection) 
    {
        _db = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
    }

    public Employee GetEmployee(int id) 
    {
        return _db.QueryFirstOrDefault<Employee>(
            "SELECT * FROM Employees WHERE Id = @Id", 
            new { Id = id });
    }

    public void SaveEmployee(Employee emp) 
    {
        _db.Execute(
            "UPDATE Employees SET Name = @Name WHERE Id = @Id", 
            new { emp.Name, emp.Id });
    }

    public void DeleteEmployee(int id) 
    {
        _db.Execute(
            "DELETE FROM Employees WHERE Id = @Id", 
            new { Id = id });
    }
}

public class EmployeeRepository 
{
    private readonly EmployeeDatabase _db;

    public EmployeeRepository(EmployeeDatabase db) 
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public Employee GetEmployee(int id) => _db.GetEmployee(id);

    public void SaveEmployee(Employee emp) => _db.SaveEmployee(emp);

    public void DeleteEmployee(int id) => _db.DeleteEmployee(id);
}

public class EmployeeService 
{
    private readonly EmployeeRepository _repo;

    public EmployeeService(EmployeeRepository repo) 
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public Employee GetEmployee(int id) 
    {
        ValidateEmployeeId(id);
        return _repo.GetEmployee(id);
    }

    public void UpdateEmployee(Employee emp) 
    {
        ValidateEmployee(emp);
        _repo.SaveEmployee(emp);
    }

    public void RemoveEmployee(int id) 
    {
        if (id == 1)
            throw new InvalidOperationException("Chairman cannot be removed.");

        ValidateEmployeeId(id);
        _repo.DeleteEmployee(id);
    }

    private void ValidateEmployeeId(int id)
    {
        if (id < 50)
            throw new ArgumentException("Employee ID must be 50 or higher. IDs 1-49 are reserved.");
    }

    private void ValidateEmployee(Employee emp)
    {
        if (emp is null)
            throw new ArgumentNullException(nameof(emp));

        if (string.IsNullOrWhiteSpace(emp.Name))
            throw new ArgumentException("Employee name cannot be empty.");

        if (emp.Name.Length > 150)
            throw new ArgumentException("Employee name exceeds 150 characters.");
    }
}