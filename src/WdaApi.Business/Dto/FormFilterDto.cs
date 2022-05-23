using System;
using System.Collections.Generic;
using System.Text;

namespace SaturnApi.Business.Dto
{
    public class FormFilterDto : FilterGeneric
    {
        public Guid? Id { get; set; }
        public Guid? EquipamentId { get; set; }
        public int? TypeFormId { get; set; }
        public bool? Status { get; set; }
        public string Question { get; set; }
    }
}
