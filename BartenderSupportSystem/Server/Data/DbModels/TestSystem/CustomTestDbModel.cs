using System;
using System.Collections.Generic;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomTestDbModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Topic { get; private set; }
        public string Description { get; private set; }
        public List<CustomQuestionDbModel> Questions { get; private set; }

        public CustomTestDbModel(string name, string topic, string description)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateString(topic, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateString(description, CustomValidatorDefaultValues.StrDefaultMinLength, 255);
            Name = name;
            Topic = topic;
            Description = description;
            Questions = new List<CustomQuestionDbModel>();
        }

        public CustomTestDbModel(int id, string name, string topic, string description) : this(name, topic, description)
        {
            Id = id;
            Questions = new List<CustomQuestionDbModel>();
        }

        public CustomTestDbModel(List<CustomQuestionDbModel> questions, string name, string topic, string description) : this(name, topic, description)
        {
            CustomValidator.ValidateObject(questions);
            Questions = questions;
        }

        public CustomTestDbModel(List<CustomQuestionDbModel> questions, int id, string name, string topic, string description) : this(id, name, topic, description)
        {
            CustomValidator.ValidateObject(questions);
            Questions = questions;
        }
    }
}
