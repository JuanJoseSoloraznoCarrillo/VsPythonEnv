using System.Diagnostics;

namespace vsenvprj{
    public class VsEnv{
        // Private class attributes.
        private string codeKeyWord = "${workspaceFolder}";
        private string jsonName = ".code-workspace.json";
        private string envFileName = ".env";
        private string envContent = "PYTHONPATH=";
        private string wpath = "";
        private string rootFolder = "";
        private string fileName = "";
        private string rootFilePath = "";
        // Public getters.
        public string RootFilePath {get{return rootFilePath;}}
        public string MainFolderName { get { return rootFolder; }}
        public string WorkPath { get { return wpath; }}
        public string FileName {get {return fileName;}}
        // Constructor.
        public VsEnv(string wpath){
            Console.WriteLine("[*] Creating file configuration ...");
            if(Path.Exists(wpath)){
                this.wpath = wpath;
            }else{
                Console.WriteLine(new string('!',85));
                throw new DirectoryNotFoundException($"Path given: [{wpath}] not found.");
            }
            rootFolder = Tooling.GetLastPathPattern(this.wpath);
            fileName = $"{rootFolder}{jsonName}";
            rootFilePath = this.wpath+"\\"+fileName;
        }
        public void Create(){
            // code workspace file creation.
            FileStream confFile;
            List<string> mainContent = GetMainContent();
            confFile = File.Create($"./{fileName}");
            confFile.Close();
            File.WriteAllLines($"./{fileName}",mainContent);
            // .env file creation.
            FileStream envFile;
            envFile = File.Create($"./{envFileName}");
            envFile.Close();
            List<string> envFileContent = new List<string>(){envContent,"PYTHON_PATH=C:\\LegacyApp\\Python27_x64\\python.exe"};
            File.WriteAllLines($"./{envFileName}",envFileContent);
        }
        public void OpenVsPythonEnv(){
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($">> Openning: {RootFilePath}");
            Console.ResetColor();
            try{
                Process.Start("code",RootFilePath);
            }
            catch (Exception except){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {except.Message}");
                Console.ResetColor();
            }
        }
        private List<string> GetMainContent(){
            List<string> mainContent = Tooling.GetMainContent();
            List<string> debugCont = Tooling.GetDebugContent();
            List<string> fileContent = GetWorkingPaths(rootFolder);
            int cnt = 0;
            int maxlenght = fileContent.Count;
            foreach(string path in fileContent){
                cnt++;
                if(cnt<maxlenght){
                    mainContent.Add($"\t\t\"{path}\",");
                }
                else{
                    mainContent.Add($"\t\t\"{path}\"");
                }
                envContent += path.Replace(codeKeyWord,".")+";";
            }
            mainContent.Add("\t]},");
            mainContent.AddRange(debugCont);
            return mainContent;
        }
        private List<string> GetWorkingPaths(string? mainFolder){
            string[] dirs = Directory.GetFiles(wpath,"*.py",SearchOption.AllDirectories);
            List<string> paths = new List<string>();
            foreach(string dir in dirs){
                string? fileName = Tooling.GetLastPathPattern(dir);
                string relPath = mainFolder+dir.Split(mainFolder)[1];
                string pathPattern = relPath.Replace($"{mainFolder}",$"{codeKeyWord}");
                string finalPath = pathPattern.Replace($"{fileName}","").Replace("\\","/");
                paths.Add(finalPath);
            }
            return paths;
        }
    }
}