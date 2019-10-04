using System;

namespace BDSA2019.Lecture06.Entities
{
    public class AuditInfo
    {
        public int Id { get; set; }

        public string Entity { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string WhyTheF { get; set; }
    }
}