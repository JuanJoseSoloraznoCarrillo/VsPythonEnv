
namespace vsenvprj;

public static class Tooling{
    public static string Magenta = "\u001b[38;5;13m"; // Magenta text color
    public static string Green = "\u001b[38;5;10m";   // Green text color
    public static string Reset = "\u001b[0m";         // Reset to default color
    public static List<string> GetMainContent(){
        List<string> content = new List<string>(){"{","\t\"folders\": [{\"path\": \".\"}],",
                             "\t\"settings\": {\"python.analysis.extraPaths\": ["};
        return content;
    }
    public static List<string> GetDebugContent(){
        List<string> content = new List<string>(){"\"launch\": {",
            "\t\t\t\"version\": \"0.2.0\",",
            "\t\t\t\"configurations\": [{",
            "\t\t\t\"name\": \"Python: DebugForTA\",",
            "\t\t\t\"type\": \"python\",",
            "\t\t\t\"request\": \"launch\",",
            "\t\t\t\"program\": \"${file}\",",
            "\t\t\t\"console\": \"integratedTerminal\" }",
            "]}",
            "}"};
        return content;
    }
    public static string GetLastPathPattern(string path){
        string? pattern;
        if(path.Contains('\\')){
            pattern = path.Split('\\').LastOrDefault();
        }else if(path.Contains('/')){
            pattern = path.Split('/').LastOrDefault();
        }
        else{
            pattern = "None";
        }
        if(pattern != null){
            return pattern;
        }else{
            return "None";
        }
    }
    public static void Header(bool head){
        string pattern = new string('+',85);
        string donePattern = new string('-',85);
        string message = $"{pattern}\n\t\t\tPYTHON ENVIRONMENT CONFIGURATION\n{pattern}";
        if(head){
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }else{
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(donePattern);
            Console.ResetColor();
        }
    }
    public static void PrintHelp(){
        Console.WriteLine("[?] Usage:\n\t$> vsenv.exe [opt: TargetPath] [opt: -h | --help]");
    }
}