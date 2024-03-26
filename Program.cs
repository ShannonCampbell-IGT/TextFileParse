// See https://aka.ms/new-console-template for more information
namespace TextParser
{
    enum SearchState
    {
        FindGlobalRX,
        FindFirstTX,
        FindFirstRX,
        FindLastTX,
        WriteFile
    };

    class Program
    {
        static void Main(string[] args)
        {
            // if (CheckArguments(args) == false)
            //  return;

            var inputFile = "qcom_in.txt";
            var outputFile = "qcom_out.txt";
            ParseComlabEGMInput(inputFile, outputFile);
        }
        static bool CheckArguments(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("       ComlabParser <input-file> [output-file].");
                Console.WriteLine("Where:");
                Console.WriteLine("       input-file:  Comlab RTB file to parse.");
                Console.WriteLine("       output-file: Output file. If not specified, it is input-file with .parsed appended to the end.");
                return false;
            }
            return true;

        }
        static void ParseComlabEGMInput(string inputFile, string outputFile)
        {
            var data = File.ReadAllLines(inputFile);
            var firsttx = string.Empty;
            var secondtx = string.Empty;
            var nexttx = string.Empty;
            var firstrx = string.Empty;
            var global = string.Empty;
            SearchState search = SearchState.FindGlobalRX;
            bool firstgo = true;


            foreach (var readLine in data)
            {
                switch (search)
                {
                    case SearchState.FindGlobalRX:
                        {
                            global = readLine;
                            search = SearchState.FindFirstTX;
                        }
                        break;

                    case SearchState.FindFirstTX:
                        if (readLine.Contains("TX"))
                        {
                            if (firsttx == string.Empty)
                            {
                                if (firstgo)
                                {
                                    firsttx = readLine;
                                }
                                else
                                {
                                    firsttx = secondtx;
                                    secondtx = readLine;
                                }
                            }
                            else
                            {
                                secondtx = readLine;
                            }

                            search = SearchState.FindFirstRX;
                        }
                        break;

                    case SearchState.FindFirstRX:
                        if (readLine.Contains("RX : 01"))
                            firstrx = readLine;
                                                                        
                        if (!firstgo)
                        {
                            File.AppendAllText(outputFile, global + "\n");
                            File.AppendAllText(outputFile, firstrx + "\n");
                            if (firsttx != string.Empty)
                            {
                                File.AppendAllText(outputFile, firsttx + "\n");
                                firsttx = string.Empty;
                            }

                        }
                        search = SearchState.FindGlobalRX;
                        firstgo = false;
                        break;

                }
            }
        }
    }
};

