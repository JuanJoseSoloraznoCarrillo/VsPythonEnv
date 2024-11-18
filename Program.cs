
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
                }else{
                    Initialize(args[0]);
                }
            }
        }
        return 1;
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
