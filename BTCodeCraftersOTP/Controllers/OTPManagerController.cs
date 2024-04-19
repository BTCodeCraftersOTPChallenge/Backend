using Microsoft.AspNetCore.Mvc;
using RandomString4Net;
using System.Text;

namespace BTCodeCraftersOTP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPManagerController : ControllerBase
    {
        //Logger:
        private readonly ILogger<OTPManagerController> _logger;
        private string OTP;
        private string savedOTP;
        private string secretKey;

        public OTPManagerController(ILogger<OTPManagerController> logger)
        {
            _logger = logger;
            OTP = "";
            secretKey = "rktlqtuixakparuo";
        }

        //For generating OTP:
        [HttpGet]
        [Route("generateOTP")]
        public JsonResult GenerateOTP()
        {
            _logger.LogInformation("GenerateOTP was called.");

            //OTP generated:
            //Example:
            //var OTP = "DJASKFLHKAJD"; //26^12;
            OTP = RandomString.GetString(Types.ALPHABET_UPPERCASE, 12);
            savedOTP = OTP;

            //Write in file:
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine(OTP);
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
            
            Console.WriteLine("Decrypted as: " + OTP + " ;"); 

            //Encrypted OTP: 24 characters:
            var encrypted = EncryptAndDecryptPassword.Encrypt(OTP, secretKey); //abcdefghijklmnop 16 characters lower letters;
            Console.WriteLine("Encrypted as: " + encrypted + " ;"); 

            //return new JsonResult("OTP generated.");
            return new JsonResult(encrypted);
        }

        //For trying to log in:
        [HttpPost]
        [Route("loginWithOTP")]
        public JsonResult LoginWithOTP([FromBody] string encryptedOTP)
        {
            _logger.LogInformation("LoginWithOTP was called.");
            Console.WriteLine("New encrypted as: " + encryptedOTP + " ;");

            //Get the encrypted OTP and decrypt it:
            var newDecrypted = EncryptAndDecryptPassword.Decrypt(encryptedOTP, secretKey);
            Console.WriteLine("New decrypted as: " + newDecrypted + " ;");

            //Read from file:
            string dataFromFile = "";
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("../EncryptedOTP.txt");

                dataFromFile = reader.ReadLine();
                savedOTP = dataFromFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Close file:
                reader.Close();
            }
            Console.WriteLine("Old decrypted as: " + savedOTP + " ;");

            //Try to see if they are the same:
            if (newDecrypted.Equals(savedOTP) == true && newDecrypted.Equals("") == false)
            {
                //They are the same, we can login: Doesnt matter what we send:
                //Before, we erase what we already have:
                OTP = "";
                savedOTP = "";

                //Write in file:
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter("../EncryptedOTP.txt");
                    writer.WriteLine("");
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

                return new JsonResult("Good OTP!");
            }
            else
            {
                //Not the same:
                return new JsonResult("Not a valid OTP!");
            }
        }
    }
}
