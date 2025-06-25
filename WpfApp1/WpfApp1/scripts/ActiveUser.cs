using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    public class ActiveUser
    {
        public int _userId;
        public string _username;
        private byte[] _datakey;
        public ActiveUser(int userId, string username, string password)
        {
            this._userId = userId;
            this._username = username;
            this._datakey = KeyLoader.LoadDataKey(username, password);
        }
        public byte[] GetDataKey()
        {
            return _datakey;
        }
    }
}
