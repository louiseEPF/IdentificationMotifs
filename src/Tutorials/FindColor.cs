// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.ML.Probabilistic.Tutorials
{
    using System;
    using Microsoft.ML.Probabilistic.Models;
    using Microsoft.ML.Probabilistic.Factors.Attributes;

    [Example("String tutorials", "Using StringFormat operation to reason about strings", Prefix = "2.")]
    public class FindColor
    {
        public void Run()
        {
            InferArgument();
            InferTemplate();
        }

        private static void InferArgument()
        {
            var engine = new InferenceEngine();
            engine.Compiler.RecommendedQuality = QualityBand.Experimental;

            if (engine.Algorithm is Algorithms.ExpectationPropagation)
            {
                Variable<string> color = Variable.StringCapitalized().Named("color");
                Variable<string> text = Variable.StringFormat("My color is {0}.", color).Named("text");

                text.ObservedValue = "My color is Blue.";

                Console.WriteLine("color is '{0}' ehe", engine.Infer(color));
            }
            else
            {
                Console.WriteLine("This example only runs with Expectation Propagation");
            }
        }

        private static void InferTemplate()
        {
            var engine = new InferenceEngine();
            engine.Compiler.RecommendedQuality = QualityBand.Experimental;

            if (engine.Algorithm is Algorithms.ExpectationPropagation)
            {
                Variable<string> color = Variable.StringCapitalized().Named("color");
                Variable<string> template =
                    (Variable.StringUniform() + Variable.CharNonWord() + "{0}" + Variable.CharNonWord() + Variable.StringUniform()).Named("template");
                Variable<string> text = Variable.StringFormat(template, color).Named("text");

                text.ObservedValue = "Hello, mate! My color is Red.";

                Console.WriteLine("color is '{0}'", engine.Infer(color));
                Console.WriteLine("template is '{0}'", engine.Infer(template));

                text.ObservedValue = "Hi! My color is Green.";

                Console.WriteLine("color is '{0}'", engine.Infer(color));
                Console.WriteLine("template is '{0}'", engine.Infer(template));

                Variable<string> color2 = Variable.StringCapitalized().Named("color2");
                Variable<string> text2 = Variable.StringFormat(template, color2).Named("text2");

                text2.ObservedValue = "Hi! My color is Yellow.";

                Console.WriteLine("color is '{0}'", engine.Infer(color));
                Console.WriteLine("color2 is '{0}'", engine.Infer(color2));
                Console.WriteLine("template is '{0}'", engine.Infer(template));

                Variable<string> text3 = Variable.StringFormat(template, "Pink").Named("text3");

                Console.WriteLine("text3 is '{0}'", engine.Infer(text3));
            }
            else
            {
                Console.WriteLine("This example only runs with Expectation Propagation");
            }
        }
    }
}
