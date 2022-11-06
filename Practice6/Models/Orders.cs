//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practice6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            this.DocTable = new HashSet<DocTable>();
        }
    
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderSource { get; set; }
        public string OrderEvent { get; set; }
        public int FK_Correspondent { get; set; }
        public Nullable<System.DateTime> OrderCompletedDate { get; set; }
        public bool OrderIsCompleted { get; set; }
    
        public virtual Correspondents Correspondents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocTable> DocTable { get; set; }
    }
}
