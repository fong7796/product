﻿using MemberCardManagementV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCardManagementV1.Core.Service.Interface
{
    public interface IMemberCardCategoryService
    {
        List<MemberCardCategory> GetListMemberCardCategory();
    }
}
