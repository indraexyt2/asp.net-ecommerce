using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medo;

namespace Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = (Guid)Uuid7.NewUuid7();
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
