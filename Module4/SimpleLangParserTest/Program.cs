using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SimpleLangParser;
using SimpleLangLexer;

namespace SimpleLangParserTest
{
    class Program
    {
        public static void Test(string fileContents)
        {
            TextReader inputReader = new StringReader(fileContents);
            Lexer l = new Lexer(inputReader);
            Parser p = new Parser(l);
            try
            {
                p.Progr();
                if (l.LexKind == Tok.EOF)
                {
                    Console.WriteLine("Program successfully recognized");
                }
                else
                {
                    p.SyntaxError("end of file was expected");
                }
            }
            catch (ParserException e)
            {
                Console.WriteLine("lexer error: " + e.Message);
            }
            catch (LexerException le)
            {
                Console.WriteLine("parser error: " + le.Message);
            }
        }
        static void Main(string[] args)
        {
            string fileContents = @"begin
    a := 2;
    cycle a
    begin
        b := a;
        c := 234
    end

end";

            string fileContents1 = @"begin
    while a do
        b := 11
end";
            string fileContents1_1 = @"begin
    while a do
        cycle b
    begin
        b := a;
        c := 234
    end
end";

            string fileContents2 = @"begin
    for a := 1 to 5 do
        b := 11
end";

            Console.WriteLine(" --- Test1 ---");
            Test(fileContents);

            Console.WriteLine("\n --- Test2 (WHILE expr DO statement) ---");
            Test(fileContents1);
            Test(fileContents1_1);

            Console.WriteLine("\n --- Test3 (FOR ID := expr TO expr DO statement) ---");
            Test(fileContents2);

        }
    }
}
