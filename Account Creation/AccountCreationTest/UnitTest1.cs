using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountCreation;
using System.Collections.Generic;

namespace AccountCreationTest
{
    [TestClass]
    public class UnitTest1
    {
        Accountactivation at = new Accountactivation();
        //Mail Receiving Test
        [TestMethod]
        public void SendMailTest()
        {
            bool expected = true;
            bool Actual = at.SendEmail("harithamathi58@gmail.com");
            Assert.AreEqual(expected, Actual);
        }
        [TestMethod]
        public void MailTest()
        {
            bool expected = true;
            bool Actual = at.SendEmail("harithamathi97@gmail.com");
            Assert.AreEqual(expected, Actual);
        }
        
        //OTP Generation Test
        [TestMethod]
        public void OTPTest()
        {
            int expect = 10000;
            int Actual = at.GenerateOTP();
            Assert.AreEqual(expect,Actual);
        }

        //File Reader Test
        [TestMethod]
        public void FileReadTest()
        {
            string expected = "Haritha 19 12/09/2002 hari1234 126201177176222193227208235405170123186732 harimathi@gmail.com no.9,Anna Nagar,Chennai Haritha 19 12/09/2002 hari1997 10585239150149212123193759616416170238246173 harimathi@gmail.com no.9,Anna Nagar,Chennai  ";
            string Actual = "";
            Dictionary<string, UserDetails> ud = at.FileRead("D:\\Root\\DataStorageFiles\\Register.txt");
            foreach(KeyValuePair<string,UserDetails> x in ud)
            {
                //Actual += x.Key;
                Actual += x.Value.Name + " " + x.Value.Age + " " + x.Value.Dob + " " + x.Key + " " + x.Value.Password + " " + x.Value.Email + " " + x.Value.Address+" ";
            }
            Assert.AreEqual(expected,Actual);
            
        }
       
       //Login Test
        [TestMethod]
        public void LoginTest()
        {
            bool expected = true;
            bool Actual = at.Login("hari1997","Hari-1997");
            Assert.AreEqual(expected, Actual);
        }
        //Age Limit Test
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException),"Invalid Age")]
        public void AgeLimitTest()
        {
            at.AddDetails("Haritha",1,"12/02/2020","UserHari","Pass123@","hari@gmail.com","xxx");
        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Invalid Age")]
        public void AgeLimitMaxTest()
        {
            at.AddDetails("Haritha",80, "12/02/2101", "UserHari", "Pass123@", "hari@gmail.com", "xxx");
        }

        //Dob Limit Test
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Invalid Age ")]
        public void DobLimitminTest()
        {
            at.AddDetails("Haritha", 19, "12/02/2020", "UserHari", "Pass123@", "hari@gmail.com", "xxx");
        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException), "Invalid Age")]
        public void DobMaxLimitTest()
        {
            at.AddDetails("Haritha", 19, "12/02/2120", "UserHari", "Pass123@", "hari@gmail.com", "xxx");
        }

        //Email Test
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Invalid Email Id")]
        public void EmailFormatTest()
        {
            at.AddDetails("Haritha", 19, "12/02/2002", "UserHari", "Pass123@", "harigmail.com", "xxx");
        }
        [TestMethod]
        // [ExpectedException(typeof(System.ArgumentException))]

        public void EmailValidateTest()
        {
            bool expected = false;
            bool actual = at.IsValidEmail("harithagmail.com");
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException), "Invalid Email Id")]
        public void EmailTest()
        {
            bool expected = true;
            bool Actual = at.SendEmail("Harithagmail");
        }

        //Username Test
        [TestMethod]
        [ExpectedException(typeof(System.FormatException) )]
        public void UsernameTest()
        {
            at.AddDetails("Haritha", 19, "12/02/2002", "User&Hari", "Pass123@", "hari@gmail.com", "xxx");
        }

        //Password Test
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void PasswordTest()
        {
            at.AddDetails("Haritha", 19, "12/02/2002", "UserHari", "Pass123", "hari@gmail.com", "xxx");
        }


    }
}
