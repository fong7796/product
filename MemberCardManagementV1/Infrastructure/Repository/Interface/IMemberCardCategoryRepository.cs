using MemberCardManagementV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberCardManagementV1.Infrastructure.Repository.Interface
{
    public interface IMemberCardCategoryRepository
    {
        List<MemberCardCategory> GetListMemberCardCategory();
    }
}
