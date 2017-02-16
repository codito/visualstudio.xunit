using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit.Abstractions;

namespace Xunit.Runner.VisualStudio
{
    public class AssemblyRunInfo
    {
        public string AssemblyFileName;
        public TestAssemblyConfiguration Configuration;
        public IList<TestCase> VSTestCases;
        public IList<ITestCase> TestCases;
    }
}
