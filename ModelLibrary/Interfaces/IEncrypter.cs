using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Interfaces
{
    public interface IEncrypter
    {
        public string Encrypt(string clearText);
        public string Decrypt(string clearText);
    }
}
