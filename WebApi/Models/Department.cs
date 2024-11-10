﻿using System.Text.Json.Serialization;
using WebApi.Models;

namespace WebAPI_Training.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MangerName { get; set; }
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
