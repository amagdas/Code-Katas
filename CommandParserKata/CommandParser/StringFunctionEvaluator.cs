using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandParser
{
    public class StringFunctionEvaluator
    {
        private readonly IEvaluatorFactory _factory;
        private readonly Stack<string> _functionsToEvaluate = new Stack<string>();

        public StringFunctionEvaluator() : this(new EvaluatorFactory())
        {
            
        }

        public StringFunctionEvaluator(IEvaluatorFactory factory)
        {
            _factory = factory;
        }

        public string Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Invalid input supplied.");

            if (!input.StartsWith("eval(") || !input.EndsWith(")"))
                ThrowException(input);

            TokenizeInput(input);

            return EvaluateTokens();
        }

        private void TokenizeInput(string input)
        {
            var tokens = input.Split(new string[] { "eval(" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                _functionsToEvaluate.Push(token);
            }
        }

        private string EvaluateTokens()
        {
            var funcToEval = _functionsToEvaluate.Pop();

            var closingParamIndex = funcToEval.IndexOf(")");
            if (closingParamIndex == -1)
                throw new ArgumentException("Invalid input to evaluate.No matching closing bracket found.");

            var commandBody = funcToEval.Substring(0, closingParamIndex);
            var exprNotEvaluated = funcToEval.Substring(closingParamIndex + 1);

            string commandOutput = GetCommandOutput(commandBody);

            exprNotEvaluated = commandOutput + exprNotEvaluated;

            if (_functionsToEvaluate.Any())
            {
                var nextCommand = _functionsToEvaluate.Pop();
                _functionsToEvaluate.Push(nextCommand + exprNotEvaluated);

                return EvaluateTokens();
            }

            return commandOutput;
        }

        private string GetCommandOutput(string commandBody)
        {
            if (string.IsNullOrEmpty(commandBody))
            {
                return string.Empty;
            }

            var commandArgs = commandBody.Split(new[] { "," }, StringSplitOptions.None);

            var command = _factory.GetEvaluator(commandArgs[0]);
            var commandArguments = commandArgs.Skip(1).ToArray();
            return command.Evaluate(commandArguments);
        }

        private void ThrowException(string command)
        {
            throw new ArgumentException(string.Format("Invalid input supplied: {0}", command));
        }
    }
}
