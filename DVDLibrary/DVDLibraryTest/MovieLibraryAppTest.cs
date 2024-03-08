using DVDLibrary;
using System.Text;
using Xunit;

namespace DVDLibraryTest
{
    public class UnitTest1
    {
        [Fact]
        public void MainMenu_ExitProgram()
        {
            var input = new StringBuilder();
            var output = new StringBuilder();
            var expectedOutput = new StringBuilder();

            input.AppendLine("0"); // enter 0 to exit the program
            expectedOutput.AppendLine("\nMain Menu");
            expectedOutput.AppendLine("-----------------");
            expectedOutput.AppendLine("\nSelect from the following:");
            expectedOutput.AppendLine("1. Staff");
            expectedOutput.AppendLine("2. Member");
            expectedOutput.AppendLine("0. End the program");
            expectedOutput.Append("\nEnter your choice ==> ");
            expectedOutput.AppendLine("Exiting program...");
            expectedOutput.AppendLine("!-----Good Bye-----!");

            using (var consoleInput = new StringReader(input.ToString())) 
            {
                using (var consoleOutput = new StringWriter(output))
                {
                    Console.SetIn(consoleInput); 
                    Console.SetOut(consoleOutput);

                    var MovieLibraryApp = new MovieLibraryApp();
                    MovieLibraryApp.MainMenu();

                    Assert.Equal(expectedOutput.ToString(), output.ToString());
                }
            }

        }
    }
}