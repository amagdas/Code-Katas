using System;

namespace CommandParser
{
    public abstract class Evaluator
    {
        public abstract string Key { get; }
        public abstract string Evaluate(string[] evaluatorArgs);

        protected void ThrowException(string command)
        {
            throw new ArgumentException(string.Format("Invalid input supplied: {0}", command));
        }
    }

    internal class SubstringEvaluator : Evaluator
    {
        public override string Key { get { return "substr"; } }

        public override string Evaluate(string[] evaluatorArgs)
        {
            if (evaluatorArgs.Length != 3)
                ThrowException(Key);

            int startIndex = -1;
            if (!int.TryParse(evaluatorArgs[1], out startIndex))
                ThrowException(Key);

            int charsToTake = -1;
            if (!int.TryParse(evaluatorArgs[2], out charsToTake))
                ThrowException(Key);

            startIndex--;
            if (startIndex > evaluatorArgs[0].Length - 1)
                ThrowException(Key);

            return evaluatorArgs[0].Substring(startIndex, charsToTake);
        }
    }

    internal class ConcatEvaluator : Evaluator
    {
        public override string Key { get { return "concat"; } }
        public override string Evaluate(string[] evaluatorArgs)
        {
            return string.Join("", evaluatorArgs).Trim();
        }
    }

    internal class TrimEvaluator : Evaluator
    {
        public override string Key { get { return "trim"; } }
        public override string Evaluate(string[] evaluatorArgs)
        {
            if (evaluatorArgs.Length != 1)
                ThrowException(Key);

            return evaluatorArgs[0].Trim();
        }
    }
}