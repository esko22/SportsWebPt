using System;
using System.Collections.Generic;
using System.IO;

namespace SportsWebPt.Common.Utilities
{
    public class BulkInsert
    {
        public String BulkInsertDirectory { get; protected set; }

        public BulkInsert(String bulkInsertDirectory)
        {
            BulkInsertDirectory = bulkInsertDirectory;
        }

        public void Invoke(IEnumerable<Object[]> bulkInsertRows, Action<String> bulkInsertMethod, Boolean isEntityFrameworkMethod)
        {
            if (isEntityFrameworkMethod)
                throw new Exception("This method is reserved for non Entity Framework stored procedures. Please use the provided Func<String, Int32> Invoke method ");

            var bulkInsertFilePath = Path.Combine(BulkInsertDirectory, Guid.NewGuid().ToString());
            using (var writer = new StreamWriter(bulkInsertFilePath))
            {
                bulkInsertRows.ForEach(p => writer.WriteLine(String.Join("|", p)));
            }

            bulkInsertMethod(bulkInsertFilePath);

            File.Delete(bulkInsertFilePath);            
        }

        //(cse): This should be an Action<>, but the EF seems to return ints for all
        //stored procdedures that don't have return value.
        public void Invoke(IEnumerable<Object[]> bulkInsertRows, Func<String, Int32> bulkInsertMethod)
        {
            var bulkInsertFilePath = Path.Combine(BulkInsertDirectory, Guid.NewGuid().ToString());
            using (var writer = new StreamWriter(bulkInsertFilePath))
            {
                bulkInsertRows.ForEach(p => writer.WriteLine(String.Join("|", p)));
            }
           
            bulkInsertMethod(bulkInsertFilePath);

            File.Delete(bulkInsertFilePath);
        }
    }

    public class BulkInsert<TInputType, TOutputType> : BulkInsert
    {
        private Int32 _syncKey = 1;

        public Dictionary<Int32, TInputType> ImportData { get; private set; }

        public BulkInsert(String bulkInsertDirectory, IEnumerable<TInputType> importData) : base(bulkInsertDirectory)
        {
            BulkInsertDirectory = bulkInsertDirectory;
            ImportData = new Dictionary<int, TInputType>();
            importData.ForEach(p => ImportData.Add(_syncKey++, p));
        }

        public TOutputType Invoke(IEnumerable<Object[]> bulkInsertRows, Func<String, TOutputType> bulkInsertMethod)
        {
            var bulkInsertFilePath = Path.Combine(BulkInsertDirectory, Guid.NewGuid().ToString());
            using (var writer = new StreamWriter(bulkInsertFilePath))
            {
                bulkInsertRows.ForEach(p => writer.WriteLine(String.Join("|", p)));
            }

            var results = bulkInsertMethod(bulkInsertFilePath);

            File.Delete(bulkInsertFilePath);

            return results;
        }
    }
}
