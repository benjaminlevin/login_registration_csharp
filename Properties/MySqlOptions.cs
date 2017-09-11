//*--Added for secure DB use
//"Properties" directory in root, MySqlOptions.cs contained
//This file contains all 'models'
using System;
using System.ComponentModel.DataAnnotations;

namespace login_registration
{
    public class MySqlOptions
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}