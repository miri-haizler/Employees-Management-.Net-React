﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<RoleEmployee>? EmployeesRole { get; set; }
    }
}