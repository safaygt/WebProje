//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B201200001_WEB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Magaza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Magaza()
        {
            this.Oyuncak = new HashSet<Oyuncak>();
            this.Siparis = new HashSet<Siparis>();
        }
    
        public int Magaza_ID { get; set; }
        public string Magaza_Ad { get; set; }
        public Nullable<int> Siparis_ID { get; set; }
        public Nullable<int> Oyuncak_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oyuncak> Oyuncak { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}
