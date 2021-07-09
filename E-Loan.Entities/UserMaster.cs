using Microsoft.AspNetCore.Identity;

namespace E_Loan.Entities
{
    public class UserMaster : IdentityUser
    {
        /// <summary>
        /// Use this class for User and Identity manager
        /// </summary>
        public string Contact { get; set; }
        public string Address { get; set; }
        public IdProofType? IdproofTypes { get; set; }
        public string IdProofNumber { get; set; }
        public bool Enabled { get; set; }
    }
}
