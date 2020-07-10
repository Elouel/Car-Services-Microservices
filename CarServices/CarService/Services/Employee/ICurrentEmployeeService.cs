namespace CarServices.Services.Employee
{
    public interface ICurrentEmployeeService
    {
        GarageRoleType GarageRole { get; }

        public string EmployeeName { get; }

        bool IsAdmin { get; }

        public bool IsSalesman { get; }

        public bool IsOperator { get; }

        public int CurrentGarageId { get; }

        public int CurrentEmployeeId { get; }
    }
}
