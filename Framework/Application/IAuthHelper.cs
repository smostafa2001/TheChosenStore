﻿using System.Collections.Generic;

namespace Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);
        bool IsAuthenticated();
        void SignOut();
        string CurrentAccountRole();
        AuthViewModel CurrentAccount { get; }
        List<int> GetPermissions();
    }
}