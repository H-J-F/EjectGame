using System;
using System.Collections.Generic;

namespace MyGameServer.Model
{
    [Serializable]
    class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual int Type { get; set; }
        public virtual int Online_Type { get; set; }
        public virtual int Gender { get; set; }
        public virtual int Gold_num { get; set; }
        public virtual int Diamond_num { get; set; }
        public virtual int Lives_num { get; set; }
    }
}
