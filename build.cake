#tool nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012
#tool nuget:?package=GitLink&version=3.1.0
#tool nuget:?package=GitReleaseNotes&version=0.7.0

//////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var config = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////
var assemblyVersion = "1.0.0";
var packageVersion = "1.0.0";

var solution = GetFiles("./*.sln").Select(_ => _.FullPath).FirstOrDefault();
var projects = GetFiles("./src/**/*.csproj").Select(_ => _.FullPath);
var tests = GetFiles("./tests/**/*.csproj").Select(_ => _.FullPath);
var artifactsDir = Directory("./artifacts/");

//////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////
Information("");
Information("========================================");
Information("Settings");
Information("========================================");
Information($"Configuration: {config}");

Task("Clean")
  .Does(() =>
  {
    var settings = new DotNetCoreCleanSettings
    {
      Configuration = config
    };

    DotNetCoreClean(solution, settings);

    CleanDirectory(artifactsDir);
  });

Task("Restore")
  .Does(() =>
  {
    DotNetCoreRestore();
  });

Task("Version")
  .Does(() =>
  {
    GitVersion gitVersion = GitVersion();
    assemblyVersion = gitVersion.AssemblySemVer;
    packageVersion = gitVersion.NuGetVersionV2;

    Information($"AssemblySemVer: {assemblyVersion}");
    Information($"NuGetVersionV2: {packageVersion}");
  });

Task("SetAppVeyorVersion")
  .IsDependentOn("Version")
  .WithCriteria(() => AppVeyor.IsRunningOnAppVeyor)
  .Does(() => 
  {
    AppVeyor.UpdateBuildVersion(packageVersion);
  });

Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore")
  .IsDependentOn("Version")
  .IsDependentOn("SetAppVeyorVersion")
  .Does(() =>
  {
    var settings = new DotNetCoreBuildSettings
    {
      Configuration = config,
      NoIncremental = true,
      NoRestore = true,
      MSBuildSettings = new DotNetCoreMSBuildSettings()
        .SetVersion(assemblyVersion)
        .WithProperty("FileVersion", packageVersion)
        .WithProperty("InformationalVersion", packageVersion)
        .WithProperty("nowarn", "7035")
      //ArgumentCustomization = args => args.Append($"/p:Version={version}")
    };

    DotNetCoreBuild(solution, settings);
  });

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
  {
    var settings = new DotNetCoreTestSettings
    {
      Configuration = config
    };

    foreach (var test in tests)
    {
      DotNetCoreTest(test, settings);
    }
  })
  .DeferOnError();

Task("Package")
  .IsDependentOn("Test")
  .WithCriteria(() => HasArgument("pack"))
  .Does(() =>
  {
    var settings = new DotNetCorePackSettings
    {
      Configuration = config,
      OutputDirectory = artifactsDir,
      NoBuild = true,
      NoRestore = true,
      IncludeSymbols = true,
      MSBuildSettings = new DotNetCoreMSBuildSettings()
        .WithProperty("PackageVersion", packageVersion)
        .WithProperty("Copyright", $"Copyright Lethargic Developer {DateTime.Now.Year}")
      //ArgumentCustomization = args => args.Append($"/p:Version={version}")
    };

    foreach (var project in projects.Where(_ => _.EndsWith(".Package.csproj")))
    {
      DotNetCorePack(project, settings);
    }

    FixProps();
  });

Task("Default")
  .IsDependentOn("Package");

RunTarget(target);

private void FixProps()
{
  var restoreSettings = new DotNetCoreRestoreSettings
  {
    MSBuildSettings = new DotNetCoreMSBuildSettings()
      .WithProperty("Version", packageVersion)
      .WithProperty("Configuration", config)
  };

  DotNetCoreRestore(restoreSettings);
}