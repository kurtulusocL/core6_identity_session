﻿namespace Identity_Session.Core.CrossCuttingConcern.ViewModels
{
    public class RoleAssignVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasAssign { get; set; }
    }
}
