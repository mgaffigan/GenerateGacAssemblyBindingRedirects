# Generate GAC Assembly Binding Redirects

Tool to generate binding redirects for any assembly based off of what is listed in the GAC.  For example, if you build a tool using version 1 of a dependency, then move to version 2 of the dependency you might run the tool and delete the local copy of the dll to use the newer version.

The tool examines all of the direct referenced dependencies of the assembly and prints the appropriate redirects to take advantage of a GAC version of the dependency.  The assmebly metadata only records direct references, so references of references will not be listed nor will dependencies loaded with `Assembly.LoadFrom` or those loaded by name.

Command:

    GenerateGacAssemblyBindingRedirects.exe TargetAssembly.dll > redirects.txt

Output (in `redirects.txt` using the example above):

```xml
<configuration>
  <runtime>
    <assemblyBinding xmlns='urn:schemas-microsoft-com:asm.v1'>
      <!-- Exact match for mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->

      <!-- Alternates for Microsoft.Owin, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 -->

      <!-- Alternates for Owin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f0ebd12fd5e55cc5 -->

      <!-- Exact match for Esatto.Deployment.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b is present in GAC -->

      <!-- Alternates for Esatto.ServerSide.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.ServerSide.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Exact match for System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a is present in GAC -->

      <!-- Alternates for System.Web.Mvc, Version=5.2.7.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 -->

      <!-- Exact match for System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->

      <!-- Exact match for System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->

      <!-- Exact match for System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->

      <!-- Alternates for Esatto.Core.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.Core.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.FwLtc.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.FwLtc.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.ServerSide.OwinCommon, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->

      <!-- Exact match for System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->

      <!-- Alternates for Esatto.ServiceLocator.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.ServiceLocator.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```

Desired redirects can then be copied to `App.config` or `Web.config` as appropriate.

To print all assemblies, even if found, add `/all` to produce:

Command:

    GenerateGacAssemblyBindingRedirects.exe TargetAssembly.dll /all > redirects.txt

Output (in `redirects.txt` using the example above):

```xml
<configuration>
  <runtime>
    <assemblyBinding xmlns='urn:schemas-microsoft-com:asm.v1'>
      <!-- Exact match for mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->
      <!-- Alternates for mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 -->
      <dependentAssembly>
        <assemblyIdentity name='mscorlib' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='2.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='mscorlib' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='2.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='mscorlib' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='mscorlib' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Alternates for Microsoft.Owin, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 -->

      <!-- Alternates for Owin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f0ebd12fd5e55cc5 -->

      <!-- Exact match for Esatto.Deployment.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b is present in GAC -->
      <!-- Alternates for Esatto.Deployment.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.Deployment.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='1.0.0.0' newVersion='1.0.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.ServerSide.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.ServerSide.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Exact match for System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a is present in GAC -->
      <!-- Alternates for System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a -->
      <dependentAssembly>
        <assemblyIdentity name='System.Web' publicKeyToken='b03f5f7f11d50a3a' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='2.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System.Web' publicKeyToken='b03f5f7f11d50a3a' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='2.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System.Web' publicKeyToken='b03f5f7f11d50a3a' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System.Web' publicKeyToken='b03f5f7f11d50a3a' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Alternates for System.Web.Mvc, Version=5.2.7.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 -->

      <!-- Exact match for System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->
      <!-- Alternates for System.IdentityModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 -->
      <dependentAssembly>
        <assemblyIdentity name='System.IdentityModel' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='3.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System.IdentityModel' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Exact match for System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->
      <!-- Alternates for System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 -->
      <dependentAssembly>
        <assemblyIdentity name='System' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='2.0.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Exact match for System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->
      <!-- Alternates for System.IdentityModel.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 -->
      <dependentAssembly>
        <assemblyIdentity name='System.IdentityModel.Services' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.Core.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.Core.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.FwLtc.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.FwLtc.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.ServerSide.OwinCommon, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->

      <!-- Exact match for System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 is present in GAC -->
      <!-- Alternates for System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 -->
      <dependentAssembly>
        <assemblyIdentity name='System.Core' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='3.5.0.0' />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name='System.Core' publicKeyToken='b77a5c561934e089' />
        <bindingRedirect oldVersion='4.0.0.0' newVersion='4.0.0.0' />
      </dependentAssembly>

      <!-- Alternates for Esatto.ServiceLocator.Common, Version=4.38.0.0, Culture=neutral, PublicKeyToken=dbcbedecba1a223b -->
      <dependentAssembly>
        <assemblyIdentity name='Esatto.ServiceLocator.Common' publicKeyToken='dbcbedecba1a223b' />
        <bindingRedirect oldVersion='4.38.0.0' newVersion='4.39.0.0' />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```