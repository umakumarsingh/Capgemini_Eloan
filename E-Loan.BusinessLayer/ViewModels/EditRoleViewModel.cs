﻿using System;
using System.Collections.Generic;
using System.Text;

namespace E_Loan.BusinessLayer
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
