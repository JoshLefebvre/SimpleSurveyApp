﻿using DomainLayer.SeedWork;
using DomainLayer.SurveyAggregate;
using DomainLayer.SurveyAggregate.QuestionTypes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLayer.Entities
{
    public class Survey : Entity
    {
        public string SurveyName { get; private set; }

        // Keeping SurveyQuestions private so that new questions cannot be added from "outside the AggregateRoot"
        // but only through the method AddSurveyQuestion() methods which will often include behaviour.
        public List<SurveyQuestion> _surveyQuestions;
        [BsonIgnore]
        public IReadOnlyCollection<SurveyQuestion> SurveyQuestions => _surveyQuestions;

        public Survey()
        {
            _surveyQuestions = new List<SurveyQuestion>();
        }

        public Survey(string surveyName):this()
        {
            SurveyName = surveyName;
        }

        public void AddMultipleChoiceQuestion(string questionText, string questionA, string questionB, 
            string questionC, string questionD)
        {
            var mcQuestion = new MultipleChoiceQuestion(questionText,  questionA, questionB, questionC, questionD);
            _surveyQuestions.Add(mcQuestion);
        }

        public void AddTextQuestion(string questionText)
        {
            var textQuestion = new TextQuestion(questionText);
            _surveyQuestions.Add(textQuestion);
        }

        public void AddScaleQuestion(string questionText, int scale)
        {
            var scaleQuestion = new ScaleQuestion(questionText, scale);
            _surveyQuestions.Add(scaleQuestion);
        }

        public void AddMultiSelectQuestion(string questionText)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void answerQuestion(string id, string answer)
        {
            var question = _surveyQuestions
                .FirstOrDefault(x => x.EntityId.ToString() == id);

            if (question == null)
                throw new Exception("Unable to find question");

            //Using polymorphism to update answer
            question.UpdateAnswer(answer);
        }
    }
}
