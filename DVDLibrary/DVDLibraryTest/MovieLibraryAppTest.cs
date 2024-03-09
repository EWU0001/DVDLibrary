using DVDLibrary;

namespace DVDLibraryTest
{
    public class UnitTest1
    {
        [Fact]
        public void MainMenu_StaffAuthentication_Successful()
        {
            // Arrange
            var input = new StringReader("staff\ncraftbeer\n"); // Simulate successful authentication
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            bool authenticated = MovieLibraryApp.StaffAuth();

            // Assert
            string actualOutput = output.ToString();
            Assert.Contains("Log In successful!", actualOutput);
            Assert.True(authenticated); // Ensure authentication was successful
        }

        [Fact]
        public void MainMenu_StaffAuthentication_Unsuccessful()
        {
            // Arrange
            var input = new StringReader("wrongusername\nwrongpassword\n0\n"); // Simulate unsuccessful authentication
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            bool authenticated = MovieLibraryApp.StaffAuth();

            // Assert
            string actualOutput = output.ToString();
            Assert.Contains("User name or password is incorrect. Input again or Enter '0' to cancel.", actualOutput.Trim());
            Assert.Contains("Enter staff username >> ", actualOutput);
            Assert.Contains("Enter password >> ", actualOutput);
            Assert.False(authenticated); // Ensure authentication was unsuccessful
        }
    }
}
