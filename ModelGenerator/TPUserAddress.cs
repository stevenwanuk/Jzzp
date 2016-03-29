//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelGenerator
{
    using System;
    using System.Collections.Generic;
    
    public partial class TPUserAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TPUserAddress()
        {
            this.TPBillRef = new HashSet<TPBillRef>();
        }
    
        public long UserAddressId { get; set; }
        public System.Guid UserId_FK { get; set; }
        public string HouseNumber { get; set; }
        public string AddressField1 { get; set; }
        public string AddressField2 { get; set; }
        public string AddressField3 { get; set; }
        public string TownCity { get; set; }
        public byte[] Postcode { get; set; }
        public string Country { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TPBillRef> TPBillRef { get; set; }
        public virtual TPUser TPUser { get; set; }
    }
}
