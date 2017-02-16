#if !PLATFORM_DOTNET
using System.Xml.Linq;
#endif

namespace Xunit.Runner.VisualStudio.TestAdapter
{
    public static class RunSettingsHelper
    {
        public static bool DisableAppDomain { get; private set; }

        public static bool DisableParallelization { get; private set; }

        public static bool DesignMode { get; private set; }
/*
        public static bool NoAutoReporters { get; private set; }
        public static string ReporterSwitch { get; private set; }
*/
        /// <summary>
        /// Reads settings for the current run from run settings xml
        /// </summary>
        /// <param name="runSettingsXml">RunSettingsXml of the run</param>
        public static void ReadRunSettings(string runSettingsXml)
        {
            // reset first, do not want to propagate earlier settings in cases where execution host is kept alive
            DisableAppDomain = false;
            DisableParallelization = false;
            DesignMode = true;
            //NoAutoReporters = false;


#if !PLATFORM_DOTNET
            if (!string.IsNullOrEmpty(runSettingsXml))
            {
                try
                {
                    var element = XDocument.Parse(runSettingsXml)?.Element("RunSettings")?.Element("RunConfiguration");
                    if (element != null)
                    {
                        var disableAppDomainString = element.Element("DisableAppDomain")?.Value;
                        bool disableAppDomain;
                        if (bool.TryParse(disableAppDomainString, out disableAppDomain))
                            DisableAppDomain = disableAppDomain;

                        var disableParallelizationString = element.Element("DisableParallelization")?.Value;
                        bool disableParallelization;
                        if (bool.TryParse(disableParallelizationString, out disableParallelization))
                            DisableParallelization = disableParallelization;

                        // DesignMode is available in Test Platform (15.0+ i.e. VS 2017).
                        // It is set to True if test is running from an Editor/IDE context.
                        var designModeString = element.Element("DesignMode")?.Value;
                        bool designMode;
                        if (bool.TryParse(designModeString, out designMode))
                            DesignMode = designMode;
                        DesignMode = false;  // TODO to see perf impact
/*
                        var noAutoReportersString = element.Element("NoAutoReporters")?.Value;
                        bool noAutoReporters;
                        if (bool.TryParse(noAutoReportersString, out noAutoReporters))
                            NoAutoReporters = noAutoReporters;

                        ReporterSwitch = element.Element("ReporterSwitch")?.Value;
*/
                    }
                }
                catch { }
            }
#endif
        }
    }
}
