/**
    @Author: Solorzano, Juan Jose.
    @Date: 2021-10-10
    * MIT License
    * VsPythonEnv - Visual Studio Python Environment
    * @description: Create a Python environment for Visual Studio
    * @version: 1.0.0
    ---------------------------------------------------------------------------
    Description:
    This program creates a Python environment for Visual Studio. It creates
    a virtual environment, installs the necessary packages, and creates a Python file with the necessary code to start a project.
*/
namespace vsenvprj;
public static class Program{
    public static int Main(string [] args){
        if(args.Length == 0){
            string basePath = Path.GetFullPath(".");
            Initialize(basePath);
        }else{
            if(args.Length > 1){
                Console.WriteLine("[!] Error: there are only two possible arguments:");
                Tooling.PrintHelp();
            }
            else{
                if(args[0].Contains("--help") || args[0].Contains("-h")){
                    Tooling.PrintHelp();
                    return 1;
                }else{
                    Initialize(args[0]);
                }
            }
        }
        return 0;
    }
    public static void Initialize(string path){
        Tooling.Header(true);
        VsEnv pyEnv = new VsEnv(path);
        Console.WriteLine($"[*] File name: {pyEnv.FileName}");
        Console.WriteLine($"[*] File will be create at: {pyEnv.WorkPath}");
        pyEnv.Create();
        Console.WriteLine("[+] File has been created");
        pyEnv.OpenVsPythonEnv();
        Tooling.Header(false);
    }
}
