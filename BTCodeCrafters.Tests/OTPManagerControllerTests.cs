using BTCodeCraftersOTP.Controllers;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace BTCodeCrafters.Tests
{
    public class OTPManagerControllerTests
    {
        //For generating the OTP:
        [Fact]
        public void GenerateOTP_Generating_Correctly()
        {
            //Arrange:   
            var lengthEncryptedTest = 24;
            var fakeLogger = A.Fake<ILogger<OTPManagerController>>();
            var controller = new OTPManagerController(fakeLogger);

            //Act:
            var encrypted = controller.GenerateOTP().Value;
            var lengthEncrypted = encrypted.ToString().Length;
            //var generateOTP = controller.GenerateOTP();
            //var encrypted = controller.OTP;

            //Assert:
            //Same number of digits:
            Assert.True(lengthEncryptedTest == lengthEncrypted);
            Assert.NotNull(lengthEncrypted);
        }

        //For login:
        [Fact]
        public void LoginWithOTP_CorrectInput()
        {
            //Arrange:   
            //var encryptedMessage = "ZVMFPMTFQWIW";
            var encryptedMessage = "OkPsfFVk7nzy7FTJnNd4tg ==";
            var fakeLogger = A.Fake<ILogger<OTPManagerController>>();
            var controller = new OTPManagerController(fakeLogger);

            //Write in file the decrypted:
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine("ZVMFPMTFQWIW");
                //OkPsfFVk7nzy7FTJnNd4tg ==
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Close file:
                writer.Close();
            }

            //Act:
            var result = controller.LoginWithOTP(encryptedMessage);

            //Assert:
            Assert.NotNull(result);
            Assert.True(result.Value.Equals("Good OTP!"));
            
            //Final:
            //Empty the file:
            writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine("");
                //OkPsfFVk7nzy7FTJnNd4tg ==
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Close file:
                writer.Close();
            }
        }
    }
}