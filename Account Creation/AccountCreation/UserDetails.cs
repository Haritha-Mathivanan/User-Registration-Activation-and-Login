using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;



namespace AccountCreation
{
    public class UserDetails
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set 
            {
                try
                {
                    if (value.Length >= 3 && value.Length <= 15)
                    {
                        _name = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Name Contains Only 3 to 15 Characters");
                    }
                }
                catch (ArgumentOutOfRangeException argex)
                {
                    throw new ArgumentOutOfRangeException(argex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        int _age;

        public int Age
        {
            get { return _age; }
            set
            {
                try
                {
                    if (value >= 18 && value <= 60)
                    {
                        _age = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Invalid Age Limit");
                    }
                }
                catch (ArgumentOutOfRangeException argex)
                {
                    throw new ArgumentOutOfRangeException(argex.Message+"/n Age Limit Should be 18 to 60");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        string _dob;

        public string Dob
        {
            get { return _dob; }
            set
            {

                try
                {
                    string today = DateTime.Now.ToString("d/MM/yyyy");
                    int agedif = YearDifference(today, value);
                    if (agedif >= 18 && agedif <= 60)
                    {
                        _dob = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Invalid Age ");
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException(ex.Message);
                }
                catch (Exception exp)
                {
                    throw new Exception(exp.Message);
                }
            }
        }
        //string _username;

        //public string Username
        //{
        //    get { return _username; }
        //    set 
        //    {
        //        try
        //        {
        //            if (IsSymbol(value)==false)
        //            {
        //                _username = value;
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Username should not contains Symbols ");
        //            }
        //        }
        //        catch(ArgumentException ae)
        //        {
        //            Console.WriteLine(ae.Message);
        //        }
        //        catch(Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
                
        //    }
        //}
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
        private int YearDifference(string today, string dateofbirth)
        {

            string[] td = today.Split('-');
            string[] bd = dateofbirth.Split('/');
            int date1 = Convert.ToInt32(td[0]);
            int date2 = Convert.ToInt32(bd[0]);
            int mon1 = Convert.ToInt32(td[1]);
            int mon2 = Convert.ToInt32(bd[1]);
            int year1 = Convert.ToInt32(td[2]);
            int year2 = Convert.ToInt32(bd[2]);
            int diff = year1 - year2;
            if (mon2 > mon1)
            {
                diff = diff - 1;
            }
            else if (mon2 == mon1 && date2 < date1)
            {
                diff = diff - 1;
            }
            return diff;


        }
        private bool IsLeap(int year)
        {
            bool Isleap = false;
            if (year % 4 == 0&&year%100==0&&year%400==0)
            {
                Isleap = true;
            }
            if (year % 100 != 0 && year % 4 == 0)
            {
                Isleap = true;
            }
            return Isleap;
        }
        string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }
       
        string _email;

        public string Email
        {
            get { return _email; }
            set
            {
               // _email = value;
                try
                {
                    if (IsValidEmail(value))
                    {
                        _email = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Email Id");
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private bool IsValidEmail(string emailaddress)
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
        string _address;

        public string Address
        {
            get { return _address; }
            set 
            {
                try
                {
                    if (value.Length <= 120)
                    {
                        _address = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Address can Contains only below 120 characters");
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException(ex.Message);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public UserDetails(string name,int age,string dob,string password,string email,string address)
        {
            this.Name = name;
            this.Age = age;
            this.Dob = dob;
            this.Password = password;
            this.Email = email;
            this.Address = address;
        }
    }
}
