﻿using FastFood.Models;

namespace FastFood.Dto
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Salary { get; set; }
    }
}
