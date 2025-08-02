using System;
using System.Collections.Generic;

namespace MyFullStackApp.API.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Department { get; set; }
}
