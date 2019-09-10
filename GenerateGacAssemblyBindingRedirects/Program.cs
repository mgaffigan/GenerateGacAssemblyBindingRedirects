using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static GenerateGacAssemblyBindingRedirects.NativeMethods;

namespace GenerateGacAssemblyBindingRedirects
{
    class Program
    {
        static void Main(string[] args)
        {
            var asm = Assembly.ReflectionOnlyLoadFrom(args[0]);
            var includeAll = args.Length > 1 && args[1] == "/all";

            var sb = new StringBuilder();
            sb.AppendLine(@"<configuration>
  <runtime>
    <assemblyBinding xmlns='urn:schemas-microsoft-com:asm.v1'>");

            bool isFirst = true;
            foreach (var reference in asm.GetReferencedAssemblies())
            {
                if (!isFirst)
                {
                    sb.AppendLine();
                }
                isFirst = false;

                var versionsPresentInGac = GetGacdVersionsFor(reference).ToList();
                if (versionsPresentInGac.Any(r => r.FullName == reference.FullName))
                {
                    sb.AppendLine($"      <!-- Exact match for {reference.FullName} is present in GAC -->");
                    if (!includeAll)
                    {
                        continue;
                    }
                }

                sb.AppendLine($@"      <!-- Alternates for {reference.FullName} -->");

                foreach (var alt in versionsPresentInGac)
                {
                    sb.AppendLine($@"      <dependentAssembly>
        <assemblyIdentity name='{reference.Name}' publicKeyToken='{string.Join("", reference.GetPublicKeyToken().Select(b => b.ToString("x2")))}' />
        <bindingRedirect oldVersion='{reference.Version}' newVersion='{alt.Version}' />
      </dependentAssembly>");
                }
            }

            sb.AppendLine(@"    </assemblyBinding>
  </runtime>
</configuration>");
            Console.WriteLine(sb.ToString());
        }

        private static IEnumerable<AssemblyName> GetGacdVersionsFor(AssemblyName reference)
        {
            CreateAssemblyNameObject(out var referenceName, reference.Name, CANOF_PARSE_DISPLAY_NAME, IntPtr.Zero);
            CreateAssemblyEnum(out var enumerator, IntPtr.Zero, referenceName, ASM_CACHE_GAC, IntPtr.Zero);

            while (true)
            {
                var retVal = enumerator.GetNextAssembly(IntPtr.Zero, out var nextAsm, 0);
                if (retVal == S_FALSE)
                {
                    break;
                }
                else if (retVal != S_OK)
                {
                    Marshal.ThrowExceptionForHR(retVal);
                }

                var sbName = new StringBuilder(1024);
                int cchName = sbName.Capacity;
                nextAsm.GetDisplayName(sbName, ref cchName, ASM_DISPLAYF_VERSION_CULTURE_PUBLICKEYTOKEN);
                var name = new AssemblyName(sbName.ToString());

                yield return name;
            }
        }
    }
}
