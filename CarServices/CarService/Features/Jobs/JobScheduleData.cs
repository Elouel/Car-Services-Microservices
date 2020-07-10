namespace CarService.Features.Jobs.Models
{
    using System;
    using System.Collections.Generic;

    public class JobScheduleData 
    {
        public int CreatedByEmployeeId { get; set; }

        public string CreateByEmployeeName { get; set; }

        public DateTime DeadLine { get; set; }

        public int DepartmentId { get; set; }

        public int VehicleId { get; set; }


        public IEnumerable<PurchasedServiceModel> PurchasedServices { get; set; }

    }
}
