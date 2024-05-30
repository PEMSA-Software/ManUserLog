using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ManUserLog.Properties
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (ManUserLog.Properties.Resources.resourceMan == null)
          ManUserLog.Properties.Resources.resourceMan = new ResourceManager("ManUserLog.Properties.Resources", typeof (ManUserLog.Properties.Resources).Assembly);
        return ManUserLog.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ManUserLog.Properties.Resources.resourceCulture;
      set => ManUserLog.Properties.Resources.resourceCulture = value;
    }
  }
}
