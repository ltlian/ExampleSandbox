# Sandbox API

A reference project for setting up a .NET5 Web API with unit tests, Swagger, and Application insights.

## Enable request logging

```diff
diff --git a/src/ESWebApi/appsettings.Development.json b/src/ESWebApi/appsettings.Development.json
index 8983e0f..dd59df6 100644
--- a/src/ESWebApi/appsettings.Development.json
+++ b/src/ESWebApi/appsettings.Development.json
@@ -3,7 +3,8 @@
     "LogLevel": {
       "Default": "Information",
       "Microsoft": "Warning",
-      "Microsoft.Hosting.Lifetime": "Information"
+      "Microsoft.Hosting.Lifetime": "Information",
+      "Microsoft.AspNetCore.Hosting": "Information"
     }
   }
 }
```

## Swagger

[Official Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-5.0)

## Application Insights

[Official Microsoft docs](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#enable-application-insights-server-side-telemetry-visual-studio)


Following [official recommendations](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#prerequisites), prefer connection string over instrumentation key.

1. Add Application Insights to the project.

```diff
diff --git a/src/ESWebApi/ESWebApi.csproj b/src/ESWebApi/ESWebApi.csproj
index 9381730..8099d75 100644
--- a/src/ESWebApi/ESWebApi.csproj
+++ b/src/ESWebApi/ESWebApi.csproj
@@ -5,6 +5,7 @@
   </PropertyGroup>

   <ItemGroup>
+    <PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.18.0" />
     <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.2" />
   </ItemGroup>

diff --git a/src/ESWebApi/Startup.cs b/src/ESWebApi/Startup.cs
index d7be8ee..d53c432 100644
--- a/src/ESWebApi/Startup.cs
+++ b/src/ESWebApi/Startup.cs
@@ -26,6 +26,7 @@ namespace ESWebApi
         // This method gets called by the runtime. Use this method to add services to the container.
         public void ConfigureServices(IServiceCollection services)
         {
+            services.AddApplicationInsightsTelemetry();

             services.AddControllers();
             services.AddSwaggerGen(c =>
```

2. Add connection string to user secrets.

```json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=<key>;IngestionEndpoint=<endpoint>"
  }
}
```

Application insights should now be fully functional.

3. (Optional) Following [official recommendations](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#configuration-recommendation-for-microsoftapplicationinsightsaspnetcore-sdk-2150-and-later) and depending on requirements, disable adaptive sampling.

```diff
diff --git a/src/ESWebApi/appsettings.json b/src/ESWebApi/appsettings.json
index d9d9a9b..7b3c9b6 100644
--- a/src/ESWebApi/appsettings.json
+++ b/src/ESWebApi/appsettings.json
@@ -6,5 +6,9 @@
       "Microsoft.Hosting.Lifetime": "Information"
     }
   },
-  "AllowedHosts": "*"
+  "AllowedHosts": "*",
+  "ApplicationInsights": {
+    "EnableAdaptiveSampling": false,
+    "EnablePerformanceCounterCollectionModule": false
+  }
 }
 ```