﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskMan.BusinessObjects
{
    public class ApplicationUser : IdentityUser<Guid>
    {

    }
}
