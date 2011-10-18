using System;
using CommandParser;
using NUnit.Framework;

namespace StringParserKata
{
    [TestFixture]
    public class StringCommandTest
    {
        /*Make a program to implement an evaluator for the following string functions:
        - function can be one of the following (trim, concat, substr) where:
        ·trim – function with a single parameter which eliminates the empty space from the start and the end of the parameter
        ·concat – function with an undefined number of parameters, concatenates all the given parameters
        ·substr – function with 3 parameters: first is the string on which the substring action will be done, second is the start position(1 is the first position) and the last one is the number of caracters to extract         

        For the following input, the expected output is:
        1. "eval(trim,     code katas rocks )" returns "code katas rocks"
        2. "eval(concat,dependency ,injection ,ftw )" returns "dependency injection ftw"
        3. "eval(substr,Unit testing is easy in presence of good design,6,2)" returns "es"
        4. "eval(concat,eval(trim, par ),eval(trim, all ),ax)" returns "parallax"
        5. "eval(concat,eval(trim,eval(substr,abc,1,2)),bc)" returns "abc"*/

        StringFunctionEvaluator _parser;
        private string _output;

        [SetUp]
        public void Setup()
        {
            _parser = new StringFunctionEvaluator();
            _output = string.Empty;
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenNoInputProvided_ExpectAnExceptionIsThrown()
        {
            Given("");
            Assert.Fail();
        }

        [Test]
        public void GivenEmptyCommand_ReturnEmptyString()
        {
            Given("eval()");
            Expect("");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenInvalidCommand_ExpectAnExceptionIsThrown()
        {
            Given("eval");
            Assert.Fail();
        }

        [Test]
        public void GivenTrimCommand_InputIsTrimmed()
        {
            Given("eval(trim,     code katas rocks )");
            Expect("code katas rocks");
        }

        [Test]
        public void GivenConcatCommand_InputIsConcatenated()
        {
            Given("eval(concat,1,2,3)");
            Expect("123");
        }

        [Test]
        public void GivenConcatCommandHavingInputWithSpaces_InputIsConcatenated()
        {
            Given("eval(concat,dependency ,injection ,ftw )");
            Expect("dependency injection ftw");
        }

        [Test]
        public void GivenSubstrCommandWithInputWithoutEmptySpaces_SubstringIsFound()
        {
            Given("eval(substr,test,1,1)");
            Expect("t");
        }

        [Test]
        public void GivenSubstrCommandWithInputHavingEmptySpaces_SubstringIsFound()
        {
            Given("eval(substr,Unit testing is easy in presence of good design,6,2)");
            Expect("te");
        }

        [Test]
        public void GivenConcatWithNestedTrimFunctionsToEvaluate_InputIsEvaluated()
        {
            Given("eval(concat,eval(trim, par ),eval(trim, all ),ax)");
            Expect("parallax");
        }

        [Test]
        public void GivenConcatWithTrimAndSubstrNestedCommand()
        {
            Given("eval(concat,eval(trim,eval(substr, abc,1,2)),bc)");
            Expect("abc");
        }

        private void Given(string input)
        {
            _output = _parser.Parse(input);
        }

        private void Expect(string expected)
        {
            Assert.AreEqual(expected, _output);
        }
    }
}
