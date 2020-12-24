﻿using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces;
using BartenderSupportSystem.Shared.Models;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation
{
    internal sealed class BartenderMapper : IBartenderMapper
    {
        public BartenderDbModel ToDbModel(BartenderDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new BartenderDbModel(item.FirstName, item.LastName);
            }
            else
            {
                return new BartenderDbModel(item.Id, item.FirstName, item.LastName);
            }
        }

        public BartenderDto ToDto(BartenderDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new BartenderDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }
    }
}
