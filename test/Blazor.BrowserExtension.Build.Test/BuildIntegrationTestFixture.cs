using System.Reflection;
using Blazor.BrowserExtension.Build.Test.Helpers;

namespace Blazor.BrowserExtension.Build.Test
{
    public class BuildIntegrationTestFixture
    {
        private const string DotNetCommand = "dotnet";
        private readonly string rootTestDirectory;
        private readonly string rootSolutionDirectory;

        public BuildIntegrationTestFixture()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            rootTestDirectory = currentDirectory[..currentDirectory.LastIndexOf(Path.DirectorySeparatorChar + "bin")];
            rootSolutionDirectory = rootTestDirectory[..rootTestDirectory.LastIndexOf(Path.DirectorySeparatorChar + "test")];

            var packagesCacheDirectory = Path.Combine(rootSolutionDirectory, "PackagesCache");
            if (Directory.Exists(packagesCacheDirectory))
            {
                Directory.Delete(packagesCacheDirectory, true);
            }

            var assembly = Assembly.GetExecutingAssembly();
            var configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration;
            CommandHelper.ExecuteCommandVoid(DotNetCommand, $"pack --no-build --no-restore --configuration {configuration}", rootSolutionDirectory);
        }

        public void ExecuteDotnetCommand(string command, string testProjectName)
            => CommandHelper.ExecuteCommandVoid(DotNetCommand, command, GetTestProjectDirectory(testProjectName));

        public void ExecuteDotnetRestoreCommand(string testProjectName)
            => ExecuteDotnetCommand("restore --no-cache", testProjectName);

        public void ExecuteDotnetBuildCommand(string testProjectName)
            => ExecuteDotnetCommand("build --no-restore", testProjectName);

        public void ExecuteDotnetPublishCommand(string testProjectName)
            => ExecuteDotnetCommand("publish --no-restore", testProjectName);

        public string GetTestProjectDirectory(string testProjectName)
            => Path.Combine(rootTestDirectory, "TestProjects", testProjectName);

        public Task<WebDriverExtensionHelper> LoadExtensionBuildOutput(string testProjectName)
        {
            var copyToBinExtensionPath = Path.Combine(rootTestDirectory, "bin", "Debug", CommonTestHelper.TargetFramework, testProjectName);
            var extensionPath = Path.Combine(GetTestProjectDirectory(testProjectName), "bin", "Debug", CommonTestHelper.TargetFramework, "browserextension");
            return LoadExtension(extensionPath, copyToBinExtensionPath);
        }

        public Task<WebDriverExtensionHelper> LoadExtensionPublishOutput(string testProjectName)
        {
            var copyToBinExtensionPath = Path.Combine(rootTestDirectory, "bin", "Release", CommonTestHelper.TargetFramework, testProjectName);
            var extensionPath = Path.Combine(GetTestProjectDirectory(testProjectName), "bin", "Release", CommonTestHelper.TargetFramework, "publish", "browserextension");
            return LoadExtension(extensionPath, copyToBinExtensionPath);
        }

        private static async Task<WebDriverExtensionHelper> LoadExtension(string extensionPath, string copyToBinExtensionPath)
        {
            if (Directory.Exists(copyToBinExtensionPath))
            {
                Directory.Delete(copyToBinExtensionPath, true);
            }
            Directory.CreateDirectory(copyToBinExtensionPath);

            foreach (var file in Directory.GetFiles(extensionPath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(extensionPath, file);
                var destFileName = Path.Combine(copyToBinExtensionPath, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(destFileName));
                File.Copy(file, destFileName, true);
            }

            var extension = new WebDriverExtensionHelper();
            await extension.Load(copyToBinExtensionPath);
            return extension;
        }
    }
}
