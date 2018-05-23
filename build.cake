#tool nuget:?package=GitVersion.CommandLine&version=3.6.5
#tool nuget:?package=GitLink&version=3.1.0
#tool nuget:?package=GitReleaseNotes&version=0.7.0

//////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var config = Argument("config", "Release");

//////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////
var solution = GetFiles("./*.sln").FirstOrDefault();
var projects = ParseSolution(solution)
  .Projects
  .Where(_ => System.IO.Path.GetExtension(_.Path.ToString()) == ".csproj");
var artifactsDir = Directory("./artifacts/");
var buildNumber = EnvironmentVariable("BUILD_NUMBER");
string version = null;

//////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////
Task("Clean")
  .Does(() =>
  {
    foreach (var path in projects.Select(_ => _.Path.ToString()))
    {
      Information(path);
      DotNetCoreClean(path);
    }

    CleanDirectory(artifactsDir);
  });

Task("Version")
  .Does(() =>
  {
    version = GitVersion(new GitVersionSettings
    {
      UpdateAssemblyInfo = false
    }).NuGetVersionV2?.Replace("unstable", "preview");

    if (!string.IsNullOrEmpty(buildNumber))
    {
      version = $"{version}-{buildNumber}";
    }

    Information("Version: " + version);
  });

Task("Restore")
  .Does(() =>
  {
    foreach (var path in projects.Select(_ => _.Path.ToString()))
    {
      DotNetCoreRestore(path);
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

    foreach (var path in projects.Select(_ => _.Path.ToString()))
    {
      DotNetCoreBuild(path, settings);
    }
  });

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
  {
    foreach (var path in projects
      .Where(_ => _.Name.EndsWith("Tests"))
      .Select(_ => _.Path.ToString()))
    {
      DotNetCoreTest(path);
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

    foreach (var path in projects
      .Where(_ => !_.Name.EndsWith("Tests"))
      .Select(_ => _.Path.ToString()))
    {
      DotNetCorePack(path, settings);
    }
  });

Task("Default")
  .IsDependentOn("Package");

RunTarget(target);