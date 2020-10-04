using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionsPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<CollectionBenchmarks>();
        }
    }

    [MemoryDiagnoser]
    public class CollectionBenchmarks
    {
        private static IEnumerable<ElementParent> GenerateParentCollection(int size)
        {
            return Enumerable.Range(0, size).Select(x => new ElementParent()
            {
                Id = x
            });
        }

        private static IEnumerable<ElementChild> GenerateChildrenCollection(int size, int parentSize)
        {
            var rnd = new Random();
            return  Enumerable.Range(0, size).Select(x => new ElementChild()
            {
                Id = x,
                ParentId = rnd.Next(parentSize)
            });
        }

        [Benchmark(Baseline = true)]
        public void FillChildren()
        {
            var parents = GenerateParentCollection(1000);
            var children = GenerateChildrenCollection(10000, 1000);

            foreach (var parent in parents)
            {
                children = children.Where(x => x.ParentId == parent.Id);
                parent.Children = children;
            }
        }
    }

    public class ElementParent
    {
        public int Id { get; set; }
        public IEnumerable<ElementChild> Children { get; set; }
    }

    public class ElementChild
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
