//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblBook()
        {
            this.tblBorrowHistories = new HashSet<tblBorrowHistory>();
        }
    
        public int bookId { get; set; }
        public string Title { get; set; }
        public string serialNumber { get; set; }
        public string Author { get; set; }
        public string publisher { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBorrowHistory> tblBorrowHistories { get; set; }
    }
}
