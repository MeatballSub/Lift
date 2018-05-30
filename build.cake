#tool nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012
#tool nuget:?package=GitLink&version=3.1.0
#tool nuget:?package=GitReleaseNotes&version=0.7.0

//////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var config = Argument("config", "Debug");

//////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////
var solution = GetFiles("./*.sln").FirstOrDefault();
var projects = GetFiles("./src/**/*.csproj").Select(_ => _.FullPath);
var tests = GetFiles("./tests/**/*.csproj").Select(_ => _.FullPath);
var artifactsDir = Directory("./artifacts/");
string version = null;

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
    foreach (var project in projects)
    {
      Information(project);
      DotNetCoreClean(project);
    }

    CleanDirectory(artifactsDir);
  });

Task("Version")
  .Does(() =>
  {
    GitVersion(new GitVersionSettings
    {
      UpdateAssemblyInfo = true,
      LogFilePath = "console",
      OutputType = GitVersionOutput.BuildServer
    });

    var gitVersion = GitVersion(new GitVersionSettings
    {
      OutputType = GitVersionOutput.Json
    });

    version = gitVersion.MajorMinorPatch;
    
    if (config?.ToLower() != "release")
    {
      var tag = gitVersion.PreReleaseLabel;
      var commitNumber = gitVersion.CommitsSinceVersionSourcePadded;
      
      if (!string.IsNullOrEmpty(tag))
      {
        version = $"{version}-{tag}-{commitNumber}";
      }
    }

    Information("Version: " + version);
  });

Task("Restore")
  .Does(() =>
  {
    foreach (var project in projects)
    {
      DotNetCoreRestore(project);
    }
  });

Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Version")
  .IsDependentOn("Restore")
  .Does(() =>
  {
    var settings = new DotNetCoreBuildSettings
    {
      Configuration = config,
      ArgumentCustomization = args => args.Append($"/p:Version={version}")
    };

    foreach (var project in projects)
    {
      DotNetCoreBuild(project, settings);
    }
  });

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
  {
    foreach (var test in tests)
    {
      DotNetCoreTest(test);
    }
  });

Task("Package")
  .IsDependentOn("Test")
  .Does(() =>
  {
    var settings = new DotNetCorePackSettings
    {
      Configuration = config,
      OutputDirectory = artifactsDir,
      NoBuild = true,
      ArgumentCustomization = args => args.Append($"/p:Version={version}")
    };

    foreach (var project in projects.Where(_ => _.EndsWith(".Package.csproj")))
    {
      DotNetCorePack(project, settings);
    }
  });

Task("Default")
  .IsDependentOn("Package");

RunTarget(target);