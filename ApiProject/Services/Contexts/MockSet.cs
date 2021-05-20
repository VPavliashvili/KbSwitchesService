using System.Collections.Generic;

namespace ApiProject.Services.Contexts
{
    internal class MockSet<T>
    {
        public readonly List<T> data;
        public readonly HashSet<int> indexes;

        public MockSet(IEnumerable<T> mockData)
        {
            data = new();
            indexes = new();

            Populate(mockData);
        }

        private void Populate(IEnumerable<T> mockData)
        {
            foreach (T obj in mockData)
                data.Add(obj);

            for (int i = 1; i <= data.Count; i++)
                indexes.Add(i);
        }
    }

}

