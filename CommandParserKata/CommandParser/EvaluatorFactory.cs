using System;
using System.Collections.Generic;

namespace CommandParser
{
    public interface IEvaluatorFactory
    {
        Evaluator GetEvaluator(string evaluatorKey);
    }

    class EvaluatorFactory : IEvaluatorFactory
    {
        private readonly IDictionary<string, Evaluator> _availableCommands;

        public EvaluatorFactory()
        {
            _availableCommands = new Dictionary<string, Evaluator>();
            var trimEvaluator = new TrimEvaluator();
            var concatEvaluator = new ConcatEvaluator();
            var substrEvaluator = new SubstringEvaluator();
            _availableCommands.Add(trimEvaluator.Key, trimEvaluator);
            _availableCommands.Add(concatEvaluator.Key, concatEvaluator);
            _availableCommands.Add(substrEvaluator.Key, substrEvaluator);
        }

        public Evaluator GetEvaluator(string commandKey)
        {
            if (string.IsNullOrEmpty(commandKey))
            {
                throw new ArgumentException("Invalid function body to evaluate!");
            }

            Evaluator evaluator;
            if (!_availableCommands.TryGetValue(commandKey, out evaluator))
            {
                throw new ArgumentException("Invalid function to evaluate:" + commandKey);
            }

            return evaluator;
        }
    }
}