//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACME_WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Purchases = new HashSet<Purchase>();
        }

        public System.Guid user_ID { get; set; }

        [Required]
        [DisplayName("Username")]
        public string user_Name { get; set; }
        [DisplayName("Email")]
        public string user_Email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be 4- 8 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit.")]
        public string user_Password { get; set; }
        public bool isEmployee { get; set; }

        public string ErrorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
