using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.DbModels
{
    internal sealed class CreationDate
    {
        public Guid Id { get; private set; }
        public Guid ItemId { get; private set; }
        public DateTimeOffset CreationTime { get; private set; }

        public CreationDate(Guid id, Guid itemId, DateTimeOffset creationTime)
        {
            Id = id;
            ItemId = itemId;
            CreationTime = creationTime;
        }
    }
}
