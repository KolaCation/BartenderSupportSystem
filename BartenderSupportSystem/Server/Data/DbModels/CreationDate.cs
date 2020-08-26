using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.DbModels
{
    internal sealed class CreationDate
    {
        public int Id { get; private set; }
        public int ItemId { get; private set; }
        public DateTimeOffset CreationTime { get; private set; }

        public CreationDate(int id, int itemId, DateTimeOffset creationTime)
        {
            Id = id;
            ItemId = itemId;
            CreationTime = creationTime;
        }
    }
}
