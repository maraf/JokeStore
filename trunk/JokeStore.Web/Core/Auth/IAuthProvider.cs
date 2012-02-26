﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JokeStore.Web.Core.Auth
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);

        void SignOut();
    }
}
