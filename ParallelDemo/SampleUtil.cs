using System;
using System.Collections.Generic;
using System.Linq;

namespace ParallelDemo
{
    internal static class SampleUtil
    {
        internal static IEnumerable<ISample> GetSamples()
        {
            return
                from type in typeof(Program).Assembly.DefinedTypes
                where type.IsClass && !type.IsAbstract
                where typeof(ISample).IsAssignableFrom(type)
                let constructor = type.GetConstructor(Type.EmptyTypes)
                where constructor.IsPublic
                select (ISample)constructor.Invoke(null);
        }

        internal static void PrintSamples(IEnumerable<ISample> samples)
        {
            foreach (var sample in samples)
                Console.WriteLine("{0, 4}) {1}", sample.Id, sample.Name);
        }

        internal static ISample GetSelectedSample(List<ISample> samples, string line)
        {
            int sampleNumber;

            if (!int.TryParse(line, out sampleNumber)) return null;

            return samples.Find(sample => sample.Id == sampleNumber);
        }
    }
}
