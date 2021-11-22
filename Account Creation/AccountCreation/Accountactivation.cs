using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AccountCreation
{
    public class Accountactivation
    {
        Dictionary<string, UserDetails> Register;
        Dictionary<string, UserDetails> Activated;
        public Accountactivation() 
        {
           Register = FileRead("D:\\Root\\DataStorageFiles\\Register.txt");
           Activated = FileRead("D:\\Root\\DataStorageFiles\\Activated.txt");
        }
        public void AddDetails(string Name, int Age, string Dob, string Username, string Password, string Email, string Address)
        {
           
            try
            {
                if (IsSymbol(Username) == true)
                {
                    //throw new InvalidDataException("Username should not contains Symbols ");
                   // throw new ArgumentException("Username should not contains Symbols ");
                    throw new FormatException("Username should not contains Symbols ");
                }
               
                if(IsValid(Password))
                {
                    Password=ChangePasswordToHash(Password);
                   
                }
                else
                {
                    throw new ArgumentException("Invalid Argument,Password Should Contains Symbols,numbers,Upper case and Lowercase letter ");
                }
            }
            catch(FormatException dex)
            {
                throw new FormatException(dex.Message);
            }
            catch(ArgumentException ae)
            {
               throw new ArgumentException(ae.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            UserDetails userdetail = new UserDetails(Name, Age, Dob, Password, Email, Address);
            Register.Add(Username,userdetail);
            //Password=ChangePasswordToHash(Password); 
        }
        private bool IsValid(string pass)
        {
            int weight = 0;
            if (pass.Length > 7)
            {
                weight += 1;
            }
            if (IsSymbol(pass))
            {
                weight += 1;
            }
            if (IsDigit(pass))
            {
                weight += 1;

            }
            if (IsUpper(pass))
            {
                weight += 1;

            }
            if (IsLower(pass))
            {
                weight += 1;
            }

            if (weight == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsSymbol(string str)
        {
            char[] symbol = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '>', '<', '?', '/', ':', ';', '=', '"', '[', '{', '}', ']', '+', '-', '_', '|' };
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < symbol.Length; j++)
                {
                    if (str[i] == symbol[j])
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        private bool IsDigit(string str)
        {
            char[] digit = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int[] dig = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < dig.Length; j++)
                {
                    if (str[i] == digit[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsUpper(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 65 && str[i] <= 91)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsLower(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 92 && 118 >= str[i])
                {
                    return true;
                }
            }
            return false;
        }
        public Dictionary<string, UserDetails> FileRead(string path)
        {
            Dictionary<string, UserDetails> Registerdict = new Dictionary<string, UserDetails>(); 
            StreamReader read = new StreamReader(path);
            string line ;

            while ((line = read.ReadLine()) != null)
            {
                string[] details = line.Split('|');
                int i = 0;

                string name = details[i]; i++;
                int age = Convert.ToInt32(details[i]); i++;
                string dob = details[i]; i++;
                string username = details[i]; i++;
                
                string password = details[i]; i++;
                string email = details[i]; i++;
                string address = details[i];
               // line = read.ReadLine();
                UserDetails user = new UserDetails(name, age, dob, password, email, address);
               // user.Username = username;
                // usercheck user = new usercheck("jare",19,"12/09/2002","sfd123sfd","jksds@gmail.com","xxx");
                Registerdict.Add(username, user);
            }
            read.Close();
            return Registerdict;
        }
        public void FileWrite(string path)
        {
            StreamWriter write = new StreamWriter(path);
            foreach (KeyValuePair<string, UserDetails> x in Register)
            {
                Console.WriteLine(x.Key + " " + x.Value);
                string lin = x.Value.Name + "|";
                lin += Convert.ToString(x.Value.Age) + "|" + x.Value.Dob + "|" + x.Key + "|" + x.Value.Password + "|" + x.Value.Email + "|" + x.Value.Address;
                write.WriteLine(lin);
            }
            write.Close();

        }
        public int GenerateOTP()
        {
            Random ran = new Random();
            return ran.Next(10000,100000);
        }
        public bool SendEmail(string receiver)
        {
            try
            {
                if (IsValidEmail(receiver))
                {

                    string OTP = Convert.ToString(GenerateOTP());
                    string body = "Your OTP for Account Activation or Verification is " + OTP;
                    MailMessage message = new MailMessage("Projectvalidation2021@gmail.com", receiver, OTP, body);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("Projectvalidation2021@gmail.com", "Project2021");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    client.Send(message);
                    return true;

                }
                else
                {
                    throw new ArgumentException("Invalid Email Id");
                }

            }
            catch(ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
           
        }
        public bool IsValidEmail(string emailaddress)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(emailaddress, expression))
            {
                if (Regex.Replace(emailaddress, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }
        //public string Checkdictionary()
        //{
        //    string x = "";
        //    foreach(KeyValuePair<string,UserDetails> c in Register)
        //    {
        //        x += c.Value.Name + " " + c.Value.Age + " " + c.Value.Dob + " " + c.Key + " " + c.Value.Password + "  " + c.Value.Email + " " + c.Value.Address;
        //    }
        //    return x;
        //}
        private string ChangePasswordToHash(string pass)
        {
            byte[] pa = ASCIIEncoding.ASCII.GetBytes(pass);
            byte[] tmmp = new MD5CryptoServiceProvider().ComputeHash(pa);
            string hashpass = ByteToString(tmmp);
            return hashpass;

        }
        private string ByteToString(byte[] ar)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < ar.Length;i++ )
            {
                s.Append(Convert.ToString(ar[i]));
            }
            return s.ToString();
        }
        private bool IsHashSame(string dicpass,string inputpass)
        {
            bool IsEqual = false;
            if(dicpass.Length==inputpass.Length)
            {
                int i = 0;
                while((i<dicpass.Length)&&dicpass[i]==inputpass[i])
                {
                    i += 1;
                }
                if (i == dicpass.Length)
                {
                    IsEqual = true;
                }
            }
            return IsEqual;
        }
        public bool Login(string username,string password)
        {
           string pass = ChangePasswordToHash(password);
            foreach(KeyValuePair<string,UserDetails> x in Register)
            {
                if ( x.Key==username && IsHashSame(x.Value.Password,pass))
                {
                    
                        return true;
                    
                }
            }
            //foreach(KeyValuePair<string,UserDetails> x in Activated)
            //{
            //    if (x.Key == username && x.Value.Password == pass)
            //    {
            //        return true;
            //    }
            //}
            return false;

        }
         public bool PasswordReset(string password,string username)
        {
            string pass=ChangePasswordToHash(password);
            foreach(KeyValuePair<string,UserDetails> x in Register)
            {
                if (x.Key == username)
                {
                    x.Value.Password = pass;
                    return true;
                }
            }
            foreach(KeyValuePair<string,UserDetails> x in Activated)
            {
                if(x.Key==username)
                {
                    x.Value.Password = pass;
                    return true;
                }
            }
            return false;
        }
       
    }
}
