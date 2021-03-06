﻿using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomQuestionMapper : ICustomQuestionMapper
    {
        private readonly ICustomAnswerMapper _customAnswerMapper;

        public CustomQuestionMapper()
        {
            _customAnswerMapper = new CustomAnswerMapper();
        }

        public CustomQuestionDbModel ToDbModel(CustomQuestionDto item)
        {
            if (item.Id == 0)
            {
                return new CustomQuestionDbModel
                {
                    Answers = _customAnswerMapper.ToDbModelList(item.Answers), Statement = item.Statement,
                    TestId = item.TestId
                };
            }
            else
            {
                return new CustomQuestionDbModel
                {
                    Answers = _customAnswerMapper.ToDbModelList(item.Answers), Id = item.Id, Statement = item.Statement,
                    TestId = item.TestId
                };
            }
        }

        public CustomQuestionDto ToDto(CustomQuestionDbModel item)
        {
            return new CustomQuestionDto
            {
                Answers = _customAnswerMapper.ToDtoList(item.Answers),
                Id = item.Id,
                Statement = item.Statement,
                TestId = item.TestId
            };
        }

        public List<CustomQuestionDbModel> ToDbModelList(List<CustomQuestionDto> items)
        {
            return items.Select(ToDbModel).ToList();
        }

        public List<CustomQuestionDto> ToDtoList(List<CustomQuestionDbModel> items)
        {
            return items.Select(ToDto).ToList();
        }
    }
}