﻿using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.SurveyAggregate.QuestionTypes
{
    public class MultipleChoiceQuestion : SurveyQuestion
    {
        //For simplicity keeping MC questions static with 4 possible answers
        public MultipleChoiceAnswer AnswerA { get; private set; }
        public MultipleChoiceAnswer AnswerB { get; private set; }
        public MultipleChoiceAnswer AnswerC { get; private set; }
        public MultipleChoiceAnswer AnswerD { get; private set; }

        private MultipleChoiceQuestion(){}//Needed for EFCore

        public MultipleChoiceQuestion(string questionText, string answerA, string answerB, string answerC, string answerD) 
            : base(questionText, QuestionType.MultipleChoice)
        {
            AnswerA = new MultipleChoiceAnswer(answerA, 0);
            AnswerB = new MultipleChoiceAnswer(answerB, 0);
            AnswerC = new MultipleChoiceAnswer(answerC, 0);
            AnswerD = new MultipleChoiceAnswer(answerD, 0);
        }

        public void IncrementAnswerA() => AnswerA.IncrementCount();
        public void IncrementAnswerB() => AnswerA.IncrementCount();
        public void IncrementAnswerC() => AnswerA.IncrementCount();
        public void IncrementAnswerD() => AnswerA.IncrementCount();
    }

    public class MultipleChoiceAnswer
    {
        public string Answer { get; private set; }
        public int Count { get; private set; }

        public MultipleChoiceAnswer(string answer, int count)
        {
            Answer = answer;
            Count = count;
        }

        public void IncrementCount() => Count++;
    }
}
