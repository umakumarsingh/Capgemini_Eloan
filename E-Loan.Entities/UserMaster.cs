using Microsoft.AspNetCore.Identity;
using System;

namespace E_Loan.Entities
{
    public class UserMaster : IdentityUser
    {
        /// <summary>
        /// Use this class for User and Identity manager
        /// </summary>
        public string Contact { get; set; }
        public string Address { get; set; }
        //public enum IdproofType { get; set; }
        public string IdProof { get; set; }
        public bool Enabled { get; set; }
    }
}
